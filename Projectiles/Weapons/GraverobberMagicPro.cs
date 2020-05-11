using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace SpectraMod.Projectiles.Weapons
{ 
    public class GraverobberMagicPro : ModProjectile
    {
        public override string Texture => "SpectraMod/Projectiles/Blank";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grave Robber's Curse");
        }

        public override void SetDefaults()
        {
            projectile.Size = new Vector2(18, 18);
            projectile.damage = 12;
            projectile.knockBack = 2;
            projectile.magic = true;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.maxPenetrate = 1;
            projectile.timeLeft = 300;
        }

        public override bool PreAI()
        {
            if (Main.rand.NextBool(3)) Dust.NewDust(projectile.Center, 19, 19, DustID.Blood);
            Lighting.AddLight(projectile.Center, new Vector3(0.1f, 0.1f, 0.1f));
            return true;
        }
    }
}