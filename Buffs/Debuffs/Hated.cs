using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Buffs.Debuffs
{
    public class Hated : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Hated");
            Description.SetDefault("Slower");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SpectraPlayer>().Hated = true;
        }
    }
}
