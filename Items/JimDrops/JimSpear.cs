using System;
using Heylookamod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Heylookamod.Items.JimDrops
{
	public class JimSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Hades' Spear");
			Tooltip.SetDefault("It may not be as powerful as the real thing, but its enough to get the job done.");
		}

		public override void SetDefaults()
		{
			item.damage = 100;
			item.useStyle = 5;
			item.useAnimation = 18;
			item.useTime = 24;
			item.shootSpeed = 3.7f;
			item.knockBack = 6.5f;
			item.width = 32;
			item.height = 32;
			item.scale = 1f;
			item.rare = 8;
			item.value = Item.sellPrice(gold: 2);

			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<JimSpearP>();
		}

        public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1; 
		}
	}
}
