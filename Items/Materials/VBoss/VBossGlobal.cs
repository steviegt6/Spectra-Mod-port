using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Materials.VBoss
{
    public class VBossNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (!Main.expertMode)
            {
                switch (npc.type)
                {
                    case NPCID.TheDestroyer:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<DestroyerMandible>(), Main.rand.Next(11) + 20);
                        break;
                    case NPCID.SkeletronPrime:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<SkeletronRib>(), Main.rand.Next(16) + 30);
                        break;
                    case NPCID.Spazmatism:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<TwinScanner>(), Main.rand.Next(5) + 15);
                        break;
                    case NPCID.Retinazer:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<TwinScanner>(), Main.rand.Next(5) + 15);
                        break;
                    case NPCID.Golem:
                        //Item.NewItem(npc.getRect(), ModContent.ItemType<GolemEssence>(), Main.rand.Next(6) + 3);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class VBossItem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag")
            {
                switch (arg)
                {
                    case NPCID.TheDestroyer:
                        Item.NewItem(player.getRect(), ModContent.ItemType<DestroyerMandible>(), Main.rand.Next(11) + 20);
                        break;
                    case NPCID.SkeletronPrime:
                        Item.NewItem(player.getRect(), ModContent.ItemType<SkeletronRib>(), Main.rand.Next(16) + 30);
                        break;
                    case NPCID.Spazmatism:
                        Item.NewItem(player.getRect(), ModContent.ItemType<TwinScanner>(), Main.rand.Next(5) + 15);
                        break;
                    case NPCID.Retinazer:
                        Item.NewItem(player.getRect(), ModContent.ItemType<TwinScanner>(), Main.rand.Next(5) + 15);
                        break;
                    case NPCID.Golem:
                        //Item.NewItem(npc.getRect(), ModContent.ItemType<GolemEssence>(), Main.rand.Next(6) + 3);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
