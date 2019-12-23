using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace Heylookamod.Projectiles
{
    public class HowDoYouDodgeThisInUndertaleTbh : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nuke");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            projectile.width = 26;               //The width of projectile hitbox
            projectile.height = 46;              //The height of projectile hitbox
            projectile.friendly = true;         //Can the projectile deal damage to the player?
            projectile.hostile = false;
            projectile.alpha = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 10000;
        }

        public int Timer = 0;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.owner == Main.myPlayer)
            {
                projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                projectile.alpha = 255;
                // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.width = 150;
                projectile.height = 150;
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                projectile.knockBack = 5f;
                projectile.timeLeft = 3;
            }
        }
        public override void AI()
        {
            if(Timer == 0)
            {
                Main.PlaySound (SoundID.DD2_BookStaffCast);
                Timer++;
            }
            BaseAI.Look(projectile, 90);
            projectile.velocity.X = 0;
            if (Timer <= 20)
            {
                Timer++;
                projectile.velocity *= 0.95f;
            }
            else 
            if (Timer == 20)
            {
                projectile.velocity *= 10f;
                Timer++;
            }
            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
            {
                projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                projectile.alpha = 255;
                // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.width = 150;
                projectile.height = 150;
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                projectile.knockBack = 5f;
            }
        }

        public override void Kill(int timeLeft)
        {
            // Play explosion sound
            Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
        }
    }
}

