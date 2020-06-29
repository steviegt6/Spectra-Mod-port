using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Projectiles.Weapons.Prism.Death
{
    public class DeathBeam : PrismBeam
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.LastPrismLaser;

        protected override float BeamColorHue()
        {
            Main.NewText(Main.player[projectile.owner].name);
            switch (Main.player[projectile.owner].name)
            {
                case "Oreo":
                    Main.NewText("o");
                    return 0.7f;
                default:
                    return 1f;
            }
        }

        protected override float BeamColorLightness() => 0.25f;

        protected override float BeamColorSaturation() => 0.7f;

        protected override float BeamHitboxCollisionWidth() => 25f;

        protected override float BeamHueVariance() => 0.01f;

        protected override float BeamLightBrightness() => 2f;

        protected override int HitCD() => 0;

        protected override float MaxBeamScale() => 2f;

        protected override float MaxBeamSpread() => 1.5f;

        protected override float MaxDamageMultiplier() => 50f;

        protected override PrismHoldout Parent() => new DeathHoldout();

        protected override int ParentType() => ModContent.ProjectileType<DeathHoldout>();

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }

        public override float PerformBeamHitscan(Projectile prism, bool fullCharge)
        {
            return 2400f;
        }
    }
}
