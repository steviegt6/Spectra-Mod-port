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
    [AutoloadEquip(EquipType.Head)]
    public class AngerHeadpiece : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderHelmet";

        public override void SetStaticDefaults() => Tooltip.SetDefault("6% increased ranged critical strike chance");

        public override void SafeSetDefaults()
        {
            item.value = (10 *(40 * 100) +  10 *(1 * 100)) * 5;
            item.defense = 5;
            base.SafeSetDefaults();
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 6;
            base.UpdateEquip(player);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<AngerBreastplate>() && legs.type == ModContent.ItemType<AngerGreaves>();

        public override void UpdateArmorSet(Player player)
        {
            SpectraPlayer spectraPlayer = player.GetModPlayer<SpectraPlayer>();

            player.setBonus = "+2 defense" +
                "\n10% chance not to consume ammo" +
                "\n5% increased ranged damage";
            player.statDefense += 2;
            player.rangedDamage *= 1.04f;
            spectraPlayer.AngerSetBonus = true;
            base.UpdateArmorSet(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ModContent.ItemType<HatredBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            base.AddRecipes();
        }
    }
}
