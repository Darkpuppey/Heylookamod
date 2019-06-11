using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Heylookamod.Items
{
    public class ErrorGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unloaded Item");
            Tooltip.SetDefault("99 little bugs in the code"
                + "\n99 little bugs"
                + "\nTake one down, patch it around"
                + "\n117 little bugs in the code");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.ranged = true;
            item.width = 40;
            item.height = 24;
            item.useTime = 5;
            item.useAnimation = 5;
            item.reuseDelay = 0;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = Item.sellPrice(gold: 10);
            item.rare = -12;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("GodDammit");
            item.shootSpeed = 30f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Tink);
            return true;
        }
    }
    public class GodDammit : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            DisplayName.SetDefault("The Damn Bugs");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.LaserMachinegunLaser);
            projectile.width = 78;               //The width of projectile hitbox
            projectile.height = 18;              //The height of projectile hitbox
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.alpha = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return true;
        }

        public bool chosenframe = false;
        public override void AI()
        {
            projectile.velocity *= 1.1f;
            if (chosenframe == false)
            {
                projectile.frame = Main.rand.Next(4);
                chosenframe = true;
            }
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            GetAlpha(Main.DiscoColor);
        }
    }
}
