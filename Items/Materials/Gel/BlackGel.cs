using Terraria;
using Terraria.ID;

namespace SpectraMod.Items.Materials.Gel
{
    public class BlackGel : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Gel");
            Tooltip.SetDefault("'The darkest gel around'" +
                               "\nNeither tasty nor flammable");
        }

        public override void SafeSetDefaults()
        {
            item.maxStack = 9999;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = ItemRarityID.Blue;
        }
    }
}
