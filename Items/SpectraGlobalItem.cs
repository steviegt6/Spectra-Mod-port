using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items
{
    public class SpectraGlobalItem : GlobalItem
    {
        public static bool DirtPick;

        public override void HoldItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<Tools.Sets.Dirt.DirtPickaxe>()) DirtPick = true;  
            else DirtPick = false;  
        }
    }
}
