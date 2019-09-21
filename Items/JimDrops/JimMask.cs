using Terraria.ModLoader;

namespace Heylookamod.Items.JimDrops
{
    [AutoloadEquip(EquipType.Head)]
    public class JimMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.rare = 1;
            item.vanity = true;
        }

        public override bool DrawHead()
        {
            return false;
        }
    }
}