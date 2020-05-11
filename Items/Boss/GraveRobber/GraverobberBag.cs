using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.ID;

namespace SpectraMod.Items.Boss.GraveRobber
{
    public class GraverobberBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<NPCs.Boss.GraveRobber.GraveRobber>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right-click to open");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(24, 24);
            item.consumable = true;
            item.rare = ItemRarityID.Green;
            item.expert = true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();

            Item.NewItem(player.getRect(), ModContent.ItemType<HatredBar>(), Main.rand.Next(17) + 2);
            Item.NewItem(player.getRect(), ModContent.ItemType<UnluckyTomb>());
            //var dropChoice = new WeightedRandom<int>();
            //dropChoice.Add(ModContent.ItemType<GraverobberMachete>());
            //dropChoice.Add(ModContent.ItemType<GraverobberRanged>());
            //dropChoice.Add(ModContent.ItemType<GraverobberMagic>());
            //dropChoice.Add(ModContent.ItemType<GraverobberThrown>());
            //int choice = dropChoice;
            Item.NewItem(player.getRect(), ModContent.ItemType<GraverobberMachete>());
        }
    }
}