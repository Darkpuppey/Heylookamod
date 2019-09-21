using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
    public class VulcaniteArrow : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulcanite Arrow");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            projectile.penetrate = -1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center, projectile.velocity * 0, mod.ProjectileType("VulcaniteArrowExplosion"), projectile.damage, 5, Main.myPlayer, 0, 0);
        }
        public override void AI()
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 161, new Color(255, 255, 255), 0.5f);
            if (projectile.timeLeft <= 3)
            {
                Projectile.NewProjectile(projectile.Center, projectile.velocity * 0, mod.ProjectileType("VulcaniteArrowExplosion"), projectile.damage, 5, Main.myPlayer, 0, 0);
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center, projectile.velocity * 0, mod.ProjectileType("JimExplosion"), projectile.damage, 5, Main.myPlayer, 0, 0);
        }
    }
}
