using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Heylookamod.Items
{
	public class SomeoneIsGonnaLookAtTheSourceSomedayAndBeSoConfused : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Omega Vessel");
			Tooltip.SetDefault("Smells like explosives");
		}

		public override void SetDefaults()
		{
			item.damage = 100;
			item.magic = true;
			item.mana = 10;
			item.width = 36;
			item.height = 30;
			item.useTime = 2;
			item.useAnimation = 30;
			item.useStyle = 4;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 10;
			item.value = 100000;
			item.rare = 6;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("HowDoYouDodgeThisInUndertaleTbh");
			item.shootSpeed = 1f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X + Main.rand.Next(1980) - 990, position.Y - 600, speedX, speedY, type, damage, knockBack, player.whoAmI);
			return false;
		}
	}
}