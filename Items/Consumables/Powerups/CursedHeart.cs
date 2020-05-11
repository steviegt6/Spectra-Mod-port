using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpectraMod.Items.Materials;

namespace SpectraMod.Items.Consumables.Powerups
{
    public class CursedHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed life Crystal");
            Tooltip.SetDefault("'It beats with a malevolent force" +
                               "\nPermanently increases max life by 100");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(22, 22);
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = ItemRarityID.LightPurple;
            item.UseSound = SoundID.Item29;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 30;
            item.useAnimation = 30;
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SpectraPlayer>().PlayerLifeTier == SpectraEnums.HealthLevel.LifeFruit;
        }

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<SpectraPlayer>().PlayerLifeTier = SpectraEnums.HealthLevel.CursedLife;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.LifeFruit);
            recipe.AddIngredient(ModContent.ItemType<BlackGel>(), 100);
            recipe.AddIngredient(ItemID.Moonglow);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe.AddIngredient(ItemID.SpectreBar, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}