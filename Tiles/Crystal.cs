using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Tiles
{
	public class Crystal : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            soundType = 6;
            dustType = 2;
            minPick = 9999999;
            drop = mod.ItemType("Crystal");
			AddMapEntry(new Color(177, 255, 43));
		}
	}
}