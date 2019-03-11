using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
    public class Kirby : ModProjectile
    {
        private int backFrames = 0;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 8;
            DisplayName.SetDefault("Kirby"); // Automatic from .lang files
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.TikiSpirit);
            aiType = ProjectileID.TikiSpirit;
            projectile.width = 40;
            projectile.height = 40;
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
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 8)
                {
                    projectile.frame = 0;
                    backFrames = 0;
                }
                if (projectile.frame == 6 && backFrames < 5)
                {
                    backFrames++;
                    projectile.frame--;
                }
            }
        }
    }
}