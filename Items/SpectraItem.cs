using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items
{
    public class SpectraItem : GlobalItem
    {
        public static bool dirtPick;
        public override void HoldItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<Tools.Sets.Dirt.DirtPickaxe>())
                dirtPick = true;
            else
                dirtPick = false;
        }
    }
}
