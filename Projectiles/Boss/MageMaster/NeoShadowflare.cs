using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace SpectraMod.Projectiles.Boss.MageMaster
{
    public class NeoShadowflare : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.Size = new Vector2(32, 80);
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.penetrate = Main.expertMode ? 4 : 3;
            projectile.hostile = true;
            projectile.friendly = false;
        }

        public override void AI()
        {
            projectile.position += projectile.velocity;
            Dust.NewDust(projectile.TopRight, 4, 64, DustID.Shadowflame);
        }
    }
}
