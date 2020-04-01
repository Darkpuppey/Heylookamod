using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Heylookamod.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace Heylookamod.Items
{
    public class StructureItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Structure Item");
            Tooltip.SetDefault("This will generate the structure you code it to.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.rare = 8;
            item.value = 350000;

            item.useStyle = 1;
            item.useAnimation = 45;
            item.useTime = 45;
        }

        public override bool UseItem(Player player)
        {
            //Your assignment to learn how to use Tex-To-Gen is to generate the structure I've provided with this tutuoral.

            //You have to generate the Tiles, Walls, and Liquids with the materials I specify by assigning them to colors they appear as on the texture

            //Note that I have only used 3 colors in the textures: (255, 0, 0) (Red), (0, 0, 255) (Blue), and (0, 0, 0) (Black)

            //Your task is to make the Tiles of the structure out of a block from your mod and turn the inside area (red) to air to allow for the liquid to generate using T.png

            //You must turn the walls into a wall from your mod using TWalls.png

            //You must generate water inside the structure using TLiquid.png

            //Note with Liquids you cannot set colors for them. They have hardcoded colors.

            //(255, 0, 0) (Red) is Lava, (0, 0, 255) (Blue) is Water, and (255, 255, 0) is Honey.

            //I have set all the values to (0, 0, 0) so you can enter them yourself.

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 0, 255)] = mod.TileType("OvergrowthGrass");
            colorToTile[new Color(255, 0, 0)] = -2; //-2 Deletes the block of the color it's assigned to. Useful for clearing out areas.
            colorToTile[Color.Black] = -1; //-1 Will not touch the blocks in the coordinates it's assigned to. This is good for leaving natural generation around a structure. Leaving this as black is a good Idea

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 0, 255)] = mod.WallType("OvergrowthWall");
            colorToWall[Color.Black] = -1;
            colorToTile[new Color(255, 0, 0)] = -2;

            //Tile Texture, colorToTile, Wall Texture, colorToWall, Liquid Texture

            //TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/FloweyCave"), colorToTile, mod.GetTexture("WorldGeneration/FloweyCaveWall"), colorToWall);

            // Point origin = new Point((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));
            //WorldGen.PlaceObject(origin.X, origin.Y, mod.TileType<StrangeFlower>());
            //gen.Generate(origin.X, origin.Y + 2, true, true);
            Main.NewText("hey something happened");
            return true;
        }
    }
}