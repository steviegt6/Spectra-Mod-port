using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Materials.Bars
{
    public class SurviveBar : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Survivor's Bar");
            Tooltip.SetDefault("'The lost bar of the lost surivor'");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 1, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddIngredient(ModContent.ItemType<DelightedBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Boss.GraveRobber.HatredBar>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
