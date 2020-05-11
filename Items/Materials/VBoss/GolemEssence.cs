using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Materials.VBoss
{
    public class GolemEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Energy");
            Tooltip.SetDefault("Essence of something ancient");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(43, 32);
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = ItemRarityID.Lime;
        }
    }
}
