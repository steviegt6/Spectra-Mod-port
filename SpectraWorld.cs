using Terraria;
using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;

namespace SpectraMod
{
    public class SpectraWorld : ModWorld
    {
        public static bool professionalMode;
        public static bool IsProfessionalMode;

        public override void PreUpdate()
        {
            AnimatedColor.Update();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void PreUpdate()
        {
            if (professionalMode)
            {
                Main.expertDamage = 3f;
                Main.expertKnockBack = 0.8f;
                Main.expertDebuffTime = 3f;
                Main.expertLife = 3f;
                Main.expertNPCDamage = 2.5f;
            }
            else
            {
                Main.expertDamage = 2f;
                Main.expertKnockBack = 0.9f;
                Main.expertDebuffTime = 2f;
                Main.expertLife = 2f;
                Main.expertNPCDamage = 1.5f;
            }

            base.PreUpdate();
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();

            if (professionalMode)
                downed.Add("professionalMode");

            return new TagCompound
            {
                ["downed"] = downed,
                ["IsProfessionalMode"] = IsProfessionalMode
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");

            professionalMode = downed.Contains("professionalMode");

            IsProfessionalMode = professionalMode;

            base.Load(tag);
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();

            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();

                professionalMode = flags[0];
            }
            else
                mod.Logger.WarnFormat("SpectraMod: Unknown loadVersion: {0}", loadVersion);
            base.LoadLegacy(reader);
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();

            flags[0] = professionalMode;

            writer.Write(flags);
            base.NetSend(writer);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();

            professionalMode = flags[0];
            base.NetReceive(reader);
        }
    }
}
