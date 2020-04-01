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
				// Here is where you define stuff
				yabhb.Call("hbStart");
				yabhb.Call("hbSetTexture",
	GetTexture("Healthbars/JimBarStart"),
	GetTexture("Healthbars/JimBarMiddle"),
	GetTexture("Healthbars/JimBarEnd"),
	GetTexture("Healthbars/Phil"));
				yabhb.Call("hbSetMidBarOffset", -29, 10);
				yabhb.Call("hbSetBossHeadCentre", 78, 31);
				yabhb.Call("hbSetFillDecoOffsetSmall", 10);
				yabhb.Call("hbFinishSingle", NPCType("JimHead"));
			}
			if (bossChecklist != null)
			{
				//bossChecklist.Call(....
				// To include a description:
				bossChecklist.Call("AddBossWithInfo", "Jim", 10.5f, (Func<bool>)(() => HeylookamodWorld.downedJim), "Use a [i:" + ItemType("JimSpawner") + "] in Hell after Plantera has been defeated.");
			}
		}
		public Heylookamod()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true,
				AutoloadBackgrounds = true
			};
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
				float OvergrowthStrength = HeylookamodWorld.FloweyTiles / 200f;
				OvergrowthStrength = Math.Min(OvergrowthStrength, 1f);

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
