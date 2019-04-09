using Terraria;
using Terraria.ModLoader;

namespace Heylookamod.Items
{
    [AutoloadEquip(EquipType.Wings)]
    public class GonerWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("They pulse with power with every leap you take.");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 10000;
            item.rare = -12;
            item.accessory = true;
        }
        //these wings use the same values as the solar wings
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 260;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.9f;
            ascentWhenRising = 0.2f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 4f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 10f;
            acceleration *= 2.5f;
        }
    }
}