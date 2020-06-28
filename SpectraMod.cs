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
            Terraria.Utils.DrawBorderString(spriteBatch, fileData.Name, new Vector2(num7 + 6f, dimensions.Y - 2f), color);
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

			float x11 = Terraria.Main.fontMouseText.MeasureString(text).X;
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

            Terraria.Utils.DrawBorderString(spriteBatch, text, vector + new Vector2(x10, 3f), difficultyColor);

			vector.X += num6 + 5f;

			float num5 = 150f;
			if (!GameCulture.English.IsActive)
			{
				num5 += 40f;
			}

			DrawPanel(spriteBatch, vector, num5);

			string textValue3 = Language.GetTextValue("UI.WorldSizeFormat", fileData.WorldSizeName);
			float x9 = Terraria.Main.fontMouseText.MeasureString(textValue3).X;
			float x8 = num5 * 0.5f - x9 * 0.5f;

            Terraria.Utils.DrawBorderString(spriteBatch, textValue3, vector + new Vector2(x8, 3f), Color.White);

			vector.X += num5 + 5f;
			float num4 = innerDimensions.X + innerDimensions.Width - vector.X;

			DrawPanel(spriteBatch, vector, num4);

			string arg = (!GameCulture.English.IsActive) ? fileData.CreationTime.ToShortDateString() : fileData.CreationTime.ToString("d MMMM yyyy");
			string textValue2 = Language.GetTextValue("UI.WorldCreatedFormat", arg);
			float x7 = Terraria.Main.fontMouseText.MeasureString(textValue2).X;
			float x6 = num4 * 0.5f - x7 * 0.5f;

            Terraria.Utils.DrawBorderString(spriteBatch, textValue2, vector + new Vector2(x6, 3f), Color.White);

			vector.X += num4 + 5f;
		}

		public void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
        {
			spriteBatch.Draw(TextureManager.Load("Images/UI/InnerPanelBackground"), position, new Rectangle(0, 0, 8, TextureManager.Load("Images/UI/InnerPanelBackground").Height), Color.White);
			spriteBatch.Draw(TextureManager.Load("Images/UI/InnerPanelBackground"), new Vector2(position.X + 8f, position.Y), new Rectangle(8, 0, 8, TextureManager.Load("Images/UI/InnerPanelBackground").Height), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(TextureManager.Load("Images/UI/InnerPanelBackground"), new Vector2(position.X + width - 8f, position.Y), new Rectangle(16, 0, 8, TextureManager.Load("Images/UI/InnerPanelBackground").Height), Color.White);
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