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
            item.ranged = true;
            item.width = 44;
            item.height = 54;
            item.useTime = 30;
            item.useAnimation = 30;
            item.reuseDelay = 1;
            item.useStyle = 5;
            item.noMelee = true; 
            item.knockBack = 2;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/FloweyHurt");
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Pea");
            item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 3);
        }
    }
}
