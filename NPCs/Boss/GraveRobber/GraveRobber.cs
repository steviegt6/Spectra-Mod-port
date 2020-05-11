using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpectraMod.Items.Boss.GraveRobber;
using Terraria.Utilities;

namespace SpectraMod.NPCs.Boss.GraveRobber
{
    public class GraveRobber : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Grave Robber");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.Size = new Vector2(34, 23);
            npc.boss = true;
            npc.aiStyle = 3;
            npc.value = Item.sellPrice(0, 1, 75, 50);
            npc.defense = 5;
            npc.lifeMax = 750;
            npc.damage = 13;
            npc.knockBackResist = 0f;
            npc.buffImmune[BuffID.Confused] = true;
            music = MusicID.Boss1;
            musicPriority = MusicPriority.BossLow;
            bossBag = ModContent.ItemType<GraverobberBag>();
            animationType = NPCID.Zombie;
            npc.buffImmune[BuffID.Confused] = true;
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(4)) NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Zombie);
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if ((Main.rand.NextBool(4)) && (npc.life > npc.lifeMax / 5)) NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Zombie);
            if ((Main.expertMode) && (Main.rand.NextBool(2)) && (npc.life < npc.lifeMax / 5)) NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Drippler);
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<HatredBar>(), Main.rand.Next(14) + 1);
                //var dropChoice = new WeightedRandom<int>();
                //dropChoice.Add(ModContent.ItemType<GraverobberMachete>());
                //dropChoice.Add(ModContent.ItemType<GraverobberRanged>());
                //dropChoice.Add(ModContent.ItemType<GraverobberMagic>());
                //dropChoice.Add(ModContent.ItemType<GraverobberThrown>());
                //int choice = dropChoice;
                Item.NewItem(npc.getRect(), ModContent.ItemType<GraverobberMachete>());
            }
            else
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<HatredBar>(), Main.rand.Next(16) + 2);
                npc.DropBossBags();
            }
        }
    }
}