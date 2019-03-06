using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Heylookamod.Tiles
{
	public class Crystal : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            soundType = 21;
            drop = mod.ItemType("Crystal");
			AddMapEntry(new Color(200, 200, 200));
		}
	}
}