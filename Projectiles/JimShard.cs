using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
    public class JimShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jim Shard");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GreekFire1);
            projectile.width = 12;               //The width of projectile hitbox
            projectile.height = 8;              //The height of projectile hitbox
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.friendly = true;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 161, new Color(255, 255, 255), 0.5f);
        }
    }
}

