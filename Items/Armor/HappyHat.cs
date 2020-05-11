using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Steamworks;
using Terraria.ID;

namespace SpectraMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class HappyHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lovely Hat");
            Tooltip.SetDefault("Increases movement speed" +
                               "\nGives the happy buff, even when in vanity");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(28, 14);
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.defense += 3;
            item.rare = ItemRarityID.Green;
        }

        public override void UpdateVanity(Player player, EquipType type)
        {
            player.AddBuff(BuffID.Sunflower, 60);
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(BuffID.Sunflower, 60);
            player.moveSpeed += 0.20f;
        }
    }
}