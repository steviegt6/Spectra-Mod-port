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
using ReLogic.Graphics;

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
			On.Terraria.Main.DrawInterface_35_YouDied += YouDiedL;
			On.Terraria.Player.DropCoins += DropAllYourCoins;
		}

        public override void Unload()
        {
			Instance = null;

            base.Unload();
        }

		private int DropAllYourCoins(On.Terraria.Player.orig_DropCoins orig, Terraria.Player self)
		{
			Player player = Main.LocalPlayer;
			int num6 = 0;
			for (int i = 0; i < 59; i++)
			{
				if (player.inventory[i].type >= ItemID.CopperCoin && player.inventory[i].type <= ItemID.PlatinumCoin)
				{
					int num5 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, player.inventory[i].type);
					int num4 = player.inventory[i].stack / 2;
					if (Main.expertMode)
					{
						num4 = (int)((double)player.inventory[i].stack * 0.25);
					}
					if (SpectraWorld.professionalMode)
                    {
						num4 = (int)((double)player.inventory[i].stack * 0);
					}
					num4 = player.inventory[i].stack - num4;
					player.inventory[i].stack -= num4;
					if (player.inventory[i].type == ItemID.CopperCoin)
					{
						num6 += num4;
					}
					if (player.inventory[i].type == ItemID.SilverCoin)
					{
						num6 += num4 * 100;
					}
					if (player.inventory[i].type == ItemID.GoldCoin)
					{
						num6 += num4 * 10000;
					}
					if (player.inventory[i].type == ItemID.PlatinumCoin)
					{
						num6 += num4 * 1000000;
					}
					if (player.inventory[i].stack <= 0)
					{
						player.inventory[i] = new Item();
					}
					Main.item[num5].stack = num4;
					Main.item[num5].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
					Main.item[num5].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
					Main.item[num5].noGrabDelay = 100;
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, null, num5);
					}
					if (i == 58)
					{
						Main.mouseItem = player.inventory[i].Clone();
					}
				}
			}
			player.lostCoins = num6;
			player.lostCoinString = Main.ValueToCoins(player.lostCoins);
			return num6;
		}

		private void YouDiedL(On.Terraria.Main.orig_DrawInterface_35_YouDied orig) // method swapping for professional mode just because
		{
			if (Terraria.Main.player[Terraria.Main.myPlayer].dead)
			{
				string value = Terraria.Lang.inter[38].Value;
				DynamicSpriteFontExtensionMethods.DrawString(Terraria.Main.spriteBatch, Terraria.Main.fontDeathText, value, new Vector2((float)(Terraria.Main.screenWidth / 2) - Terraria.Main.fontDeathText.MeasureString(value).X / 2f, Terraria.Main.screenHeight / 2 - 20), Terraria.Main.player[Terraria.Main.myPlayer].GetDeathAlpha(Microsoft.Xna.Framework.Color.Transparent), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				if (Terraria.Main.player[Terraria.Main.myPlayer].lostCoins > 0)
				{
					string textValue = SpectraWorld.professionalMode ? "dropped all (" + Terraria.Main.player[Terraria.Main.myPlayer].lostCoinString + ") coins" : Language.GetTextValue("Game.DroppedCoins", Terraria.Main.player[Terraria.Main.myPlayer].lostCoinString);
					string coinText = SpectraWorld.professionalMode ? "(" + Terraria.Main.player[Terraria.Main.myPlayer].lostCoinString + ")" : "";
					DynamicSpriteFontExtensionMethods.DrawString(Terraria.Main.spriteBatch, Terraria.Main.fontMouseText, textValue, new Vector2((float)(Terraria.Main.screenWidth / 2) - Terraria.Main.fontMouseText.MeasureString(textValue).X / 2f, Terraria.Main.screenHeight / 2 + 30), Terraria.Main.player[Terraria.Main.myPlayer].GetDeathAlpha(Microsoft.Xna.Framework.Color.Transparent), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
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
            Terraria.RecipeGroup HardmodeEvilMaterial = new Terraria.RecipeGroup(() => "Any hardmode evil material", new int[] {
				ItemID.CursedFlame,
				ItemID.Ichor
			});
            Terraria.RecipeGroup.RegisterGroup("Spectra:HardmodeEvil", HardmodeEvilMaterial);

            Terraria.RecipeGroup EvilPick = new Terraria.RecipeGroup(() => "Any evil pickaxe", new int[] {
				ItemID.NightmarePickaxe,
				ItemID.DeathbringerPickaxe
			});
            Terraria.RecipeGroup.RegisterGroup("Spectra:EvilPick", EvilPick);
		}
	}

    public class WorldFileDataNew : WorldFileData
    {
    }
}