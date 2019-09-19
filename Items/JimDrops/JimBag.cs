using Terraria;
using Terraria.ModLoader;

namespace Heylookamod.Items.JimDrops
{
    public class JimBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 36;
            item.height = 32;
            item.rare = 9;
            item.expert = true;
        }

        public override int BossBagNPC => mod.NPCType("JimHead");
        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            int choice = Main.rand.Next(3);
            if (Main.rand.Next(100) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("LavaGlob"));
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("JimMask"));
            }
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("JimSpear"));
                player.QuickSpawnItem(mod.ItemType("JimStaff"));
                player.QuickSpawnItem(mod.ItemType("JimBow"));
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("JimSpear"));
                player.QuickSpawnItem(mod.ItemType("JimSword"));
                player.QuickSpawnItem(mod.ItemType("JimBow"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("JimStaff"));
                player.QuickSpawnItem(mod.ItemType("JimSword"));
                player.QuickSpawnItem(mod.ItemType("JimBow"));
            }
            player.QuickSpawnItem(mod.ItemType("JimExpert"));
        }
    }
}