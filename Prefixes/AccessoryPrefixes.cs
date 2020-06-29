using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace SpectraMod.Prefixes
{
    public class AccessoryPrefixes : ModPrefix
    {
        private byte defense;
        private byte damage;
        private byte damageReduction;
        private byte movementSpeed;
        private byte critChance;
        private byte meleeSpeed;
        private byte mana;
        private byte manaCost;
        private byte minionKnockBack;
        private byte ammoConsumptionChance;
        private byte minionDamage;
        public override float RollChance(Item item) => NPC.downedMoonlord ? 1f : 0f;

        public override bool CanRoll(Item item) => true;

        public override PrefixCategory Category => PrefixCategory.Accessory;

        public AccessoryPrefixes()
        {

        }

        public AccessoryPrefixes(byte defense, byte damage, byte damageReduction, byte movementSpeed, byte critChance, byte meleeSpeed, byte mana, byte manaCost, byte minionKnockBack, byte ammoConsumptionChance, byte minionDamage)
        {
            this.defense = defense;
            this.damage = damage;
            this.damageReduction = damageReduction;
            this.movementSpeed = movementSpeed;
            this.critChance = critChance;
            this.meleeSpeed = meleeSpeed;
            this.mana = mana;
            this.manaCost = manaCost;
            this.minionKnockBack = minionKnockBack;
            this.ammoConsumptionChance = ammoConsumptionChance;
            this.minionDamage = minionDamage;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
                return false;

            mod.AddPrefix("Shielded", new AccessoryPrefixes(5, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0));
            mod.AddPrefix("Modified", new AccessoryPrefixes(2, 2, 0, 2, 2, 2, 5, 0, 0, 0, 0));
            mod.AddPrefix("Piercing", new AccessoryPrefixes(0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            mod.AddPrefix("Windy", new AccessoryPrefixes(0, 0, 0, 5, 0, 5, 0, 0, 0, 0, 0));
            mod.AddPrefix("Magical", new AccessoryPrefixes(0, 0, 0, 0, 0, 0, 15, 4, 0, 0, 0));
            mod.AddPrefix("Ethereal", new AccessoryPrefixes(0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 8));
            mod.AddPrefix("Resourceful", new AccessoryPrefixes(0, 0, 0, 0, 0, 0, 0, 2, 0, 4, 0));
            return false;
        }

        public override void ModifyValue(ref float valueMult)
        {
            float multiplier = 1f + 0.6f;

            valueMult *= multiplier;
            base.ModifyValue(ref valueMult);
        }

        public override void Apply(Item item)
        {
            PrefixGlobalItem prefixItem = item.GetGlobalItem<PrefixGlobalItem>();

            prefixItem.defense = defense;
            prefixItem.damage = damage;
            prefixItem.damageReduction = damageReduction;
            prefixItem.movementSpeed = movementSpeed;
            prefixItem.critChance = critChance;
            prefixItem.meleeSpeed = meleeSpeed;
            prefixItem.mana = mana;
            prefixItem.manaCost = manaCost;
            prefixItem.minionKnockBack = minionKnockBack;
            prefixItem.ammoConsumptionChance = ammoConsumptionChance;
            prefixItem.minionDamage = minionDamage;

            base.Apply(item);
        }
    }

    public class PrefixGlobalItem : GlobalItem
    {
        public int defense;
        public int damage;
        public int damageReduction;
        public int movementSpeed;
        public int critChance;
        public int meleeSpeed;
        public int mana;
        public int manaCost;
        public int minionKnockBack;
        public int ammoConsumptionChance;
        public int minionDamage;

        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            PrefixGlobalItem clone = (PrefixGlobalItem)base.Clone(item, itemClone);

            clone.defense = defense;
            clone.damage = damage;
            clone.damageReduction = damageReduction;
            clone.movementSpeed = movementSpeed;
            clone.critChance = critChance;
            clone.meleeSpeed = meleeSpeed;
            clone.mana = mana;
            clone.manaCost = manaCost;
            clone.minionKnockBack = minionKnockBack;
            clone.ammoConsumptionChance = ammoConsumptionChance;
            clone.minionDamage = minionDamage;

            return clone;
        }

        public override bool NewPreReforge(Item item)
        {
            defense = 0;
            damage = 0;
            damageReduction = 0;
            movementSpeed = 0;
            critChance = 0;
            meleeSpeed = 0;
            mana = 0;
            manaCost = 0;
            minionKnockBack = 0;
            ammoConsumptionChance = 0;
            minionDamage = 0;

            return base.NewPreReforge(item);
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (item.prefix > 0)
            {
                player.statDefense += defense;
                player.allDamage += damage * 0.01f;
                player.endurance += damageReduction * 0.01f;
                player.moveSpeed += movementSpeed * 0.01f;
                player.magicCrit += critChance;
                player.meleeCrit += critChance;
                player.rangedCrit += critChance;
                player.thrownCrit += critChance;
                player.meleeSpeed += meleeSpeed * 0.01f;
                player.statManaMax2 += mana;
                player.manaCost *= 1f - (manaCost * 0.01f);
                player.minionKB += minionKnockBack * 0.01f;
                player.minionDamage += minionDamage * 0.01f;
            }
            base.UpdateEquip(item, player);
        }

        public override bool ConsumeAmmo(Item item, Player player)
        {
            if (item.prefix > 0)
                if (ammoConsumptionChance > 0)
                    if (Main.rand.Next(100) <= ammoConsumptionChance)
                        return false;
            return base.ConsumeAmmo(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (defense > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccDefense", "+" + defense + " defense");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (damage > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccDamage", "+" + damage + "% damage");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (damageReduction > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccDamageReduction", "+" + damageReduction + "% damage reduction");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (movementSpeed > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccMoveSpeed", "+" + movementSpeed + "% increased movement speed");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (critChance > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccCritChance", "+" + critChance + "% critical strike chance");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (meleeSpeed > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccMeleeSpeed", "+" + meleeSpeed + "% increased melee speed");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (mana > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccMaxMana", "+" + mana + " maximum mana");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (manaCost > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccUseMana", "-" + manaCost + "% mana cost");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (minionKnockBack > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccKnockback", "+" + minionKnockBack + "% minion knockback");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (ammoConsumptionChance > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccConsumeAmmo", "+" + ammoConsumptionChance + "% chance not to consume ammo");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (minionDamage > 0)
            {
                TooltipLine line = new TooltipLine(mod, "PrefixAccMinionDamage", "+" + minionDamage + "% minion damage");
                line.isModifier = true;
                tooltips.Add(line);
            }

            base.ModifyTooltips(item, tooltips);
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(defense);
            writer.Write(damage);
            writer.Write(damageReduction);
            writer.Write(movementSpeed);
            writer.Write(critChance);
            writer.Write(meleeSpeed);
            writer.Write(mana);
            writer.Write(manaCost);
            writer.Write(minionKnockBack);
            writer.Write(ammoConsumptionChance);
            writer.Write(minionDamage);
            base.NetSend(item, writer);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            defense = reader.ReadInt32();
            damage = reader.ReadInt32();
            damageReduction = reader.ReadInt32();
            movementSpeed = reader.ReadInt32();
            critChance = reader.ReadInt32();
            meleeSpeed = reader.ReadInt32();
            mana = reader.ReadInt32();
            manaCost = reader.ReadInt32();
            minionKnockBack = reader.ReadInt32();
            ammoConsumptionChance = reader.ReadInt32();
            minionDamage = reader.ReadInt32();

            base.NetReceive(item, reader);
        }
    }
}
