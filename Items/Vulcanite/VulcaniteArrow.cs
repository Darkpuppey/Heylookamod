using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items.Vulcanite
{
	public class VulcaniteArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This might leave a burn.");
		}

		public override void SetDefaults()
		{
			item.damage = 14;
			item.ranged = true;
			item.width = 14;
			item.height = 32;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 20;
			item.rare = 7;
			item.shoot = mod.ProjectileType("VulcaniteArrow");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 10f;                  //The speed of the projectile
			item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenArrow, 250);
			recipe.AddIngredient(mod.ItemType("VulcaniteBar"));
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 250);
			recipe.AddRecipe();
		}
	}
}
