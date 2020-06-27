using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Tools.Sets.Dirt
{
    public class DirtPickaxe : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dirt Pickaxe");
            Tooltip.SetDefault("That's not a pickaxe!");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 0, 0, 1);
            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 2;
            item.melee = true;
            item.knockBack = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.pick = 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
