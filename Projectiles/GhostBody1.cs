using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
	public class GhostBody1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			 DisplayName.SetDefault("GhostBody1"); // Automatic from .lang files
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.StardustDragon2);
			aiType = ProjectileID.StardustDragon2;
            projectile.width = 30;
            projectile.height = 14;
            projectile.alpha = 0;
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			HeylookamodPlayer modPlayer = player.GetModPlayer<HeylookamodPlayer>(mod);
			if (player.dead)
			{
				modPlayer.friendPet = false;
			}
			if (modPlayer.friendPet)
			{
				projectile.timeLeft = 2;
			}
        }
	}
}