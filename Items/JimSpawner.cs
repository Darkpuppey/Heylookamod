using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Items
{
    //imported from my tAPI mod because I'm lazy
    public class JimSpawner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Hot Pearl");
            Tooltip.SetDefault("It's as hot as lava.");
        }

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 20;
            item.maxStack = 20;
            item.rare = 10;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            // "player.ZoneUnderworldHeight" could also be written as "player.position.Y / 16f > Main.maxTilesY - 200"
            return NPC.downedPlantBoss && player.ZoneUnderworldHeight && !NPC.AnyNPCs(mod.NPCType("JimHead"));
        }
        public int pCenterY;
        public override bool UseItem(Player player)
        {
            pCenterY = (int)player.Center.Y;
            NPC.NewNPC((int)player.Center.X, (pCenterY - 2500), mod.NPCType("JimHead"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            Main.NewText("[c/FFA500:Battle Jim has awoken!]");
            Main.NewText("[c/d5ff82:Battle Theme of Jim:] XI - Solar Storm");
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MoltenEssence"), 15);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}