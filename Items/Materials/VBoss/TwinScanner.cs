using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Materials.VBoss
{
    public class TwinScanner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Optical Scanner");
            Tooltip.SetDefault("Mechanical pieces of the Two Eternal Watchers");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(40, 34);
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.Pink;
        }
    }
}
