using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace Heylookamod.Projectiles
{
	public abstract class GhostLevi : ModProjectile
	{
        public bool head;
        public bool tail;
        public int minLength;
        public int maxLength;
        public int headType;
        public int bodyType;
        public int tailType;
        public bool flies = true;
        public bool directional = false;
        public float speed;
        public float turnSpeed;
        public override void SetStaticDefaults()
		{
			 DisplayName.SetDefault("GhostHead"); // Automatic from .lang files
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.StardustDragon1);
			aiType = ProjectileID.StardustDragon1;
            projectile.width = 30;
            projectile.height = 24;
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