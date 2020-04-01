using Terraria.ModLoader;

namespace Heylookamod.Items.JimDrops
{
	public class LavaGlob : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lava Glob");
			Tooltip.SetDefault("Smh Javyz");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 10;
			item.height = 14;
			item.rare = 8;
			item.value = 0;
		}
	}
}