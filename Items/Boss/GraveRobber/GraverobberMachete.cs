using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpectraMod.Buffs.Debuffs;

namespace SpectraMod.Items.Boss.GraveRobber
{
    public class GraverobberMachete : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grave Robber's Machete");
            Tooltip.SetDefault("The robber's weapon");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(30, 30);
            item.value = Item.sellPrice(0, 0, 6, 66);
            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 12;
            item.melee = true;
            item.knockBack = 4;
            item.useTime = 12;
            item.useAnimation = 12;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
        }
    }
}