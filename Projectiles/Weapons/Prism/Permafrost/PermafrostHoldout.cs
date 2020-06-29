using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace SpectraMod.Projectiles.Weapons.Prism.Permafrost
{
    public class PermafrostHoldout : PrismHoldout
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.LastPrism;

        public override float AimResponsiveness() => 0.075f;

        public override int BeamType() => ModContent.ProjectileType<PermafrostBeam>();

        public override float ChargeTime() => 220f;

        public override float DamageStart() => 45f;

        public override Color DrawColor() => Color.DeepSkyBlue;

        public override float MaxManaConsumptionDelay() => 18f;

        public override float MinManaConsumptionDelay() => 9f;

        public override int NumAnimationFrames() => 5;

        public override int NumBeams() => 3;

        public override int SoundInterval() => 60;

        public override float DamageMulti() => 1f;
    }
}
