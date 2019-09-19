using Terraria.ModLoader;

namespace Heylookamod.Items
{
	public class MoltenEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Molten Essence");
			Tooltip.SetDefault("It pulses with heat.");
		}
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 18;
            item.height = 18;
            item.rare = 7;
        }
    }
}