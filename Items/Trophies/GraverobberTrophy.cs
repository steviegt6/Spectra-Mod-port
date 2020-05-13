using Terraria;
using Terraria.ModLoader;

namespace SpectraMod.Items.Trophies
{
    public class GraverobberTrophy : SpectraItem
    {
        public override void SafeSetDefaults()
        {
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 1;
            item.createTile = ModContent.TileType<Tiles.BossTrophy>();
            item.placeStyle = 0;
        }
    }
}
