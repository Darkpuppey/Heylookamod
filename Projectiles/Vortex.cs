using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
	public class Vortex : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			 DisplayName.SetDefault("Vortex"); // Automatic from .lang files
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BabySkeletronHead);
			aiType = ProjectileID.BabySkeletronHead;
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
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position + projectile.velocity, 10, 10, 274, 0, projectile.velocity.Y * -0.9f);
            }
        }
	}
}