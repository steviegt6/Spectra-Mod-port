using Terraria;
using Terraria.ID;

namespace SpectraMod.Items.Materials.VBoss
{
    public class SkeletronRib : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Rib");
            Tooltip.SetDefault("Mechanical pieces of the Great Dungeon Robot");
        }

        public override void SafeSetDefaults()
        {
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.Pink;
        }
    }
}
