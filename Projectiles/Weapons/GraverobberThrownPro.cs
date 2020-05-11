using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Projectiles.Weapons
{
    public class GraverobberThrownPro : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grave Robber's Knife");
        }

        public override void SetDefaults()
        {
            projectile.Size = new Vector2(14, 14);
            projectile.damage = 12;
            projectile.knockBack = 2;
            projectile.thrown = true;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = 2;
        }
    }
}