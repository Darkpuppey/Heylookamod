using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
	public class ClownCopter : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 2;
			DisplayName.SetDefault("ClownCopter"); // Automatic from .lang files
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.ZephyrFish);
			aiType = ProjectileID.ZephyrFish;
			projectile.width = 36;
			projectile.height = 34;
		}

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.zephyrfish = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			HeylookamodPlayer modPlayer = player.GetModPlayer<HeylookamodPlayer>();
			if (player.dead)
			{
				modPlayer.friendPet = false;
			}
			if (modPlayer.friendPet)
			{
				projectile.timeLeft = 2;
			}
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 2)
				{
					projectile.frame = 0;
				}
			}
		}
	}
}