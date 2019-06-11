using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Heylookamod.Items
{
    public class PetalPusher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Petal Pusher");
            Tooltip.SetDefault("You'll be pushing a lot more than a few petals with this.");
        }
        public override void SetDefaults()
        {
            item.damage = 15;
            item.crit = 10;
            item.ranged = true;
            item.width = 38;
            item.height = 46;
            item.useTime = 15;
            item.useAnimation = 15;
            item.reuseDelay = 1;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 10;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = new Terraria.Audio.LegacySoundStyle(SoundID.Grass, 0);
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Petal");
            item.shootSpeed = 20f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(7); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                                                                                                                // If you want to randomize the speed to stagger the projectiles
                float scale = 1f - (Main.rand.NextFloat() * .10f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(ItemID.GrassSeeds, 100);
            recipe.AddIngredient(null, "Peashooter");
            recipe.AddIngredient(ItemID.FallenStar, 2);
            recipe.AddTile(TileID.ClayPot);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 3);
        }
    }
}
