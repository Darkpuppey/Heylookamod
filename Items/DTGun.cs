using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items
{
    public class DTGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Extractor");
            Tooltip.SetDefault("Seems familiar...");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.mana = 10;
            item.width = 54;
            item.height = 52;
            item.useTime = 5;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 5;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DT");
            item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            return true;
        }
    }
    public class DT : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Essence");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.FlowerPetal);
            projectile.width = 16;               //The width of projectile hitbox
            projectile.height = 16;              //The height of projectile hitbox
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.alpha = 255;
            projectile.timeLeft = 30;
        }

        public float timer = 10;
        public override void AI()
        {
            timer *= 0.95f;
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 16, 0f, 0f, 125, new Color(255, 255, 255), timer / 2)];
                dust.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
            }
        }
    }
}