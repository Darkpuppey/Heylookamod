using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items
{
    public class FriendPet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
            DisplayName.SetDefault("Orb of Gratitude");
            Tooltip.SetDefault("Summons a group of strange and interesting entities to keep you company!");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ZephyrFish);
            item.shoot = mod.ProjectileType("Orange");
            item.shoot = mod.ProjectileType("Vortex");
            item.shoot = mod.ProjectileType("RescueBoat");
            item.shoot = mod.ProjectileType("Kirby");
            item.buffType = mod.BuffType("friendPet");
            item.width = 36;
            item.height = 38;
            item.rare = -12;
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}