using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpectraMod.Items.Banner;

namespace SpectraMod.NPCs.Slimes
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
            // Bar
        }
        public override void NPCLoot()
        {
            // Zilla
        }
    }
}
