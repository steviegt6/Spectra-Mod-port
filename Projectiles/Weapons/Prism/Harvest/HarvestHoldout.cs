using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Projectiles.Weapons.Prism.Harvest
{
    public class HarvestHoldout : PrismHoldout
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.LastPrism;

        public override float AimResponsiveness() => 0.05f;

        public override float ChargeTime() => 240f;

        public override float DamageStart() => 45f;

        public override float MaxManaConsumptionDelay() => 20f;

        public override float MinManaConsumptionDelay() => 10f;

        public override int NumAnimationFrames() => 5;

        public override int NumBeams() => 3;

        public override int SoundInterval() => 30;

        public override Color DrawColor() => Color.Orange;

        public override int BeamType() => ModContent.ProjectileType<HarvestBeam>();

        public override float DamageMulti() => 1f;
    }
}
