using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpectraMod.Items.Banner;

namespace SpectraMod.NPCs.Slimes
{
    public class PearlsandSlime : SpectraNPC
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
            bannerItem = ModContent.ItemType<PearlsandSlimeBanner>();

            npc.damage = NPC.downedPlantBoss ? 32 : 16;

            if (!Main.hardMode) npc.lifeMax = 85;
            else npc.lifeMax = 170;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.Gel);
            SpectraHelper.AttemptSlimeStaff(npc, 485);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneDesert && spawnInfo.player.ZoneHoly) ? SpawnCondition.OverworldDayDesert.Chance * 1.5f : 0f;
        }
    }
}
