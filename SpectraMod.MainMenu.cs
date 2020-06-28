using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection;
using Terraria.IO;
using Terraria.UI;
using Terraria.Localization;
using Terraria.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using System.IO;
using System.Linq;
using MonoMod.Cil;
using Mono.Cecil.Cil;

namespace SpectraMod
{
    public partial class SpectraMod : Mod
    {
        #region WORLDCREATION
        private void AddProfessionalMode(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            ILLabel ifStatementEnd = null;

            var a = typeof(Main).GetField(nameof(Main.menuMode));

            if (!c.TryGotoNext(i => i.MatchLdsfld(a),
                i => i.MatchLdcI4(-7),
                i => i.MatchBneUn(out ifStatementEnd)))
            {
                Logger.Info("SpectraMod failed to patch DrawMenu");
                return;
            }
            if (ifStatementEnd == null)
            {
                Logger.Info("SpectraMod's DrawMenu patch's label is null");
                return;
            }

            ILLabel reeee = il.DefineLabel();

            int start = c.Index;
            c.GotoLabel(ifStatementEnd);
            int end = c.Index;
            c.Goto(start);

            c.Emit(OpCodes.Br, reeee);
            c.Goto(end);
            c.MarkLabel(reeee);

            var focus = typeof(Main).GetField("focusMenu", BindingFlags.NonPublic | BindingFlags.Instance);
            var selectedmenu = typeof(Main).GetField("selectedMenu", BindingFlags.NonPublic | BindingFlags.Instance);

            // pushing fields by reference to the delegate (except this)
            c.Emit(OpCodes.Ldarg_0); // this
            c.Emit(OpCodes.Ldarg_0); // this
            c.Emit(OpCodes.Ldflda, focus); // focusmenu
            c.Emit(OpCodes.Ldarg_0); // this
            c.Emit(OpCodes.Ldflda, selectedmenu); // selectedmenu
            c.Emit(OpCodes.Ldloca, 5); // num2
            c.Emit(OpCodes.Ldloca, 7); // num4
            c.Emit(OpCodes.Ldloca, 19); // array4
            c.Emit(OpCodes.Ldloca, 21); // array6
            c.Emit(OpCodes.Ldloca, 26); // array9
            c.Emit(OpCodes.Ldloca, 16); // array
            c.Emit(OpCodes.Ldloca, 8); // num5
            c.Emit(OpCodes.Ldloca, 25); // flag5

            c.Emit(OpCodes.Call, ((modifyingdelegate)ProfessionalMenu).Method); // now we call E V E R Y T H I N G that was inside that if statement manually, rip
        }
        private delegate void modifyingdelegate(Main instance, ref int focusmenu, ref int selectedmenu, ref int num2, ref int num4, ref int[] array4, ref byte[] array6, ref string[] array9, ref bool[] array, ref int num5, ref bool flag);
        private static void ProfessionalMenu(Main instance, ref int focusMenu, ref int selectedMenu, ref int num2, ref int num4, ref int[] array4, ref byte[] array6, ref string[] array9, ref bool[] array, ref int num5, ref bool flag5)
        {
            num2 = 200;
            num4 = 60;
            int offset = -10;
            array4[2] = 30 + offset - 1; //30 - 20; // 30
            array4[3] = 30 + offset - 3 - 1; //30 - 10; // 30
            array6[3] = 2; //2; // rarity // 2
            array4[4] = 70; // 70
            array4[5] = -40 + offset / 2 - 1; // -40 - 10;
            array6[5] = 4;
            if (focusMenu == 2)
            {
                array9[0] = Language.GetTextValue("UI.NormalDescriptionFlavor");
                array9[1] = Language.GetTextValue("UI.NormalDescription");
            }
            else if (focusMenu == 3)
            {
                array9[0] = Language.GetTextValue("UI.ExpertDescriptionFlavor");
                array9[1] = Language.GetTextValue("UI.ExpertDescription");
            }
            else if (focusMenu == 5) // Professional
            {
                array9[0] = "No Pain, No Gain.";
                array9[1] = "(Even Greater Difficulty & Loot!)";
            }
            else
            {
                array9[0] = Lang.menu[32].Value;
            }
            array[0] = true;
            array[1] = true;

            array9[2] = Language.GetTextValue("UI.Normal");
            array9[3] = Language.GetTextValue("UI.Expert");
            array9[4] = Language.GetTextValue("UI.Back");
            array9[5] = "Professional";
            num5 = 6;
            if (selectedMenu == 2)
            {
                Main.expertMode = false;
                SpectraWorld.professionalMode = false;
                SpectraWorld.IsProfessionalMode = false;
                Main.PlaySound(10, -1, -1, 1, 1f, 0f);
                Main.menuMode = 7;
                if (Main.SettingsUnlock_WorldEvil)
                {
                    Main.menuMode = -71;
                }
            }
            else if (selectedMenu == 3)
            {
                Main.expertMode = true;
                SpectraWorld.professionalMode = false;
                SpectraWorld.IsProfessionalMode = false;
                Main.PlaySound(10, -1, -1, 1, 1f, 0f);
                Main.menuMode = 7;
                if (Main.SettingsUnlock_WorldEvil)
                {
                    Main.menuMode = -71;
                }
            }
            else if (selectedMenu == 5)
            {
                Main.PlaySound(10, -1, -1, 1, 1f, 0f);
                Main.menuMode = Main.SettingsUnlock_WorldEvil ? -71 : 7;
                Main.expertMode = true;
                SpectraWorld.professionalMode = true;
                SpectraWorld.IsProfessionalMode = true;
            }
            else if (selectedMenu == 4 || flag5)
            {
                flag5 = false;
                Main.PlaySound(11, -1, -1, 1, 1f, 0f);
                Main.menuMode = 16;
            }
            Main.clrInput();
        }
        #endregion

        #region SELECTSCREEN
        private void ProfessionalText(On.Terraria.GameContent.UI.Elements.UIWorldListItem.orig_DrawSelf orig, Terraria.GameContent.UI.Elements.UIWorldListItem self, SpriteBatch spriteBatch)
        {
            orig(self, spriteBatch);

            FieldInfo dataInfo = self.GetType().GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo worldIconInfo = self.GetType().GetField("_worldIcon", BindingFlags.NonPublic | BindingFlags.Instance);
            WorldFileData fileData = (WorldFileData)dataInfo.GetValue(self);
            UIImage worldIcon = (UIImage)worldIconInfo.GetValue(self);

            string filePath = fileData.Path.Replace(".wld", ".twld"); //remember .wld exists ahh

            TagCompound tagCompound1;

            try
            {
                byte[] bytes = FileUtilities.ReadAllBytes(filePath, fileData.IsCloudSave);
                tagCompound1 = TagIO.FromStream(new MemoryStream(bytes), true);
            }
            catch
            {
                tagCompound1 = null;
            }

            TagCompound tagCompound2 = tagCompound1?.GetList<TagCompound>("modData").FirstOrDefault(i => i.GetString("mod") == "SpectraMod" && i.GetString("name") == "SpectraWorld"); //allow null
            TagCompound tagCompound3 = tagCompound2?.Get<TagCompound>("data");

            CalculatedStyle innerDimensions = self.GetInnerDimensions();
            CalculatedStyle dimensions = worldIcon.GetDimensions();
            float num7 = dimensions.X + dimensions.Width;
            Color color = fileData.IsValid ? Color.White : Color.Red;
            Utils.DrawBorderString(spriteBatch, fileData.Name, new Vector2(num7 + 6f, dimensions.Y - 2f), color);
            spriteBatch.Draw(TextureManager.Load("Images/UI/Divider"), new Vector2(num7, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((self.GetDimensions().X + self.GetDimensions().Width - num7) / 8f, 1f), SpriteEffects.None, 0f);
            Vector2 vector = new Vector2(num7 + 6f, innerDimensions.Y + 29f);
            float num6 = 100f;
            DrawPanel(spriteBatch, vector, num6);

            bool? IsProfessionalMode = tagCompound3?.GetBool("IsProfessionalMode");

            if (IsProfessionalMode == null)
                IsProfessionalMode = false;

            string text;

            if ((bool)IsProfessionalMode)
                text = "Professional";
            else if (fileData.IsExpertMode && (bool)!IsProfessionalMode)
                text = Language.GetTextValue("UI.Expert");
            else
                text = Language.GetTextValue("UI.Normal");

            float x11 = Main.fontMouseText.MeasureString(text).X;
            float x10 = num6 * 0.5f - x11 * 0.5f;

            if ((bool)IsProfessionalMode)
                text = "Professional";
            else if (fileData.IsExpertMode && (bool)!IsProfessionalMode)
                text = Language.GetTextValue("UI.Expert");
            else
                text = Language.GetTextValue("UI.Normal");

            Color difficultyColor;

            if ((bool)IsProfessionalMode)
                difficultyColor = new Color(255, 0, 0);
            else if (fileData.IsExpertMode && (bool)!IsProfessionalMode)
                difficultyColor = new Color(217, 143, 244);
            else
                difficultyColor = Color.White;

            Utils.DrawBorderString(spriteBatch, text, vector + new Vector2(x10, 3f), difficultyColor);

            vector.X += num6 + 5f;

            float num5 = 150f;
            if (!GameCulture.English.IsActive)
            {
                num5 += 40f;
            }

            DrawPanel(spriteBatch, vector, num5);

            string textValue3 = Language.GetTextValue("UI.WorldSizeFormat", fileData.WorldSizeName);
            float x9 = Main.fontMouseText.MeasureString(textValue3).X;
            float x8 = num5 * 0.5f - x9 * 0.5f;

            Utils.DrawBorderString(spriteBatch, textValue3, vector + new Vector2(x8, 3f), Color.White);

            vector.X += num5 + 5f;
            float num4 = innerDimensions.X + innerDimensions.Width - vector.X;

            DrawPanel(spriteBatch, vector, num4);

            string arg = (!GameCulture.English.IsActive) ? fileData.CreationTime.ToShortDateString() : fileData.CreationTime.ToString("d MMMM yyyy");
            string textValue2 = Language.GetTextValue("UI.WorldCreatedFormat", arg);
            float x7 = Main.fontMouseText.MeasureString(textValue2).X;
            float x6 = num4 * 0.5f - x7 * 0.5f;

            Utils.DrawBorderString(spriteBatch, textValue2, vector + new Vector2(x6, 3f), Color.White);

            vector.X += num4 + 5f;
        }

        public void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
        {
            spriteBatch.Draw(TextureManager.Load("Images/UI/InnerPanelBackground"), position, new Rectangle(0, 0, 8, TextureManager.Load("Images/UI/InnerPanelBackground").Height), Color.White);
            spriteBatch.Draw(TextureManager.Load("Images/UI/InnerPanelBackground"), new Vector2(position.X + 8f, position.Y), new Rectangle(8, 0, 8, TextureManager.Load("Images/UI/InnerPanelBackground").Height), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
            spriteBatch.Draw(TextureManager.Load("Images/UI/InnerPanelBackground"), new Vector2(position.X + width - 8f, position.Y), new Rectangle(16, 0, 8, TextureManager.Load("Images/UI/InnerPanelBackground").Height), Color.White);
        }
        #endregion
    }
}
