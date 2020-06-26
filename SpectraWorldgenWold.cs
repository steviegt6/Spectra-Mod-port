using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using static Terraria.ModLoader.ModContent;

namespace SpectraMod
{
    public class SpectraWorldgenWold : ModWorld
    {
        /* public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int LivingTreeIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
            if (LivingTreeIndex != -1)
            {
                tasks.Insert(LivingTreeIndex + 1, new PassLegacy("Spectra:AnceintHouse", delegate (GenerationProgress progress) {
                    progress.Message = "Building ancient houses";
                    GenAncientHouse();
                }));
            }
        }*/

        public override void PostWorldGen()
        {
            GenAncientHouse();

            int[] itemsToPlaceInWaterChests = { ItemType<Items.Weapons.Sets.Water.WaterSword>(), ItemID.BreathingReed, ItemType<Items.Weapons.Sets.Water.WaterBow>() }; //breathing reed is common and pretty useless, easiest to "replace"
            int itemsToPlaceInWaterChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 17 * 36)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.BreathingReed)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInWaterChests[itemsToPlaceInWaterChestsChoice]);
                            itemsToPlaceInWaterChestsChoice = (itemsToPlaceInWaterChestsChoice + 1) % itemsToPlaceInWaterChests.Length;
                            break;
                        }
                    }
                }
            }
        }

        private void GenAncientHouse()
        {
            float widthScale = Main.maxTilesX / 4200f;
            int numberToGenerate = WorldGen.genRand.Next(1, (int)(2f * widthScale));

            for (int k = 0; k < numberToGenerate; k++)
            {
                bool success = false;
                int attempts = 0;
                while (!success)
                {
                    attempts++;
                    if (attempts > 1000) success = true;
                }

                int i = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
                if (i <= Main.maxTilesX / 2 - 50 || i >= Main.maxTilesX / 2 - 50)
                {
                    int j = 0;
                    while (!Main.tile[i, j].active() && (double)j < Main.worldSurface) j++;
                    if (Main.tile[i, j].type == TileID.Dirt)
                    {
                        j--;
                        if (j > 150)
                        {
                            bool okPlacement = true;
                            for (int l = i - 4; l > i + 4; l++)
                            {
                                for (int m = j - 6; m < j + 20; m++)
                                {
                                    if (Main.tile[l, m].active())
                                    {
                                        int type = Main.tile[l, m].type;
                                        if ((type == TileID.BlueDungeonBrick) || (type == TileID.GreenDungeonBrick) || (type == TileID.PinkDungeonBrick) || (type == TileID.Cloud) || (type == TileID.RainCloud)) okPlacement = false;
                                    }
                                }
                            }
                            if (okPlacement) success = PlaceAncientHouse(i, j);
                        }
                    }
                }
            }
        }

        private const int _ = 0;    // Air
        private const int W = 1;    // Wood
        private const int H = 2;    // Wooden platform
        private const int S = 3;    // Stairs
        private const int F = 4;    // Flower pot
        private const int I = 5;    // Lower Door
        private const int i = 6;    // Upper Door
        private static int[,] AncientHouseBlocks = {
            { _,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_ },
            { _,W,W,W,W,_,_,W,W,W,W,W,W,W,W,W,_,W,W,W,W,W,_,W,W,_ },
            { _,W,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_ },
            { _,W,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { _,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_ },
            { _,W,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { _,W,S,H,H,H,W,W,W,W,W,W,_,W,_,W,W,W,W,W,H,W,W,W,W,_ },
            { _,W,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_ },
            { _,_,_,S,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { _,W,_,_,S,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { _,W,H,_,_,S,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,H,H,H,W,_ },
            { _,W,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { _,W,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_ },
            { _,W,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { _,W,H,H,H,W,W,W,H,S,W,W,W,W,_,W,W,_,W,W,W,W,_,W,_,_ },
            { _,W,S,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { _,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { F,_,_,H,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_ },
            { H,W,_,_,H,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { _,W,_,_,_,S,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,W,_ },
            { _,i,_,_,_,_,_,_,S,_,_,_,_,_,_,_,_,_,_,_,_,_,_,_,i,_ },
            { _,I,_,_,_,_,_,_,_,S,_,_,_,_,_,_,_,_,_,_,_,_,_,_,I,_ },
            { _,W,W,W,W,W,W,W,W,S,W,H,W,W,W,_,W,W,W,_,H,W,W,W,W,_ }
        };

        public bool PlaceAncientHouse(int i, int j)
        {
            if (!WorldGen.SolidTile(i, j + 1)) return false;
            if (Main.tile[i, j].active()) return false;
            if (j > 150) return false;

            for (int y = 0; y < AncientHouseBlocks.GetLength(0); y++)
            {
                for (int x = 0; x < AncientHouseBlocks.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = i - 6 + y;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (AncientHouseBlocks[x, y])
                        {
                            case @_:
                                tile.active(false);
                                break;
                            case W:
                                tile.type = TileID.WoodBlock;
                                tile.active(true);
                                break;
                            case H:
                                tile.type = TileID.Platforms;
                                tile.active(true);
                                break;
                            case S:
                                tile.type = TileID.Platforms;
                                tile.slope(1);
                                tile.active(true);
                                break;
                            case F:
                                tile.type = TileID.ClayPot;
                                tile.active(true);
                                break;
                            case I:
                                tile.type = TileID.ClosedDoor;
                                tile.active(true);
                                break;
                        }
                    }
                }
            }

            return true;
        }
    }
}
