using Microsoft.Xna.Framework;
using SpectraMod.Projectiles.Weapons;
using Terraria;
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
            Tooltip.SetDefault("Wooden arrows turn into water");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 15;
            item.crit = 4;
            item.maxStack = 1;
            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3.5f;
            item.value = (1 * 100 + (18)) * 5;
            item.rare = ItemRarityID.White;
#pragma warning disable ChangeMagicNumberToID // Change magic numbers into appropriate ID values
            item.shoot = 10;
#pragma warning restore ChangeMagicNumberToID // Change magic numbers into appropriate ID values
            item.shootSpeed = 9f;
            item.useAmmo = AmmoID.Arrow;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ProjectileType<WaterStream2>();
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
