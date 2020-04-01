using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Heylookamod.Tiles;
using Heylookamod.WorldGeneration;
using Heylookamod.Items;

namespace Heylookamod
{
    public class HeylookamodWorld : ModWorld
    {
        private const int saveVersion = 0;
        public static bool downedJim = false;
        public static bool Vulcanite;
        public static int FloweyTiles = 0;
        public static int NearVulcanite = 0;
        private Vector2 OvergrowthPos = new Vector2(0, 0);

        public override void Initialize()
        {
            downedJim = false;
            Vulcanite = downedJim;
        }
        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedJim) downed.Add("JimHead");

            return new TagCompound {
                {"downed", downed}
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedJim = downed.Contains("JimHead");
            Vulcanite = downedJim;
        }

        class HeylookamodGlobalNPC : GlobalNPC
        {
            public override void NPCLoot(NPC npc)
            {
                if (npc.type == NPCID.DungeonGuardian)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Mistake"));
                }
                // Addtional if statements here if you would like to add drops to other vanilla npc.
            }
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedJim = flags[0];
            }
            else
            {
                ErrorLogger.Log("Darkpuppey you fucking idiot: Unknown loadVersion: " + loadVersion);
            }
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            FloweyTiles = tileCounts[ModContent.TileType<Tiles.OvergrowthGrass>()] + tileCounts[ModContent.TileType<Tiles.OvergrowthStone>()] + tileCounts[ModContent.WallType<Walls.OvergrowthWall>()];
            NearVulcanite = tileCounts[ModContent.TileType<VulcaniteOre>()];
        }



        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedJim;
            writer.Write(flags);

            //If you prefer, you can use the BitsByte constructor approach as well.
            //writer.Write(saveVersion);
            //BitsByte flags = new BitsByte(downedJim, downedPuritySpirit);
            //writer.Write(flags);

            // This is another way to do the same thing, but with bitmasks and the bitwise OR assignment operator (the |=)
            // Note that 1 and 2 here are bit masks. The next values in the pattern are 4,8,16,32,64,128. If you require more than 8 flags, make another byte.
            //writer.Write(saveVersion);
            //byte flags = 0;
            //if (downedJim)
            //{
            //	flags |= 1;
            //}
            //if (downedPuritySpirit)
            //{
            //	flags |= 2;
            //}
            //writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedJim = flags[0];
        }

        public override void PostUpdate()
        {
            if (downedJim == true)
            {
                if (Vulcanite == false)
                {
                    Vulcanite = true;
                    Main.NewText("A roar of flames quivers in the distance...", Color.OrangeRed.R, Color.OrangeRed.G, Color.Yellow.B);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 300), WorldGen.genRand.Next(7, 10), WorldGen.genRand.Next(11, 12), (ushort)mod.TileType("VulcaniteOre"));
                    }
                }
            }
        }
        // A helper method that draws a bordered rectangle. 
        public static void DrawBorderedRect(SpriteBatch spriteBatch, Color color, Color borderColor, Vector2 position, Vector2 size, int borderWidth)
        {
            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), color);
            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)position.X - borderWidth, (int)position.Y - borderWidth, (int)size.X + borderWidth * 2, borderWidth), borderColor);
            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)position.X - borderWidth, (int)position.Y + (int)size.Y, (int)size.X + borderWidth * 2, borderWidth), borderColor);
            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)position.X - borderWidth, (int)position.Y, (int)borderWidth, (int)size.Y), borderColor);
            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)position.X + (int)size.X, (int)position.Y, (int)borderWidth, (int)size.Y), borderColor);
        }
    }
}
