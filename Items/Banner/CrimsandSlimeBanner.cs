using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Banner
{
    public class CrimsandSlimeBanner : SpectraItem
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
			item.rare = ItemRarityID.Blue;
			item.value = Item.buyPrice(0, 0, 10, 0);
			item.createTile = ModContent.TileType<Tiles.MonsterBanner>();
			item.placeStyle = 1;
		}
	}
}
