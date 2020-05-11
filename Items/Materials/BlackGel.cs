using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Materials
{
    public class BlackGel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Gel");
            Tooltip.SetDefault("'The darkest gel around'" +
                               "\nNeither tasty nor flammable");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(16, 14);
            item.maxStack = 9999;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = ItemRarityID.Blue;
        }
    }
}
