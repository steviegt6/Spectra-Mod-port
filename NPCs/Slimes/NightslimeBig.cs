using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpectraMod.Items.Banner;

namespace SpectraMod.NPCs.Slimes
{
    public class NightslimeBig : SpectraNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Large Night Slime");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SafeSetDefaults()
        {
            npc.aiStyle = 1;
            animationType = NPCID.BlueSlime;
            banner = npc.type;
            bannerItem = ModContent.ItemType<NightslimeBanner>();

            npc.damage = Main.hardMode ? 64 : 48;

            if (!Main.hardMode) npc.lifeMax = 124;
            else npc.lifeMax = NPC.downedPlantBoss ? 506 : 248;
        }


        public override void NPCLoot()
        {
            int amount = Main.expertMode ? 4 : 2;
            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.Gel.BlackGel>(), amount);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (SpawnCondition.OverworldNightMonster.Chance > 0f) ? SpawnCondition.OverworldNightMonster.Chance / 2.5f : 0f;
        }
    }
}
