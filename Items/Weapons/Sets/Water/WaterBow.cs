using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace SpectraMod.Items.Weapons.Sets.Water
{
    public class WaterBow : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Bow");
            Tooltip.SetDefault("Wooden arrows turn into water arrows");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 15;
            item.crit = 4;
            item.maxStack = 1;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2f;
            item.value = (1 * 100 + (15)) * 5;
            item.rare = ItemRarityID.White;
#pragma warning disable ChangeMagicNumberToID // Change magic numbers into appropriate ID values
            item.shoot = 10;
#pragma warning restore ChangeMagicNumberToID // Change magic numbers into appropriate ID values
            item.shootSpeed = 14f;
            item.useAmmo = AmmoID.Arrow;
        }
    }
}
