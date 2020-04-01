using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Heylookamod.Tiles
{
	public class OvergrowthGrass : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMerge[mod.TileType("OvergrowthStone")][Type] = true;
			Main.tileMergeDirt[Type] = true;
			soundType = 6;
			dustType = 2;
			minPick = 205;
			drop = mod.ItemType("OvergrowthGrass");
			AddMapEntry(new Color(177, 255, 43));
		}
		public override bool CanExplode(int i, int j)
		{
			if (!HeylookamodWorld.downedJim)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}