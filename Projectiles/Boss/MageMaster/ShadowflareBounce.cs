using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;


namespace SpectraMod.Projectiles.Boss.MageMaster
{
    public class ShadowflareBounce : ModProjectile
    {
        public override string Texture => "SpectraMod/Projectiles/Boss/MageMaster/ShadowflareBolt";

        public override void SetDefaults()
        {
            projectile.Size = new Vector2(16, 16);
            projectile.aiStyle = -1;
            projectile.penetrate = Main.expertMode ? 8 : 6;
            projectile.hostile = true;
            projectile.friendly = false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Item10, projectile.position);
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(6)) Dust.NewDust(projectile.Center, 24, 24, DustID.Shadowflame);
        }
    }
}
