using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpectraMod.Items.Banner;

namespace SpectraMod.NPCs.Slimes
{
    public class EbonsandSlime : SpectraNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SafeSetDefaults()
        {
            npc.damage = 15;
            npc.aiStyle = 1;
            animationType = NPCID.SandSlime;
            banner = npc.type;
            bannerItem = ModContent.ItemType<EbonsandSlimeBanner>();

            npc.damage = Main.hardMode ? 24 : 48;

            if (!Main.hardMode) npc.lifeMax = 75;
            else npc.lifeMax = 150;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.Gel);
            SpectraHelper.AttemptSlimeStaff(npc, 500);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (SpectraWorld.professionalMode)
                if (Main.rand.NextBool(3))
                    target.AddBuff(BuffID.Slimed, Main.rand.Next(3, 11));
            base.OnHitPlayer(target, damage, crit);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneDesert && spawnInfo.player.ZoneCorrupt) ? SpawnCondition.OverworldDayDesert.Chance * 1.5f : 0f;
        }
    }
}
