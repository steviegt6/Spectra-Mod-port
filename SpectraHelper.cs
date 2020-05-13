using Microsoft.Xna.Framework;
using SpectraMod.Items.Banner;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod
{
    public class SpectraHelper
    {
        public static int[] Pool_ZombiesNormal = { 
            NPCID.Zombie, NPCID.SmallZombie, NPCID.BigZombie, NPCID.FemaleZombie, 
            NPCID.BigFemaleZombie, NPCID.SmallFemaleZombie, NPCID.TwiggyZombie, 
            NPCID.BigTwiggyZombie, NPCID.SmallTwiggyZombie, NPCID.SwampZombie,
            NPCID.BigSwampZombie, NPCID.SmallSwampZombie, NPCID.SlimedZombie,
            NPCID.BigSlimedZombie, NPCID.SmallSlimedZombie, NPCID.PincushionZombie,
            NPCID.BigPincushionZombie, NPCID.SmallPincushionZombie, NPCID.BaldZombie,
            NPCID.BigBaldZombie, NPCID.SmallBaldZombie
        };
        public static int[] Pool_ZombiesExpert = {
            NPCID.Zombie, NPCID.ArmedZombie, NPCID.BigZombie, 
            NPCID.FemaleZombie, NPCID.BigFemaleZombie, NPCID.ArmedZombieCenx, 
            NPCID.TwiggyZombie, NPCID.BigTwiggyZombie, NPCID.ArmedZombieTwiggy, 
            NPCID.SwampZombie, NPCID.BigSwampZombie, NPCID.ArmedZombieSwamp,
            NPCID.SlimedZombie, NPCID.BigSlimedZombie, NPCID.ArmedZombieSlimed, 
            NPCID.PincushionZombie, NPCID.BigPincushionZombie, NPCID.ArmedZombiePincussion,
            NPCID.BaldZombie, NPCID.BigBaldZombie, NPCID.ArmedZombiePincussion
        };

        public static void AttemptSlimeStaff(NPC npc, int chance)
        {
            if (Main.expertMode)
            {
                chance = (int)(chance * 0.7);
            }
            if (Main.rand.Next(chance) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.SlimeStaff);
            }
        }

        // TODO: make this world lol
        public static void ProjSpread(Vector2 position, Vector2 velocity, float speed, int amount, float degrees, int type, int damage, float knockback, int owner, int ai0, int ai1)
        {
            Main.NewText("E");

            float rotation = MathHelper.ToRadians(45);
            position += Vector2.Normalize(velocity) * 45f;
            for (int i = 0; i < amount; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / amount)) * 2f;
                Projectile.NewProjectile(position, perturbedSpeed, type, damage, knockback, owner, ai0, ai1);
            }
        }

        public static void ProjInCircle(Vector2 position, Vector2 velocity, float speed, int amount, int type, int damage, float knockback, int owner, int ai0, int ai1)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector2 velocityRotated = velocity.RotatedBy(MathHelper.ToRadians((360 / amount) * i));
                Projectile.NewProjectile(position, velocityRotated, type, damage, knockback, owner, ai0, ai1);
            }
        }
    }
}
