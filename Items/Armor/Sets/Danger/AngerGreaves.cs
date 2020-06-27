using SpectraMod.Items.Boss.GraveRobber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor.Sets.Danger
{
    [AutoloadEquip(EquipType.Legs)]
    public class AngerGreaves : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderLeggings";

        public override void SetStaticDefaults() => Tooltip.SetDefault("2% increased ranged damage");

        public override void SafeSetDefaults()
        {
            item.value = (8 *(40 * 100) +  8 *(1 * 100)) * 5;
            item.defense = 5;
            base.SafeSetDefaults();
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.02f;
            base.UpdateEquip(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ModContent.ItemType<HatredBar>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            base.AddRecipes();
        }
    }
}
