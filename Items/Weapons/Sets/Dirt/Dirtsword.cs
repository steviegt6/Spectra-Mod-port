using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Weapons.Sets.Dirt
{
    public class DirtSword : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Is that it?");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 0, 0, 1);
            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 3;
            item.melee = true;
            item.knockBack = 0;
            item.useTime = 30;
            item.useAnimation = 30;
            item.autoReuse = false;
            item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
