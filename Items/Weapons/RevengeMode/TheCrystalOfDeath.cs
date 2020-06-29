using Terraria;
using Terraria.ID;
using SpectraMod.Projectiles.Weapons.Prism.Death;
using static Terraria.ModLoader.ModContent;

namespace SpectraMod.Items.Weapons.RevengeMode
{
    public class TheCrystalOfDeath : SpectraItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.LastPrism;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fire a beam of absolute dispair and destruction");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SafeSetDefaults()
        {
            professional = true;

            item.CloneDefaults(ItemID.LastPrism);
            item.magic = true;
            item.mana = 100;
            item.damage = 9999;
            item.crit = 9999;
            item.useTime = 1;
            item.useAnimation = 1;
            item.knockBack = 0;
            item.shoot = ProjectileType<DeathHoldout>();
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ProjectileType<DeathHoldout>()] <= 0;
    }
}
