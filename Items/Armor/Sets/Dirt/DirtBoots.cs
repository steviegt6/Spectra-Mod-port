using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor.Sets.Dirt
{
    [AutoloadEquip(EquipType.Legs)]
    public class DirtBoots : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderLeggings";

        public override void SafeSetDefaults()
        {
            item.value = 5 * 5;
            item.defense = 0;
            base.SafeSetDefaults();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
            base.AddRecipes();
        }
    }
}
