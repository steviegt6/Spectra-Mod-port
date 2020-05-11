using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;


namespace SpectraMod.Projectiles.Boss.MageMaster
{
    public class ShadowflareHome : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.Size = new Vector2(16, 16);
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.timeLeft = 150;
        }

        private float Target { get => projectile.ai[0]; set => projectile.ai[0] = value; }

        public override void AI()
        {
            Player target = Main.player[(int)Target];
            Vector2 dir = projectile.DirectionTo(target.Center);
            dir.Normalize();
            if (projectile.Distance(target.Center) > 10f)
            {
                projectile.velocity = dir * 2f;
                projectile.position += projectile.velocity;
            }
            if (Main.rand.NextBool(6)) Dust.NewDust(projectile.Center, 24, 24, DustID.Shadowflame);
        }
    }
}
