using Terraria;
using Terraria.ID;


namespace SpectraMod.Items.Materials.VBoss
{
    public class DestroyerMandible : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steel Mandible");
            Tooltip.SetDefault("Mechanical Pieces of the Metallic Devourer");
        }

        public override void SafeSetDefaults()
        {
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.Pink;
        }
    }
}
