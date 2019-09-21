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
    public class StrangeFlowerWeapon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The First Golden Flower");
            Tooltip.SetDefault("I don't think it likes you very much");
            //DisplayName.SetDefault("The First Golden Flower");
            //Tooltip.SetDefault("\n[c/ff0000:Finally, it bends to our will. Perhaps now it will know who the TRUE god of this world is.]");
            //Post Plantera Info
        }
        public override void SetDefaults()
        {
            item.damage = 75; //75
            item.width = 32;
            item.height = 42;
            item.useTime = 3;
            item.useAnimation = 5;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = 10000;
            item.rare = 8;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/FloweyHurt");
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("PelletHoming");
            item.shootSpeed = 2f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (NPC.downedPlantBoss)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Pellet"), 10, knockBack, player.whoAmI);
            }
            else
            {
                player.statLife = (player.statLife - 50);
                if (player.statLife <= 0 && Main.myPlayer == player.whoAmI)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " got a taste of LOVE"), 10, 0, false);
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/FloweyLaugh"));
                }
            }
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 3);
        }
    }
}
