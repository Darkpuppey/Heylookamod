using Terraria;
using Terraria.ModLoader;

namespace Heylookamod.Items.Vulcanite
{
	public class VulcaniteOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vulcanite Ore");
			Tooltip.SetDefault("As sharp as a blade and as hot as the sun.");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.rare = 7;
			item.value = Item.sellPrice(silver: 6);
			item.consumable = true;
			item.createTile = mod.TileType("VulcaniteOre");
		}
	}
}