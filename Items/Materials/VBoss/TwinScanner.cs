using Terraria;
using Terraria.ID;

namespace SpectraMod.Items.Materials.VBoss
{
    public class TwinScanner : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Optical Scanner");
            Tooltip.SetDefault("Mechanical pieces of the Two Eternal Watchers");
        }

        public override void SafeSetDefaults()
        {
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.Pink;
        }
    }
}
