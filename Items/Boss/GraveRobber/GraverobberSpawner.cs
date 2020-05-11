using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;

namespace SpectraMod.Items.Boss.GraveRobber
{
    public class GraverobberSpawner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Mask");
            Tooltip.SetDefault("Summons the Grave Robber" +
                               "\nCan only be used at night");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 0;
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(32, 36);
            item.maxStack = 20;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 30;
            item.useAnimation = 30;
            item.value = 0;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Boss.GraveRobber.GraveRobber>());
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.Boss.GraveRobber.GraveRobber>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Wood");
            recipe.AddIngredient(ModContent.ItemType<Materials.ShadowGel>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}