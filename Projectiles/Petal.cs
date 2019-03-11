using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Projectiles
{
    public class Petal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Petal");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.width = 18;               //The width of projectile hitbox
            projectile.height = 16;              //The height of projectile hitbox
            projectile.friendly = true;         //Can the projectile deal damage to enemies?
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.ranged = true;           //Is the projectile shoot by a ranged weapon?
            projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            projectile.timeLeft = 300;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds) 
            projectile.alpha = 0;
            projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            projectile.tileCollide = true;          //Can the projectile collide with tiles?
            projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
         public override void AI()
        {
            BaseMod.BaseAI.Look(projectile, 0);
            projectile.velocity *= 0.95f;
            projectile.alpha += 5;
            if (projectile.alpha >= 254)
            {
                projectile.Kill();
            }
        }
    }
}