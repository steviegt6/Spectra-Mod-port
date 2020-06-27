using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpectraMod.Items.Currency;
using SpectraMod.Items.Tools.Sets.Dirt;
using SpectraMod.Items.Weapons.Sets.Dirt;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items
{
    public class SpectraGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public static bool DirtPick;

        public override void HoldItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<Tools.Sets.Dirt.DirtPickaxe>()) DirtPick = true;  
            else DirtPick = false;  
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            SpectraPlayer spectraPlayer = player.GetModPlayer<SpectraPlayer>();

            if (item.type == ModContent.ItemType<DirtSword>() || item.type == ModContent.ItemType<DirtPickaxe>() && spectraPlayer.DirtSetBonus)
                mult = 2;
            base.ModifyWeaponDamage(item, player, ref add, ref mult, ref flat);
        }

        public override bool ConsumeAmmo(Item item, Player player)
        {
            SpectraPlayer spectraPlayer = player.GetModPlayer<SpectraPlayer>();

            if (spectraPlayer.AngerSetBonus && Main.rand.NextBool(10))
                return false;
            return base.ConsumeAmmo(item, player);
        }
    }
}
