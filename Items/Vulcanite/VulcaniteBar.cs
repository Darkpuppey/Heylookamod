using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace Heylookamod.Items.Vulcanite
{
    public class VulcaniteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulcanite Bar");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 999;
            item.rare = 7;
            item.value = Item.sellPrice(silver: 80);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VulcaniteOre"), 5);
            recipe.AddIngredient(mod.ItemType("MoltenEssence"), 10);
            recipe.AddTile(TileID.AdamantiteForge);

            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }
}