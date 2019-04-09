using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items.JimDrops
{
	public class JimStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Vulcan's Fury");
			Tooltip.SetDefault("You get 14 years of bad luck every time you use this thing. Have fun.");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 55;
			item.magic = true;
			item.mana = 30;
			item.width = 66;
			item.height = 56;
			item.useTime = 15;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 20000;
			item.rare = 8;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("CrystalJimBall");
			item.shootSpeed = 12f;
		}
	}
}