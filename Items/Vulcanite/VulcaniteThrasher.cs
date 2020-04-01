using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items.Vulcanite   //where is located
{
	public class VulcaniteThrasher : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vulcanite Thrasher");
			Tooltip.SetDefault("Create a flail to thrash your own ass.");
		}
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.SolarEruption);

			item.damage = 65;            //Sword damage
			item.melee = true;            //if it's melee
			item.width = 46;              //Sword width
			item.height = 50;             //Sword height
			item.knockBack = 5;      //Sword knockback
			item.value = Item.sellPrice(gold: 2);
			item.rare = 7;
			item.autoReuse = true;   //if it's capable of autoswing.
			item.useTurn = false;
			item.shoot = mod.ProjectileType("VulcaniteThrasherP");
			item.reuseDelay = 5;
		}
		public override void AddRecipes()  //How to craft this sword
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddTile(TileID.MythrilAnvil);   //at work bench
			recipe.AddIngredient(mod.ItemType("VulcaniteBar"), 16);
			recipe.AddIngredient(ItemID.HellstoneBar, 5);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}
