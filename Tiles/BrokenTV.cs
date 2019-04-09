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
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Height = 6;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Origin = new Point16(2, 5);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(194, 184, 157));
            soundType = 21;
            dustType = 8;
            animationFrameHeight = 106;
            //Can't use this since texture is vertical.
            //animationFrameHeight = 56;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter > 4) // Increase this for a slower animation (different variants of speed in an animation uses more code)
            {
                frameCounter = 0;
                frame++;
                if (frame > 3) // The "9" should be the amount of frames youre using
                {
                    frame = 0;
                }
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 80, 96, mod.ItemType("BrokenTV"));
        }
    }
}
