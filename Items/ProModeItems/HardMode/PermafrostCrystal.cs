using Terraria;
using Terraria.ID;
using SpectraMod.Projectiles.Weapons.Prism.Permafrost;
using static Terraria.ModLoader.ModContent;

namespace SpectraMod.Items.ProModeItems.HardMode
{
    public class PermafrostCrystal : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fire a beam of festive energy");
        }

        public override void SafeSetDefaults()
        {
            professional = true;

            item.CloneDefaults(ItemID.LastPrism);
            item.magic = true;
            item.mana = 14;
            item.damage = 70;
            item.crit = 3;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 0;
            item.shoot = ProjectileType<PermafrostHoldout>();
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ProjectileType<PermafrostHoldout>()] <= 0;
    }
}
