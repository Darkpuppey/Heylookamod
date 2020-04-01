using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
	public class JimBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jim Ball");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.EyeFire);
			projectile.width = 16;               //The width of projectile hitbox
			projectile.height = 16;              //The height of projectile hitbox
			projectile.hostile = true;         //Can the projectile deal damage to the player?
			projectile.alpha = 0;
			projectile.timeLeft = 60;
		}
	}
}

