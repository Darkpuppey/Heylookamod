using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
	public class JimBallFriendly : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jimothy Ball Friendly");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Fireball);
			projectile.width = 16;               //The width of projectile hitbox
			projectile.height = 16;              //The height of projectile hitbox
			projectile.friendly = true;         //Can the projectile deal damage to the player?
			projectile.hostile = false;
			projectile.alpha = 0;
		}
	}
}

