using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items.JimDrops
{
	public class JimExpertAnkh : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vulcanian Clarity");
			Tooltip.SetDefault("Increased mobility and stats while submerged in lava."
				+ "\nTaking damage causes a small burst of red-hot rock shards."
				+ "\nGrants immunity to knockback, fire blocks and lava."
				+ "\nGrants immunity to most debuffs");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 44;
			item.value = Item.sellPrice(gold: 10);
			item.rare = -12;
			item.accessory = true;
			item.expert = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<HeylookamodPlayer>().JimExpert = true;
			player.lavaImmune = true;
			player.buffImmune[46] = true;
			player.noKnockback = true;
			player.fireWalk = true;
			player.buffImmune[33] = true;
			player.buffImmune[36] = true;
			player.buffImmune[30] = true;
			player.buffImmune[20] = true;
			player.buffImmune[32] = true;
			player.buffImmune[31] = true;
			player.buffImmune[35] = true;
			player.buffImmune[23] = true;
			player.buffImmune[22] = true;
			player.buffImmune[24] = true;
			if (player.lavaWet == true)
			{
				player.meleeDamage *= 1.2f;
				player.thrownDamage *= 1.2f;
				player.rangedDamage *= 1.2f;
				player.magicDamage *= 1.2f;
				player.minionDamage *= 1.2f;
				player.statDefense += 5;
				player.releaseJump = true;
				player.accFlipper = true;
				player.ignoreWater = true;
				player.GetModPlayer<HeylookamodPlayer>().JimExpertLava = true;
			}
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("JimExpert"));
			recipe.AddIngredient(ItemID.AnkhShield);
			recipe.AddTile(TileID.TinkerersWorkbench);

			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}