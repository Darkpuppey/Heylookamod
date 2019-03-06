using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Heylookamod.Tiles
{
	class StrangeFlower : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Strange Flower");
            soundType = 6;
            dustType = 2;
            AddMapEntry(new Color(244, 217, 66), name);
            animationFrameHeight = 34;
            //Can't use this since texture is vertical.
            //animationFrameHeight = 56;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter > 32) // Increase this for a slower animation (different variants of speed in an animation uses more code)
            {
                frameCounter = 0;
                frame++;
                if (frame > 1) // The "9" should be the amount of frames youre using
                {
                    frame = 0;
                }
            }
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("StrangeFlowerWeapon"));
		}
	}
}
