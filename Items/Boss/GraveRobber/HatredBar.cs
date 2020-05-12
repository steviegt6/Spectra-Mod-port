using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Boss.GraveRobber
{
    public class HatredBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bar of Hatred");
            Tooltip.SetDefault("The bar of hate");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(30, 24);
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = ItemRarityID.Blue;
            item.maxStack = 999;
        }
    }
}