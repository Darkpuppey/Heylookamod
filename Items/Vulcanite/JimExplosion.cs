using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items.Vulcanite
{
	// to investigate: Projectile.Damage, (8843)
	class JimExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 8;
		}
		public override void SetDefaults()
		{
			projectile.scale = 1.5f;
			projectile.width = 76;
			projectile.height = 58;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 6000;
			projectile.alpha = 125;
			// These 2 help the projectile hitbox be centered on the projectile sprite.
			drawOffsetX = 5;
			drawOriginOffsetY = 5;
			projectile.tileCollide = false;
			projectile.localNPCHitCooldown = -1;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 1;
		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			// Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
			if (Main.expertMode)
			{
				if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
				{
					damage /= 5;
				}
			}
		}
		int Explosion = 0;
		public override void AI()
		{
			if (++projectile.frameCounter >= 2)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 8)
				{
					projectile.timeLeft = 0;
				}
			}
			if (Explosion == 0)
			{
				// Play explosion sound
				Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, projectile.position);
				// Smoke Dust spawn
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				// Fire Dust spawn
				for (int i = 0; i < 80; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 5f;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[dustIndex].velocity *= 3f;
				}
				Explosion = +1;
			}
		}
	}
}