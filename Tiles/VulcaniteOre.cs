using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Heylookamod.Tiles
{
	public class VulcaniteOre : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = 55;
            soundType = SoundID.Tink;
            drop = mod.ItemType("VulcaniteOre");
            minPick = 200;
            AddMapEntry(new Color(255, 245, 96));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
            num = fail ? 4 : 5;
        }

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 0.9f;
			b = 0.4f;
		}
	}
}