using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System;
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

namespace SpectraMod
{
	public partial class SpectraMod : Mod
	{
		internal static SpectraMod Instance;
		internal static bool SizeFix;

		public static SpectraHelper[] spectraHelper = new SpectraHelper[20];

		public override void Load()
		{
			Instance = this;

			IL.Terraria.Main.DrawMenu += AddProfessionalMode;

			On.Terraria.GameContent.UI.Elements.UIWorldListItem.DrawSelf += ProfessionalText;
		}

        public override void Unload()
        {
			Instance = null;

            base.Unload();
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

    public class WorldFileDataNew : WorldFileData
    {
    }
}