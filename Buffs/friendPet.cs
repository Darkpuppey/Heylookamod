using Terraria;
using Terraria.ModLoader;

namespace Heylookamod.Buffs
{
	public class friendPet : ModBuff
	{
		public override void SetDefaults()
		{
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			DisplayName.SetDefault("The Power of Friendship");
			Description.SetDefault("This is such a bad cliche why did you add i-");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<HeylookamodPlayer>().friendPet = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("Orange")] <= 0;
			petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("Vortex")] <= 0;
			petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("RescueBoat")] <= 0;
			petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("Kirby")] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("Orange"), 0, 0f, player.whoAmI, 0f, 0f);
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("Vortex"), 0, 0f, player.whoAmI, 0f, 0f);
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("RescueBoat"), 0, 0f, player.whoAmI, 0f, 0f);
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("Kirby"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}