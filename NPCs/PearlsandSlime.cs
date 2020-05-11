using Microsoft.Xna.Framework;
using SpectraMod.Items.Banner;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.NPCs
{
    public class PearlsandSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.Size = new Vector2(32, 22);
            npc.damage = 15;
            npc.aiStyle = 1;
            animationType = NPCID.SandSlime;
            banner = npc.type;
            bannerItem = ModContent.ItemType<PearlsandSlimeBanner>();
            if (!Main.hardMode) npc.lifeMax = 85;
            else npc.lifeMax = 170;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (!Main.hardMode) damage = 16;
            else damage = 32;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.Gel);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneDesert && spawnInfo.player.ZoneHoly) ? SpawnCondition.OverworldDayDesert.Chance * 1.5f : 0f;
        }
    }
}
