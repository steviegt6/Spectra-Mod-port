using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Accessories
{
    public class GreaterPygmyNecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A better necklace?'" +
                               "\n+3 Max minions" +
                               "\n+25% minion damage" +
                               "\nIncreases minion knockback");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(40, 40);
            item.value = Item.sellPrice(0, 6, 6, 6);
            item.accessory = true;
            item.rare = ItemRarityID.Lime;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SpectraPlayer>().GreaterPygmyNecklaceEffect = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PygmyNecklace);
            recipe.AddIngredient(ItemID.PapyrusScarab);
            recipe.AddIngredient(ModContent.ItemType<Materials.Bars.SurviveBar>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}