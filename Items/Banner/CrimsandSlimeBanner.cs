using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Banner
{
    public class CrimsandSlimeBanner : ModItem
    {
		public override void SetDefaults()
		{
			item.Size = new Vector2(10, 24);
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
