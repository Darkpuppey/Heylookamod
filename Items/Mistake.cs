using Heylookamod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items
{
	public class Mistake : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Dungeon");
			Tooltip.SetDefault("The Guardian was already taken."
				+ "\nBosses are only damaged for half of this weapon's damage.");

			// These are all related to gamepad controls and don't seem to affect anything else
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.width = 22;
			item.height = 42;
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 0.1f;
			item.damage = 9999;
			item.rare = -12;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = Item.sellPrice(platinum: 1);
			item.shoot = ModContent.ProjectileType<MistakeP>();
		}
	}
}
