using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpectraMod.Items.Materials;
using SpectraMod.Items.Banner;

namespace SpectraMod.NPCs
{
    public class Darkslime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Slime");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.Size = new Vector2(32, 22);
            npc.damage = 15;
            npc.aiStyle = 1;
            animationType = NPCID.BlueSlime;
            banner = npc.type;
            bannerItem = ModContent.ItemType<DarkslimeBanner>();
            if (!Main.hardMode) npc.lifeMax = 100;
            else npc.lifeMax = NPC.downedPlantBoss ? 200 : 300;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (!Main.hardMode) damage = 16;
            else damage = NPC.downedPlantBoss ? 84 : 55;
        }

        public override void NPCLoot()
        {
            int amount = Main.expertMode ? 2 : 1;
            Item.NewItem(npc.getRect(), ModContent.ItemType<ShadowGel>(), amount);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (SpawnCondition.OverworldNightMonster.Chance > 0f) ? SpawnCondition.OverworldNightMonster.Chance / 2.75f : 0f;
        }
    }
}
