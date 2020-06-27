using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace SpectraMod.Items.Weapons.Sets.Water
{
    public class WaterSword : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Sword");
            Tooltip.SetDefault("A sword forged out of water");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 0, 25, 0);
            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 14;
            item.melee = true;
            item.knockBack = 0;
            item.useTime = 33;
            item.useAnimation = 33;
            item.autoReuse = false;
            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileType<Projectiles.Weapons.WaterStream>();
            item.shootSpeed = 8f;
            item.scale = 0.8f;
            item.knockBack = 3f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            damage = 7;
            knockBack = 1f;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
