using Microsoft.Xna.Framework;
using System.Runtime.ExceptionServices;
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

        /// <summary>
        /// Attempts to drop a slime staff with the given chance
        /// </summary>
        /// <param name="npc">The npc dropping it</param>
        /// <param name="chance">The default chance</param>
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

        /// <summary>
        /// Simply drops an item
        /// </summary>
        /// <typeparam name="Loot">The ModItem you wish to drop. Can be set to anything if you're using a vanilla item</typeparam>
        /// <param name="npc">The npc that dropping it</param>
        /// <param name="chance">The chance</param>
        /// <param name="expertMode">The multiplier for being in expert mode</param>
        /// <param name="vanillaItem">The ID of the vanilla item</param>
        /// <param name="lowerRange">The least of the item that can drop</param>
        /// <param name="upperRange">The most of the item that can drop</param>
        /// <returns>The Main.Item[] index of the Item, null if something went horribly wrong</returns>
        public static int? SimpleItemDrop<Loot>(NPC npc, int chance, float expertMode, int? vanillaItem = null, int? lowerRange = null) where Loot : ModItem
        {
            float dropChance = chance * expertMode;

            if (vanillaItem == null)
            {
                if (Main.rand.NextBool((int)dropChance)) return Item.NewItem(npc.getRect(), ModContent.ItemType<Loot>());
            }
            else
            {
                if (Main.rand.NextBool((int)dropChance)) return Item.NewItem(npc.getRect(), chance);
            }

            return null;
        }

        // TODO: make this work lol
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
