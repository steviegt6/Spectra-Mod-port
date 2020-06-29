using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Projectiles.Weapons.Prism.Harvest
{
    public class HarvestBeam : PrismBeam
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.LastPrismLaser;

        protected override float BeamColorHue() => 0.085f;

        protected override float BeamHueVariance() => 0.25f;

        protected override float BeamHitboxCollisionWidth() => 20f;

        protected override float BeamLightBrightness() => 0.75f;

        protected override float MaxBeamScale() => 1.45f;

        protected override float MaxBeamSpread() => 0.85f;

        protected override float MaxDamageMultiplier() => 1.10f;

        protected override PrismHoldout Parent() => new HarvestHoldout();

        protected override int ParentType() => ModContent.ProjectileType<HarvestHoldout>();

        protected override float BeamColorLightness() => 0.5f;

        protected override float BeamColorSaturation() => 7f;

        protected override int HitCD() => 20;
    }
}
