using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
    public class JimFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jim Flame");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.width = 22;               //The width of projectile hitbox
            projectile.height = 12;              //The height of projectile hitbox
            projectile.hostile = true;         //Can the projectile deal damage to the player?
            projectile.friendly = false;
            projectile.alpha = 0;
            projectile.tileCollide = false;
            projectile.timeLeft = 999;
        }
        public override void AI()
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Main.dust[Dust.NewDust(position, 30, 30, 55, 0f, 0f, 161, new Color(255, 255, 255), 1f)];
                dust.shader = GameShaders.Armor.GetSecondaryShader(7, Main.LocalPlayer);
            }
            projectile.alpha += 2 + Main.rand.Next(3);
            if (projectile.alpha >= 254)
            {
                projectile.Kill();
            }
        }
    }
}

