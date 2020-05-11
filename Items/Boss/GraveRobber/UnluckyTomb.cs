using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Boss.GraveRobber
{
    public class UnluckyTomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unlucky Gravestone");
            Tooltip.SetDefault("'The robber's downfall...'" +
                               "\nMakes some creatures of the night passive");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(45, 45);
            item.value = Item.sellPrice(0, 6, 6, 6);
            item.accessory = true;
            item.rare = ItemRarityID.Green;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SpectraPlayer>().UnluckyTombEffect = true;
        }
    }
}