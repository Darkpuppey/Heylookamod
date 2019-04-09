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
    public class Peashooter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Peashooter");
            Tooltip.SetDefault("This one had a bit too much sunlight.");
        }
        public override void SetDefaults()
        {
            item.damage = 1;
            item.crit = 10;
            item.ranged = true;
            item.width = 28;
            item.height = 38;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 10;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/FloweyHurt");
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("Pea");
            item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 5 + Main.rand.Next(7); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
                                                                                                                // If you want to randomize the speed to stagger the projectiles
                float scale = 1f - (Main.rand.NextFloat() * .10f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false; // return false because we don't want tmodloader to shoot projectile
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 20);
            recipe.AddIngredient(ItemID.Acorn);
            recipe.AddIngredient(ItemID.Sunflower, 2);
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
