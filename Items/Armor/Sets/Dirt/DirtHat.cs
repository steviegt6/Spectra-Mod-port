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
    [AutoloadEquip(EquipType.Head)]
    public class DirtHat : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderHelmet";

        public override void SafeSetDefaults()
        {
            item.value = 10 * 5;
            item.defense = 0;
            base.SafeSetDefaults();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<DirtShirt>() && legs.type == ModContent.ItemType<DirtBoots>();

        public override void UpdateArmorSet(Player player)
        {
            SpectraPlayer spectraPlayer = player.GetModPlayer<SpectraPlayer>();

            player.setBonus = "Dirt weapons and tools deal twice the damage";
            spectraPlayer.DirtSetBonus = true;
            base.UpdateArmorSet(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
            base.AddRecipes();
        }
    }
}
