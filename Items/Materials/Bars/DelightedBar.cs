using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Materials.Bars
{
    public class DelightedBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bar of Delight");
            Tooltip.SetDefault("'Condensed happiness'");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(30, 24);
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
