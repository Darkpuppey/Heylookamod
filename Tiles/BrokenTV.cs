using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Heylookamod.Tiles
{
	class BrokenTV : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.Height = 6;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16};
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.addTile(Type);
            soundType = 6;
            dustType = 2;
            animationFrameHeight = 88;
            //Can't use this since texture is vertical.
            //animationFrameHeight = 56;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter > 8) // Increase this for a slower animation (different variants of speed in an animation uses more code)
            {
                frameCounter = 0;
                frame++;
                if (frame > 3) // The "9" should be the amount of frames youre using
                {
                    frame = 0;
                }
            }
        }
	}
}
