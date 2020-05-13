using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpectraMod.NPCs
{
    public abstract class SpectraNPC : ModNPC
    {
        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            Texture2D texture = Main.npcTexture[npc.type];
            int frameCount = Main.npcFrameCount[npc.type];
            if (SpectraMod.SizeFix)
            {
                int realWidth = texture.Width;
                int realHeight = texture.Height / frameCount;
                if (SpectraMod.SizeFix) npc.Size = new Vector2(realWidth, realHeight);
            }

            SafeSetDefaults();
        }
    }
}
