using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items.JimDrops
{
	public class JimBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jimbo");
			Tooltip.SetDefault("What? You thought I'd be serious about this?"
								+ "\nConverts wooden arrows into vulcanite arrows.");
		}

		public override void SetDefaults()
		{
			item.damage = 40;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 5;
			item.useAnimation = 15;
			item.reuseDelay = 15;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = Item.sellPrice(gold: 2);
			item.rare = 8;
			item.autoReuse = true;
			item.shoot = 1;
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Arrow;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = mod.ProjectileType("VulcaniteArrow");
			}
			Main.PlaySound(SoundID.DD2_FlameburstTowerShot);
			return true;
		}
	}
}
