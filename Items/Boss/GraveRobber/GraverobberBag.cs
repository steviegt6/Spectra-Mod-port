using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SpectraMod.Items.Boss.GraveRobber
{
    public class GraverobberBag : SpectraItem
    {
        public override int BossBagNPC => ModContent.NPCType<NPCs.Boss.GraveRobber.GraveRobber>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right-click to open");
        }

        public override void SafeSetDefaults()
        {
            item.consumable = true;
            item.rare = ItemRarityID.Green;
            item.expert = true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();

            Item.NewItem(player.getRect(), ModContent.ItemType<HatredBar>(), Main.rand.Next(17) + 2);
            Item.NewItem(player.getRect(), ModContent.ItemType<UnluckyTomb>());
            Item.NewItem(player.getRect(), ModContent.ItemType<GraverobberMachete>());
        }
    }
}