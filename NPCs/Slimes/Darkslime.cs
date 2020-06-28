using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpectraMod.Items.Banner;

namespace SpectraMod.NPCs.Slimes
{
    public class Darkslime : SpectraNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Slime");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SafeSetDefaults()
        {
            npc.aiStyle = 1;
            animationType = NPCID.BlueSlime;
            banner = npc.type;
            bannerItem = ModContent.ItemType<DarkslimeBanner>();

            npc.damage = 64;

            if (!Main.hardMode) npc.lifeMax = 128;
            else npc.lifeMax = NPC.downedPlantBoss ? 256 : 512;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }

        public override void NPCLoot()
        {
            int amount = Main.expertMode ? 2 : 1;
            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.Gel.DoomGel>(), amount);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (SpectraWorld.professionalMode)
            {
                if (Main.rand.NextBool(3))
                    target.AddBuff(BuffID.Slimed, Main.rand.Next(3, 11));
                if (Main.rand.NextBool(5))
                    target.AddBuff(BuffID.Darkness, Main.rand.Next(2, 4));
            }
            base.OnHitPlayer(target, damage, crit);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (SpawnCondition.OverworldNightMonster.Chance > 0f) ? SpawnCondition.OverworldNightMonster.Chance / 2.75f : 0f;
        }
    }
}
