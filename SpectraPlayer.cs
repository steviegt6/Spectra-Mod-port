using Microsoft.Xna.Framework.Graphics;
using SpectraMod.Items.Consumables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SpectraMod
{
    public class SpectraPlayer : ModPlayer
    {

        #region ACC_BOOLS
        public bool UnluckyTombEffect;
        public bool GreaterPygmyNecklaceEffect;
        #endregion

        #region MISC_EFFECTS_BOOLS
        public bool AllPassive;
        public bool Hated;
        #endregion

        #region ARMOR_BOOLS
        public bool DirtSetBonus;
        #endregion

        public SpectraEnums.HealthLevel PlayerLifeTier = SpectraEnums.HealthLevel.None;
        public SpectraEnums.ManaLevel PlayerManaTier = SpectraEnums.ManaLevel.None;

        public override void Initialize()
        {
            PlayerLifeTier = SpectraEnums.HealthLevel.None;
            PlayerManaTier = SpectraEnums.ManaLevel.None;
        }

        public override void ResetEffects()
        {
            UnluckyTombEffect = false;
            GreaterPygmyNecklaceEffect = false;
            Hated = false;
            DirtSetBonus = false;

            AllPassive = false;
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            #region ACC_EFFECTS
            if (UnluckyTombEffect)
            {
                player.npcTypeNoAggro[NPCID.DemonEye] = true;
                player.npcTypeNoAggro[NPCID.CataractEye] = true;
                player.npcTypeNoAggro[NPCID.SleepyEye] = true;
                player.npcTypeNoAggro[NPCID.DialatedEye] = true;
                player.npcTypeNoAggro[NPCID.GreenEye] = true;
                player.npcTypeNoAggro[NPCID.PurpleEye] = true;
                player.npcTypeNoAggro[NPCID.DemonEyeOwl] = true;
                player.npcTypeNoAggro[NPCID.DemonEyeSpaceship] = true;

                player.npcTypeNoAggro[NPCID.Zombie] = true;
                player.npcTypeNoAggro[NPCID.ZombieDoctor] = true;
                player.npcTypeNoAggro[NPCID.ZombieElf] = true;
                player.npcTypeNoAggro[NPCID.ZombieElfBeard] = true;
                player.npcTypeNoAggro[NPCID.ZombieElfGirl] = true;
                player.npcTypeNoAggro[NPCID.ZombieEskimo] = true;
                player.npcTypeNoAggro[NPCID.ZombieMushroom] = true;
                player.npcTypeNoAggro[NPCID.ZombieMushroomHat] = true;
                player.npcTypeNoAggro[NPCID.ZombiePixie] = true;
                player.npcTypeNoAggro[NPCID.ZombieRaincoat] = true;
                player.npcTypeNoAggro[NPCID.ZombieSuperman] = true;
                player.npcTypeNoAggro[NPCID.ZombieXmas] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombie] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieCenx] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieEskimo] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombiePincussion] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieSlimed] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieSwamp] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieTwiggy] = true;
                player.npcTypeNoAggro[NPCID.BaldZombie] = true;
            }

            if (GreaterPygmyNecklaceEffect)
            {
                player.maxMinions += 3;
                player.minionKB += 5f;
                player.minionDamage += 0.25f;
            }
            #endregion

            #region MISC_EFFECTS
            if (AllPassive)
            {
                foreach (NPC npc in Main.npc)
                {
                    player.npcTypeNoAggro[npc.type] = true;
                }
            }
            #endregion

            #region POWERUP_STATBOOSTS
            switch (PlayerLifeTier)
            {
                case SpectraEnums.HealthLevel.CursedLife:
                    player.statLifeMax2 += 100;
                    break;
                default:
                    break;
            }

            switch (PlayerManaTier)
            {
                case SpectraEnums.ManaLevel.LavaMana:
                    player.statManaMax2 += 100;
                    break;
                default:
                    break;
            }
            #endregion
        }

        public override void PreUpdate()
        {
            #region POWERUP_STATCHECKS
            if (player.statLifeMax == 400 && PlayerLifeTier < SpectraEnums.HealthLevel.LifeCrystal)
            {
                PlayerLifeTier = SpectraEnums.HealthLevel.LifeCrystal;
            }
            if (player.statLifeMax == 500 && PlayerLifeTier < SpectraEnums.HealthLevel.LifeFruit)
            {
                PlayerLifeTier = SpectraEnums.HealthLevel.LifeFruit;
            }

            if (player.statManaMax == 200 && PlayerManaTier < SpectraEnums.ManaLevel.ManaCrystal)
            {
                PlayerManaTier = SpectraEnums.ManaLevel.ManaCrystal;
            }
            #endregion

            #region POWERUP_RESOURCETEXTURES
            if (Main.netMode != NetmodeID.Server)
            {
                switch (PlayerLifeTier)
                {
                    case SpectraEnums.HealthLevel.CursedLife:
                        //Main.heart2Texture = ModContent.GetTexture("SpectraMod/ResourceTextures/CursedHeart");
                        break;
                    default:
                        break;
                }

                switch (PlayerManaTier)
                {
                    case SpectraEnums.ManaLevel.LavaMana:
                        //Main.manaTexture = ModContent.GetTexture("SpectraMod/ResourceTextures/LavaMana");
                        break;
                    default:
                        break;

                }
            }
            #endregion
        }

        public override void PostUpdateBuffs()
        {
            if (Hated)
            {
                player.moveSpeed -= 0.3f;
                player.maxRunSpeed -= 1.1f;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (Hated)
            {
                player.lifeRegen = 0;
                player.lifeRegenTime = 0;
                player.lifeRegen -= 4;
            }
        }

        public override TagCompound Save()
        {
            //if (PlayerLifeTier > SpectraEnums.HealthLevel.LifeCrystal) Main.heart2Texture = ModContent.GetTexture("SpectraMod/ResourceTextures/Heart2");

            return new TagCompound() {
                { "LifeTier", (int)PlayerLifeTier },
                { "ManaTier", (int)PlayerManaTier }
            };
        }

        public override void Load(TagCompound tag)
        {
            PlayerLifeTier = (SpectraEnums.HealthLevel)tag.GetInt("LifeTier");
            PlayerManaTier = (SpectraEnums.ManaLevel)tag.GetInt("ManaTier");
        }
    }
}