using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Projectiles.Ammo
{
    public class HatredArrowPro : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vengence Arrow");
        }

        public override void SetDefaults()
        {
            projectile.Size = new Vector2(14, 14);
            projectile.aiStyle = 1;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.friendly = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Hated>(), 180);
        }
    }
}
