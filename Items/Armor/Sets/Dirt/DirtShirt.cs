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
    [AutoloadEquip(EquipType.Body)]
    public class DirtShirt : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderBreastplate";

        public override void SafeSetDefaults()
        {
            item.value = 15 * 5;
            item.defense = 1;
            base.SafeSetDefaults();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 15);
            recipe.SetResult(this);
            recipe.AddRecipe();
            base.AddRecipes();
        }
    }
}
