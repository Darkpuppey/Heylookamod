using Heylookamod.Items;
using Heylookamod.Items.JimDrops;
using Heylookamod.NPCs.Jim;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Heylookamod
{
	class Heylookamod : Mod
	{
		internal static Heylookamod instance;
		public static Heylookamod self = null;
		public static IDictionary<string, Texture2D> Textures = null;
		public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();

		public override void PostSetupContent()
		{
			Mod bossChecklist = ModLoader.GetMod("BossChecklist");
			Mod yabhb = ModLoader.GetMod("FKBossHealthBar");

			if (yabhb != null)
			{
				//Health bar from Yet Another Boss Healthbar
				yabhb.Call("hbStart"); //start
				yabhb.Call("hbSetTexture", GetTexture("HealthBars/JimBarStart"), GetTexture("HealthBars/JimBarMiddle"), GetTexture("HealthBars/JimBarEnd"), GetTexture("HealthBars/Phil")); //set textures
				yabhb.Call("hbSetMidBarOffset", -29, 10); //set offset
				yabhb.Call("hbSetBossHeadCentre", 78, 31); //set boss head center
				yabhb.Call("hbSetFillDecoOffsetSmall", 10); //set fill offset
				yabhb.Call("hbFinishSingle", ModContent.NPCType<JimHead>()); //finish call and set npc
			}

			if (bossChecklist != null)
			{
				bossChecklist.Call("AddBoss", 10.5f, new List<int>() { ModContent.NPCType<JimHead>(), ModContent.NPCType<JimBody>(), ModContent.NPCType<JimTail>() }, this, "Jim", (Func<bool>)(() => HeylookamodWorld.downedJim), ModContent.ItemType<JimSpawner>(), new List<int>() { ModContent.ItemType<JimMask>(), ModContent.ItemType<JimTrophy>() }, new List<int>() { ModContent.ItemType<JimBag>(), ModContent.ItemType<JimBow>(), ModContent.ItemType<JimExpert>(), ModContent.ItemType<JimExpertAnkh>(), ModContent.ItemType<JimSpear>(), ModContent.ItemType<JimStaff>(), ModContent.ItemType<JimSword>(), ModContent.ItemType<LavaGlob>() }, "Use a [i:" + ModContent.ItemType<JimSpawner>() + "] in Hell after Plantera has been defeated.", "Jim beat your ass");
			}
		}

		public Heylookamod()
		{
			instance = this;
		}

		public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
			{
				// Make sure your logic here goes from lowest priority to highest so your intended priority is maintained.
				if (Main.LocalPlayer.GetModPlayer<HeylookamodPlayer>().Overgrowth)
				{
					music = GetSoundSlot(SoundType.Music, "Sounds/Music/Overgrowth");
					priority = MusicPriority.BiomeHigh;
				}
			}
		}

		public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
		{
			if (HeylookamodWorld.FloweyTiles > 0)
			{
				float OvergrowthStrength = Math.Min(HeylookamodWorld.FloweyTiles / 200f, 1f);

				int sunR = backgroundColor.R;
				int sunG = backgroundColor.G;
				int sunB = backgroundColor.B;

				// Remove some green and more red.
				sunR -= (int)(50f * OvergrowthStrength * (backgroundColor.R / 255f));
				sunB -= (int)(90f * OvergrowthStrength * (backgroundColor.B / 255f));

				sunR = Utils.Clamp(sunR, 15, 255);
				sunG = Utils.Clamp(sunG, 15, 255);
				sunB = Utils.Clamp(sunB, 15, 255);

				backgroundColor.R = (byte)sunR;
				backgroundColor.G = (byte)sunG;
				backgroundColor.B = (byte)sunB;
			}
		}
	}
}