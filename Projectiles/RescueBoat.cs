using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
    public class RescueBoat : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("RescueBoat"); // Automatic from .lang files
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BabyHornet);
            aiType = ProjectileID.BabyHornet;
            projectile.width = 38;
            projectile.height = 38;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
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
        }
    }
}