using Terraria;
using Terraria.ID;

namespace SpectraMod.Items.Materials.Gel
{
    public class DoomGel : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Gel");
            Tooltip.SetDefault("'The most evil gel around'" +
                               "\nNeither tasty nor flammable");
        }

        public override void SafeSetDefaults()
        {
            item.maxStack = 9999;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Red;
        }
    }
}
