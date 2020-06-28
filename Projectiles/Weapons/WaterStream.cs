using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Projectiles.Weapons
{
    public class WaterStream : ModProjectile
    {
        public bool collidedWithTile = false;
        public bool lockedOn = false;
        public override string Texture => "Terraria/Projectile_" + ProjectileID.WaterStream;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Stream");
            ProjectileID.Sets.Homing[projectile.type] = true;
            Main.projFrames[projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            projectile.width = projectile.height = 18;
            projectile.damage = 7;
            projectile.melee = true;
            projectile.timeLeft = 5 * 60;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            aiType = -1;
            projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 3;
            projectile.knockBack = 1f;
        }

        public override void AI()
        {
            int amount = 0;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == projectile.type)
                {
                    amount++;
                }
            }
            if (amount > 2)
                projectile.Kill();
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.friendly && npc.type != NPCID.TargetDummy)
                {
                    //float shootToX = npc.Center.X - projectile.Center.X;
                    //float shootToY = npc.Center.Y - projectile.Center.Y;
                    Vector2 shootTo = projectile.DirectionTo(npc.Center);
                    float distance = shootTo.Length();

                    if (distance < 300f && !npc.friendly && npc.active)
                    {
                        projectile.ai[1] = 0;
                        lockedOn = true;
                        distance = 3f / distance;

                        shootTo *= distance;

                        //Vector2 shootTo = new Vector2(shootToX, shootToY);
                        Vector2 npcDir = Vector2.Normalize(shootTo);
                        Vector2 originalDir = Vector2.Normalize(projectile.velocity);
                        Vector2 homingDir = (npcDir + originalDir * 7) / 8;
                        projectile.velocity = homingDir * shootTo.Length();
                    }
                    else
                    {
                        lockedOn = false;
                    }
                }
            }

            projectile.ai[0] += 1f;

            if (projectile.ai[0] > 3f)
            {
                projectile.velocity.Y += 0.1f;
                for (int num611 = 0; num611 < 1; num611++)
                {
                    for (int num612 = 0; num612 < 3; num612++)
                    {
                        float num619 = projectile.velocity.X / 3f * (float)num612;
                        float num620 = projectile.velocity.Y / 3f * (float)num612;
                        int num621 = 6;
                        int num622 = Dust.NewDust(new Vector2(projectile.position.X + (float)num621, projectile.position.Y + (float)num621), projectile.width - num621 * 2, projectile.height - num621 * 2, 172, 0f, 0f, 100, default, 1.2f);
                        Main.dust[num622].noGravity = true;
                        Dust dust136 = Main.dust[num622];
                        Dust dust304 = dust136;
                        dust304.velocity *= 0.3f;
                        dust136 = Main.dust[num622];
                        dust304 = dust136;
                        dust304.velocity += projectile.velocity * 0.5f;
                        Main.dust[num622].position.X -= num619;
                        Main.dust[num622].position.Y -= num620;
                    }
                    if (Main.rand.Next(8) == 0)
                    {
                        int num615 = 6;
                        int num616 = Dust.NewDust(new Vector2(projectile.position.X + (float)num615, projectile.position.Y + (float)num615), projectile.width - num615 * 2, projectile.height - num615 * 2, 172, 0f, 0f, 100, default, 0.75f);
                        Dust dust134 = Main.dust[num616];
                        Dust dust304 = dust134;
                        dust304.velocity *= 0.5f;
                        dust134 = Main.dust[num616];
                        dust304 = dust134;
                        dust304.velocity += projectile.velocity * 0.5f;
                    }
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }

            if (!lockedOn || collidedWithTile)
            {
                projectile.ai[1] += 1f;
            }

            if (projectile.ai[1] >= 60f)
            {
                projectile.Kill();
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            collidedWithTile = true;
            return base.OnTileCollide(oldVelocity);
        }
    }
}
