using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Materials.VBoss
{
    public class SkeletronRib : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Rib");
            Tooltip.SetDefault("Mechanical pieces of the Great Dungeon Robot");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(36, 40);
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.Pink;
        }
    }
}
