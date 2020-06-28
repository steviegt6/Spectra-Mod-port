using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace SpectraMod.NPCs
{
    public class SpectraGlobalNPC : GlobalNPC
    {
        public override void AI(NPC npc)
        {
            switch (npc.type) // sorry for the magic numbers, cba to change them to their respective npc ids
            {
                case 3:
                case 331:
                case 332:
                case 132:
                case 161:
                case 186:
                case 187:
                case 188:
                case 189:
                case 200:
                case 223:
                case 320:
                case 321:
                case 319:
                case 109:
                case 163:
                case 164:
                case 199:
                case 236:
                case 239:
                case 257:
                case 258:
                case 290:
                case 391:
                case 425:
                case 427:
                case 426:
                case 508:
                case 415:
                case 520:
                case 532:
                        if (SpectraWorld.professionalMode)
                        {
                            //TODO: Zombies open doors always
                        }
                    break;
            }

            base.AI(npc);
        }
    }
}
