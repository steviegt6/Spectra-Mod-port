using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpectraMod.Items.Trophies;
using SpectraMod.Items.Boss.GraveRobber;

namespace SpectraMod.NPCs.Boss.GraveRobber
{
    [AutoloadBossHead]
    public class GraveRobber : SpectraNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Grave Robber");
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SafeSetDefaults()
        {
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
            animationType = NPCID.ArmoredSkeleton;
            npc.buffImmune[BuffID.Confused] = true;
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            int zombiesChosenNormal = Main.rand.Next(0, SpectraHelper.Pool_ZombiesNormal.Length);
            int zombiesChosenExpert = Main.rand.Next(0, SpectraHelper.Pool_ZombiesExpert.Length);
            if (Main.rand.NextBool(4) && !Main.expertMode) NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, SpectraHelper.Pool_ZombiesNormal[zombiesChosenNormal]);
            if (Main.rand.NextBool(3) && Main.expertMode) NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, SpectraHelper.Pool_ZombiesExpert[zombiesChosenExpert]);
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            int zombiesChosenNormal = Main.rand.Next(0, SpectraHelper.Pool_ZombiesNormal.Length);
            int zombiesChosenExpert = Main.rand.Next(0, SpectraHelper.Pool_ZombiesExpert.Length);
            if (Main.rand.NextBool(6) && !Main.expertMode) NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, SpectraHelper.Pool_ZombiesNormal[zombiesChosenNormal]);
            if (Main.rand.NextBool(5) && Main.expertMode) NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, SpectraHelper.Pool_ZombiesExpert[zombiesChosenExpert]);
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                SpectraHelper.SimpleItemDrop<GraverobberMachete>(npc, 1, 1f);
                Item.NewItem(npc.getRect(), ModContent.ItemType<HatredBar>(), Main.rand.Next(14) + 1);
            }
            else
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<HatredBar>(), Main.rand.Next(16) + 2);
                npc.DropBossBags();
            }

            if (Main.rand.NextBool(10)) Item.NewItem(npc.getRect(), ModContent.ItemType<GraverobberTrophy>());
        }
    }
}