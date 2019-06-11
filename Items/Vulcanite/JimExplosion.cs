using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace Heylookamod.Items.Vulcanite
{
    // to investigate: Projectile.Damage, (8843)
    class JimExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.scale = 1.5f;
            projectile.width = 76;
            projectile.height = 58;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 6000;
            projectile.alpha = 125;
            // These 2 help the projectile hitbox be centered on the projectile sprite.
            drawOffsetX = 5;
            drawOriginOffsetY = 5;
            projectile.tileCollide = false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
            if (Main.expertMode)
            {
                if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
                {
                    damage /= 5;
                }
            }
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 2)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 8)
                {
                    projectile.timeLeft = 0;
                }
            }
        }
    }
}