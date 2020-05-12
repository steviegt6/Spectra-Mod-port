using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Buffs.Debuffs
{
    public class DebuffNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool Hated;
        private int oldDamage;

        public override void ResetEffects(NPC npc)
        {
            Hated = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            Main.NewText("e");
            if (Hated)
            {
                // Less damage here sometime
                npc.lifeRegen -= 4;
            }
        }
    }
}
