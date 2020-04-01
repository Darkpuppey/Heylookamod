using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.Tiles
{
	public class OvergrowthStone : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMerge[mod.TileType("OvergrowthGrass")][Type] = true;
			Main.tileMergeDirt[Type] = true;
			soundType = SoundID.Tink;
			dustType = 3;
			minPick = 205;
			drop = mod.ItemType("OvergrowthStone");
			AddMapEntry(new Color(167, 197, 203));
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