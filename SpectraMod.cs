using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SpectraMod
{
	public partial class SpectraMod : Mod
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