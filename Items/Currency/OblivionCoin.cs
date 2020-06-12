using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Mono.Cecil.Cil.OpCodes;

namespace SpectraMod.Items.Currency
{
    public class OblivionCoin : ModItem
    {
        public override bool Autoload(ref string name)
        {
            IL.Terraria.Chest.SetupShop += HookSetupShop;
            IL.Terraria.Item.UpdateItem += HookUpdateItem; //795
			On.Terraria.ItemText.NewText += HookNewText;
			IL.Terraria.Main.UpdateTime_SpawnTownNPCs += HookUpdateTime_SpawnTownNPCs;
			//IL.Terraria.Player.GrabItems += HookGrabItems;
            return base.Autoload(ref name);
        }

		#region IL and On Stuff
#pragma warning disable ChangeMagicNumberToID // Change magic numbers into appropriate ID values
		/*private void HookGrabItems(ILContext il)
		{
			var c = new ILCursor(il);

			if (!c.TryGotoNext(i => i.MatchLdloc(8)))
				return;

			//c.Emit(Ldloc);

			c.Index -= 2;

			c.EmitDelegate<Func<int, int, int>>(delegate (int j, int num)
			{
				Player player = Main.player[Main.myPlayer];
				if (player.goldRing && Main.item[j].type == ModContent.ItemType<OblivionCoin>())
				{
					return num += Item.coinGrabRange;
				}
				return num;
			});
		}*/

		private void HookUpdateTime_SpawnTownNPCs(ILContext il)
		{
			var c = new ILCursor(il);

			if (!c.TryGotoNext(i => i.MatchStloc(63)))
				return;

			c.Index++;

			c.Emit(Stloc);

			c.EmitDelegate<Func<int, int, int, int>>(delegate (int l, int m, int num27)
			{
				if (Main.player[l].inventory[m].type == ModContent.ItemType<OblivionCoin>())
				{
					return num27 += Main.player[l].inventory[m].stack * 100000000;
				}
				return num27;
			});
		}

		#region HookNewText lol
		private void HookNewText(On.Terraria.ItemText.orig_NewText orig, Terraria.Item newItem, int stack, bool noStack, bool longText)
		{
			bool flag = ((newItem.type >= 71 && newItem.type <= 74) || newItem.type == ModContent.ItemType<OblivionCoin>());
			if (!Main.showItemText || newItem.Name == null || !newItem.active || Main.netMode == 2)
			{
				return;
			}
			for (int k = 0; k < 20; k++)
			{
				if ((!Main.itemText[k].active || (!(Main.itemText[k].name == newItem.AffixName()) && (!flag) || Main.itemText[k].NoStack) | noStack))
				{
					continue;
				}
				string text3 = newItem.Name + " (" + (Main.itemText[k].stack + stack).ToString() + ")";
				string text2 = newItem.Name;
				if (Main.itemText[k].stack > 1)
				{
					text2 = text2 + " (" + Main.itemText[k].stack.ToString() + ")";
				}
				Vector2 vector2 = Main.fontMouseText.MeasureString(text2);
				vector2 = Main.fontMouseText.MeasureString(text3);
				if (Main.itemText[k].lifeTime < 0)
				{
					Main.itemText[k].scale = 1f;
				}
				if (Main.itemText[k].lifeTime < 60)
				{
					Main.itemText[k].lifeTime = 60;
				}
				if (flag)
				{
					int num = 0;
					if (newItem.type == 71)
					{
						num += newItem.stack;
					}
					else if (newItem.type == 72)
					{
						num += 100 * newItem.stack;
					}
					else if (newItem.type == 73)
					{
						num += 10000 * newItem.stack;
					}
					else if (newItem.type == 74)
					{
						num += 1000000 * newItem.stack;
					}
					else if (newItem.type == ModContent.ItemType<OblivionCoin>())
					{
						num += 100000000 * newItem.stack;
					}
					Main.itemText[k].coinValue += num;
					text3 = SpectraHelper.SpectraValueToName(Main.itemText[k].coinValue);
					vector2 = Main.fontMouseText.MeasureString(text3);
					Main.itemText[k].name = text3;
					if (Main.itemText[k].coinValue >= 100000000)
					{
						if (Main.itemText[k].lifeTime < 450)
						{
							Main.itemText[k].lifeTime = 450;
						}
						Main.itemText[k].color = Color.MediumPurple;
					}
					else if (Main.itemText[k].coinValue >= 1000000)
					{
						if (Main.itemText[k].lifeTime < 300)
						{
							Main.itemText[k].lifeTime = 300;
						}
						Main.itemText[k].color = new Color(220, 220, 198);
					}
					else if (Main.itemText[k].coinValue >= 10000)
					{
						if (Main.itemText[k].lifeTime < 240)
						{
							Main.itemText[k].lifeTime = 240;
						}
						Main.itemText[k].color = new Color(224, 201, 92);
					}
					else if (Main.itemText[k].coinValue >= 100)
					{
						if (Main.itemText[k].lifeTime < 180)
						{
							Main.itemText[k].lifeTime = 180;
						}
						Main.itemText[k].color = new Color(181, 192, 193);
					}
					else if (Main.itemText[k].coinValue >= 1)
					{
						if (Main.itemText[k].lifeTime < 120)
						{
							Main.itemText[k].lifeTime = 120;
						}
						Main.itemText[k].color = new Color(246, 138, 96);
					}
				}
				Main.itemText[k].stack += stack;
				Main.itemText[k].scale = 0f;
				Main.itemText[k].rotation = 0f;
				Main.itemText[k].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector2.X * 0.5f;
				Main.itemText[k].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector2.Y * 0.5f;
				Main.itemText[k].velocity.Y = -7f;
				if (flag)
				{
					Main.itemText[k].stack = 1;
				}
				return;
			}
			int num4 = -1;
			for (int j = 0; j < 20; j++)
			{
				if (!Main.itemText[j].active)
				{
					num4 = j;
					break;
				}
			}
			if (num4 == -1)
			{
				double num3 = Main.bottomWorld;
				for (int i = 0; i < 20; i++)
				{
					if (num3 > (double)Main.itemText[i].position.Y)
					{
						num4 = i;
						num3 = Main.itemText[i].position.Y;
					}
				}
			}
			if (num4 < 0)
			{
				return;
			}
			string text4 = newItem.AffixName();
			if (stack > 1)
			{
				text4 = text4 + " (" + stack.ToString() + ")";
			}
			Vector2 vector3 = Main.fontMouseText.MeasureString(text4);
			Main.itemText[num4].alpha = 1f;
			Main.itemText[num4].alphaDir = -1;
			Main.itemText[num4].active = true;
			Main.itemText[num4].scale = 0f;
			Main.itemText[num4].NoStack = noStack;
			Main.itemText[num4].rotation = 0f;
			Main.itemText[num4].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector3.X * 0.5f;
			Main.itemText[num4].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector3.Y * 0.5f;
			Main.itemText[num4].color = Color.White;
			if (newItem.rare == 1)
			{
				Main.itemText[num4].color = new Color(150, 150, 255);
			}
			else if (newItem.rare == 2)
			{
				Main.itemText[num4].color = new Color(150, 255, 150);
			}
			else if (newItem.rare == 3)
			{
				Main.itemText[num4].color = new Color(255, 200, 150);
			}
			else if (newItem.rare == 4)
			{
				Main.itemText[num4].color = new Color(255, 150, 150);
			}
			else if (newItem.rare == 5)
			{
				Main.itemText[num4].color = new Color(255, 150, 255);
			}
			else if (newItem.rare == -11)
			{
				Main.itemText[num4].color = new Color(255, 175, 0);
			}
			else if (newItem.rare == -1)
			{
				Main.itemText[num4].color = new Color(130, 130, 130);
			}
			else if (newItem.rare == 6)
			{
				Main.itemText[num4].color = new Color(210, 160, 255);
			}
			else if (newItem.rare == 7)
			{
				Main.itemText[num4].color = new Color(150, 255, 10);
			}
			else if (newItem.rare == 8)
			{
				Main.itemText[num4].color = new Color(255, 255, 10);
			}
			else if (newItem.rare == 9)
			{
				Main.itemText[num4].color = new Color(5, 200, 255);
			}
			else if (newItem.rare == 10)
			{
				Main.itemText[num4].color = new Color(255, 40, 100);
			}
			else if (newItem.rare >= 11)
			{
				Main.itemText[num4].color = new Color(180, 40, 255);
			}
			Main.itemText[num4].expert = newItem.expert;
			Main.itemText[num4].name = newItem.AffixName();
			Main.itemText[num4].stack = stack;
			Main.itemText[num4].velocity.Y = -7f;
			Main.itemText[num4].lifeTime = 60;
			if (longText)
			{
				Main.itemText[num4].lifeTime *= 5;
			}
			Main.itemText[num4].coinValue = 0;
			Main.itemText[num4].coinText = ((newItem.type >= 71 && newItem.type <= 74) || newItem.type == ModContent.ItemType<OblivionCoin>());
			if (!Main.itemText[num4].coinText)
			{
				return;
			}
			if (newItem.type == 71)
			{
				Main.itemText[num4].coinValue += Main.itemText[num4].stack;
			}
			else if (newItem.type == 72)
			{
				Main.itemText[num4].coinValue += 100 * Main.itemText[num4].stack;
			}
			else if (newItem.type == 73)
			{
				Main.itemText[num4].coinValue += 10000 * Main.itemText[num4].stack;
			}
			else if (newItem.type == 74)
			{
				Main.itemText[num4].coinValue += 1000000 * Main.itemText[num4].stack;
			}
			else if (newItem.type == ModContent.ItemType<OblivionCoin>())
			{
				Main.itemText[num4].coinValue += 100000000 * Main.itemText[num4].stack;
			}
			SpectraValueToName();
			Main.itemText[num4].stack = 1;
			int num2 = num4;
			if (Main.itemText[num2].coinValue >= 100000000)
			{
				if (Main.itemText[num2].lifeTime < 450)
				{
					Main.itemText[num2].lifeTime = 450;
				}
				Main.itemText[num2].color = Color.MediumPurple;
			}
			else if (Main.itemText[num2].coinValue >= 1000000)
			{
				if (Main.itemText[num2].lifeTime < 300)
				{
					Main.itemText[num2].lifeTime = 300;
				}
				Main.itemText[num2].color = new Color(220, 220, 198);
			}
			else if (Main.itemText[num2].coinValue >= 10000)
			{
				if (Main.itemText[num2].lifeTime < 240)
				{
					Main.itemText[num2].lifeTime = 240;
				}
				Main.itemText[num2].color = new Color(224, 201, 92);
			}
			else if (Main.itemText[num2].coinValue >= 100)
			{
				if (Main.itemText[num2].lifeTime < 180)
				{
					Main.itemText[num2].lifeTime = 180;
				}
				Main.itemText[num2].color = new Color(181, 192, 193);
			}
			else if (Main.itemText[num2].coinValue >= 1)
			{
				if (Main.itemText[num2].lifeTime < 120)
				{
					Main.itemText[num2].lifeTime = 120;
				}
				Main.itemText[num2].color = new Color(246, 138, 96);
			}
		}

		private void SpectraValueToName()
		{
			ItemText itemText = new ItemText();
			int num10 = 0;
			int num9 = 0;
			int num8 = 0;
			int num7 = 0;
			int num6 = 0;
			int num5 = itemText.coinValue;
			while (num5 > 0)
			{
				if (num5 >= 100000000)
				{
					num5 -= 100000000;
					num10++;
				}
				if (num5 >= 1000000)
				{
					num5 -= 1000000;
					num9++;
				}
				else if (num5 >= 10000)
				{
					num5 -= 10000;
					num8++;
				}
				else if (num5 >= 100)
				{
					num5 -= 100;
					num7++;
				}
				else if (num5 >= 1)
				{
					num5--;
					num6++;
				}
			}
			itemText.name = "";
			if (num10 > 0)
			{
				itemText.name = itemText.name + num10.ToString() + string.Format(" {0} ", "Oblivion");
			}
			if (num9 > 0)
			{
				itemText.name = itemText.name + num9.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Platinum"));
			}
			if (num8 > 0)
			{
				itemText.name = itemText.name + num8.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Gold"));
			}
			if (num7 > 0)
			{
				itemText.name = itemText.name + num7.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Silver"));
			}
			if (num6 > 0)
			{
				itemText.name = itemText.name + num6.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Copper"));
			}
			if (itemText.name.Length > 1)
			{
				itemText.name = itemText.name.Substring(0, itemText.name.Length - 1);
			}
		}
		#endregion
		private void HookUpdateItem(ILContext il)
        {
            var c = new ILCursor(il);

            var label = il.DefineLabel();

            c.Emit(Ldarg_0);

            c.EmitDelegate<Action<int>>(delegate (int i)
            {
                if (!item.beingGrabbed)
                {
					if (Terraria.Main.netMode != 2 && Terraria.Main.expertMode && item.owner == Terraria.Main.myPlayer && item.type == ModContent.ItemType<OblivionCoin>())
					{
						Rectangle rectangle = new Rectangle((int)item.position.X, (int)item.position.Y, item.width, item.height);
						for (int k = 0; k < 200; k++)
						{
							if (Terraria.Main.npc[k].active && Terraria.Main.npc[k].lifeMax > 5 && !Terraria.Main.npc[k].friendly && !Terraria.Main.npc[k].immortal && !Terraria.Main.npc[k].dontTakeDamage)
							{
								float num6 = (float)item.stack;
								float num7 = 1f;
								if (item.type == ModContent.ItemType<OblivionCoin>())
								{
									num7 = 100000000f;
								}
								num6 *= num7;
								float extraValue = Terraria.Main.npc[k].extraValue;
								int num8 = Terraria.Main.npc[k].realLife;
								if (num8 >= 0 && Terraria.Main.npc[num8].active)
								{
									extraValue = Terraria.Main.npc[num8].extraValue;
								}
								else
								{
									num8 = -1;
								}
								if (extraValue < num6)
								{
									Rectangle rectangle2 = new Rectangle((int)Terraria.Main.npc[k].position.X, (int)Terraria.Main.npc[k].position.Y, Terraria.Main.npc[k].width, Terraria.Main.npc[k].height);
									if (rectangle.Intersects(rectangle2))
									{
										float num9 = (float)Terraria.Main.rand.Next(50, 76) * 0.01f;
										if (num9 > 1f)
										{
											num9 = 1f;
										}
										int num10 = (int)((float)item.stack * num9);
										if (num10 < 1)
										{
											num10 = 1;
										}
										if (num10 > item.stack)
										{
											num10 = item.stack;
										}
										item.stack -= num10;
										float num11 = (float)num10 * num7;
										int num12 = k;
										if (num8 >= 0)
										{
											num12 = num8;
										}
										Terraria.Main.npc[num12].extraValue += num11;
										if (Terraria.Main.netMode == 0)
										{
											Terraria.Main.npc[num12].moneyPing(item.position);
										}
										else
										{
											Terraria.NetMessage.SendData(92, -1, -1, null, num12, num11, item.position.X, item.position.Y, 0, 0, 0);
										}
										if (item.stack <= 0)
										{
											item.SetDefaults(0, false);
											item.active = false;
										}
										Terraria.NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
									}
								}
							}
						}
					}
				}
            });
		}
#pragma warning restore ChangeMagicNumberToID // Change magic numbers into appropriate ID values

        private void HookSetupShop(ILContext il) // credit to lolxd for helping
        {
            var c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchLdcI4(1000000))) // this method of pushing into the code is kinda ech but it should work as long as no other mods mess with anything here
                return;

            c.Index += 6;

            c.Emit(Ldloc, 9); // num4
            c.Emit(Ldloc, 10); // num5

            c.EmitDelegate<Func<long, int, long>>(delegate (long num4, int num5)
            {
                if (Terraria.Main.player[Terraria.Main.myPlayer].inventory[num5].type == ModContent.ItemType<Items.Currency.OblivionCoin>())
                {
                    return num4 += Terraria.Main.player[Terraria.Main.myPlayer].inventory[num5].stack * 100000000;
                }
                return num4;
            });

            c.Emit(Stloc, 9);
        }
		#endregion

		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oblivion Coin");
			//ItemID.Sets.IsAMaterial[ModContent.ItemType<OblivionCoin>()] = false;
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PlatinumCoin);
            item.value = 100 * 100 * 100 * 100 * 5;
        }

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			Color color = Lighting.GetColor((int)((double)item.position.X + (double)item.width * 0.5) / 16, (int)((double)item.position.Y + (double)item.height * 0.5) / 16);
			if (!Main.gamePaused && (double)(Math.Abs(item.velocity.X) + Math.Abs(item.velocity.Y)) > 0.2)
			{
				float num = (float)Main.rand.Next(500) - (Math.Abs(item.velocity.X) + Math.Abs(item.velocity.Y)) * 20f;
				int num2 = item.type - 72;
				num -= (float)(num2 * 20);
				int type = 244 + item.type - 71;
				if (item.isBeingGrabbed)
				{
					num /= 100f;
				}
				if (num < (float)((int)color.R / 70 + 1))
				{
					int num17 = Dust.NewDust(item.position - new Vector2(1f, 2f), item.width, item.height, type, 0f, 0f, 254, default(Microsoft.Xna.Framework.Color), 0.25f);
					Main.dust[num17].velocity *= 0f;
				}
			}
			return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
		}

		public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumCoin, 100);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}