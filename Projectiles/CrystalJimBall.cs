using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
	// to investigate: Projectile.Damage, (8843)
	class CrystalJimBall : ModProjectile
	{
		public override void SetDefaults()
		{
			// while the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
			projectile.width = 20;
			projectile.height = 26;
			projectile.friendly = true;
			projectile.penetrate = 1;

			// 5 second fuse.
			projectile.timeLeft = 300;

			// These 2 help the projectile hitbox be centered on the projectile sprite.
			drawOffsetX = 5;
			drawOriginOffsetY = 5;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();
			return true;
		}

		public override void AI()
		{
			// Smoke and fuse dust spawn.
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 161, new Color(255, 255, 255), 1f);
			}
			projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			// Rotation increased by velocity.X 
			projectile.rotation += projectile.velocity.X * 0.1f;
			return;
		}

		public override void Kill(int timeLeft)
		{
			// If we are the original projectile, spawn the 5 child projectiles
			if (projectile.ai[1] == 0)
			{
				for (int i = 0; i < 5; i++)
				{
					// Random upward vector.
					Vector2 vel = new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-5, -4));
					Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("JimShard"), projectile.damage, projectile.knockBack, projectile.owner, 0, 1);
				}
			}
			// Play explosion sound
			Main.PlaySound(SoundID.Shatter, projectile.position);
			// Smoke Dust spawn
			for (int i = 0; i < 50; i++)
			{
				int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 161, new Color(255, 255, 255), 0.8f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}
	}
}
