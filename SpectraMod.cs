using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.UI.Gamepad;
using System.Collections.Generic;
using Terraria.UI;
using System;
using MonoMod.Utils;
using MonoMod.Cil;
using System.Reflection;
using Mono.Cecil.Cil;

namespace SpectraMod
{
	public class SpectraMod : Mod
	{
		internal static SpectraMod Instance;
		internal static bool SizeFix;

		private static int maxMenuItems = 16; //TODO: search all
		private int focusMenu = -1;
		private int selectedMenu = -1;

		public static SpectraHelper[] spectraHelper = new SpectraHelper[20];

		public override void Load()
		{
			Instance = this;

			IL.Terraria.Main.DrawMenu += AddProfessionalMode;
		}

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
                //InteritosWorld.GenkaiMode = true;
            }
            else if (selectedMenu == 4 || flag5)
            {
                flag5 = false;
                Main.PlaySound(11, -1, -1, 1, 1f, 0f);
                Main.menuMode = 16;
            }
            Main.clrInput();
        }


		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            base.ModifyInterfaceLayers(layers);
        }

        public override void LoadResources()
		{
			base.LoadResources();
			SizeFix = false;
		}

		public override void PostAddRecipes()
		{
			SizeFix = true;
		}

		public SpectraMod()
		{
		}

		public override void AddRecipeGroups()
		{
			RecipeGroup HardmodeEvilMaterial = new RecipeGroup(() => "Any hardmode evil material", new int[] {
				ItemID.CursedFlame,
				ItemID.Ichor
			});
			RecipeGroup.RegisterGroup("Spectra:HardmodeEvil", HardmodeEvilMaterial);

			RecipeGroup EvilPick = new RecipeGroup(() => "Any evil pickaxe", new int[] {
				ItemID.NightmarePickaxe,
				ItemID.DeathbringerPickaxe
			});
			RecipeGroup.RegisterGroup("Spectra:EvilPick", EvilPick);
		}
	}
}