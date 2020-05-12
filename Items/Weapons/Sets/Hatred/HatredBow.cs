using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Weapons.Sets.Hatred
{
    public class HatredBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker Bow");
            Tooltip.SetDefault("Strike with the bow of vengence!");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(20, 40);
            item.value = Item.sellPrice(0, 5, 60, 0);
            item.rare = ItemRarityID.Orange;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.damage = 38;
            item.ranged = true;
            item.knockBack = 9;
            item.useTime = 8;
            item.useAnimation = 8;
            item.autoReuse = true;
            item.UseSound = SoundID.Item5;
            item.ammo = AmmoID.Arrow;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<Projectiles.Ammo.HatredArrowPro>();
            }

            return true;
        }
    }
}
