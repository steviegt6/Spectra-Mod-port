using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Materials.Bars
{
    public class DelightedBar : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bar of Delight");
            Tooltip.SetDefault("'Condensed happiness'");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.White;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddIngredient(ItemID.Daybloom, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
