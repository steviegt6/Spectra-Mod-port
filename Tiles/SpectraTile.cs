using SpectraMod.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace SpectraMod.Tiles
{
    public class SpectraTile : GlobalTile
    {
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if (SpectraItem.dirtPick)
                switch (type)
                {
                    case TileID.Dirt:
                    case TileID.Mud:
                    case TileID.ClayBlock:
                    case TileID.Grass:
                    case TileID.CorruptGrass:
                    case TileID.FleshGrass:
                    case TileID.HallowedGrass:
                        return true;
                    default:
                        return false;
                }
            else
                return base.CanKillTile(i, j, type, ref blockDamaged);
        }
    }
}