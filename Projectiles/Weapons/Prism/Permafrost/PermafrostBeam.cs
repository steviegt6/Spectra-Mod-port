using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Projectiles.Weapons.Prism.Permafrost
{
    public class PermafrostBeam : PrismBeam
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.LastPrismLaser;

        protected override float BeamColorHue() => 0.55f;

        protected override float BeamHueVariance() => 0.15f;

        protected override float BeamHitboxCollisionWidth() => 20f;

        protected override float BeamLightBrightness() => 0.80f;

        protected override float MaxBeamScale() => 1.5f;

        protected override float MaxBeamSpread() => 0.8f;

        protected override float MaxDamageMultiplier() => 1.15f;

        protected override PrismHoldout Parent() => new PermafrostHoldout();

        protected override int ParentType() => ModContent.ProjectileType<PermafrostHoldout>();

        protected override float BeamColorLightness() => 0.6f;

        protected override float BeamColorSaturation() => 7.5f;

        protected override int HitCD() => 16;
    }
}
