using Terraria;
using Terraria.ModLoader;

namespace Heylookamod.Items.JimDrops
{
    public class JimExpert : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulcanian Wrath");
            Tooltip.SetDefault("Increased mobility and stats while submerged in lava."
                + "\nImmunity to lava."
                + "\nTaking damage causes a small burst of red-hot rock shards."
                + "\nATTENTION, THIS ACCESSORY WAS MADE BY OUR GREAT GOD AND SAVIOR, HERO.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 44;
            item.value = Item.sellPrice(gold: 4);
            item.rare = -12;
            item.accessory = true;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<HeylookamodPlayer>().JimExpert = true;
            player.lavaImmune = true;
            player.buffImmune[24] = true;
            if (player.lavaWet == true)
            {
                player.meleeDamage *= 1.2f;
                player.thrownDamage *= 1.2f;
                player.rangedDamage *= 1.2f;
                player.magicDamage *= 1.2f;
                player.minionDamage *= 1.2f;
                player.statDefense += 5;
                player.releaseJump = true;
                player.accFlipper = true;
                player.ignoreWater = true;
                player.GetModPlayer<HeylookamodPlayer>().JimExpertLava = true;
            }
        }
    }
}