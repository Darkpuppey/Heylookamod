using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"
using Terraria.Localization;

namespace Heylookamod.Items
{
	public class StrangeFlowerSpawner : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This better fucking work. - Darkpuppey");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("StrangeFlower");
		}
	}
}
