using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace SpectraMod.Projectiles.Boss.MageMaster
{
    public class ShadowflareBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.Size = new Vector2(16, 16);
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.penetrate = Main.expertMode ? 4 : 3;
            projectile.hostile = true;
            projectile.friendly = false;
        }

        public override void AI()
        {
            projectile.position += projectile.velocity;
            if (Main.rand.NextBool(6)) Dust.NewDust(projectile.Center, 24, 24, DustID.Shadowflame);
        }
    }
}
