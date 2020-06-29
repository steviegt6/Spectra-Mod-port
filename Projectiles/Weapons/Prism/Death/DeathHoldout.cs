using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace SpectraMod.Projectiles.Weapons.Prism.Death
{
    public class DeathHoldout : PrismHoldout
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.LastPrism;

        public override float AimResponsiveness() => 1f;

        public override int BeamType() => ModContent.ProjectileType<DeathBeam>();

        public override float ChargeTime() => 75;

        public override float DamageMulti() => 50f;

        public override float DamageStart() => 5f;

        public override Color DrawColor() => new Color(50, 50, 50);

        public override float MaxManaConsumptionDelay() => 10;

        public override float MinManaConsumptionDelay() => 5;

        public override int NumAnimationFrames() => 5;

        public override int NumBeams() => 13;

        public override int SoundInterval() => 15;
    }
}
