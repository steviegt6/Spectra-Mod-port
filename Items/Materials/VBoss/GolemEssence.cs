using Terraria;
using Terraria.ID;

namespace SpectraMod.Items.Materials.VBoss
{
    public class GolemEssence : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Energy");
            Tooltip.SetDefault("Essence of something ancient");
        }

        public override void SafeSetDefaults()
        {
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = ItemRarityID.Lime;
        }
    }
}
