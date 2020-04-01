using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items.Vulcanite
{
	public class VulcaniteCrusher : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vulanite Crusher");
			Tooltip.SetDefault("Hot enough to break through even the toughest vines and rocks.");
		}

		public override void SetDefaults()
		{
			item.damage = 35;
			item.melee = true;
			item.width = 38;
			item.height = 38;
			item.useTime = 7;
			item.useAnimation = 15;
			item.pick = 205;
			item.useStyle = 1;
			item.knockBack = 5;
			item.value = Item.sellPrice(gold: 2);
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("VulcaniteBar"), 18);
			recipe.AddIngredient(ItemID.HellstoneBar, 7);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 55, 0f, 0f, 161, new Color(255, 255, 255), 0.6f);
		}
	}
}