using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SpectraMod.Tiles
{
    public class BossTrophy : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            dustType = 7;
            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Trophy");
            AddMapEntry(new Color(120, 85, 60), name);

            TileID.Sets.FramesOnKillWall[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int style = frameX / 54;
            string item = "";
            switch (style)
            {
                case 0:
                    item = "GraverobberTrophy";
                    break;
                default:
                    break;
            }
            Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType(item));
        }
    }
}
