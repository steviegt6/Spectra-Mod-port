using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Materials.VBoss
{
    public class DestroyerMandible : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steel Mandible");
            Tooltip.SetDefault("Mechanical Pieces of the Metallic Devourer");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(16, 40);
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.Pink;
        }
    }
}
