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
using BaseMod;
using Heylookamod.Tiles;
using Heylookamod.WorldGeneration;

namespace Heylookamod
{
    public class HeylookamodWorld : ModWorld
    {
        private const int saveVersion = 0;
        public static bool downedJim = false;
        private Vector2 FloweyPos = new Vector2(0, 0);
        public static int FloweyTiles = 0;

        public override void Initialize()
        {
            downedJim = false;
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
            FloweyTiles = tileCounts[mod.TileType<Crystal>()];
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if (Main.worldName == "worldgentest")
            {
                tasks.Insert(shiniesIndex2, new PassLegacy("FloweyCave", delegate (GenerationProgress progress)
                {
                    FloweyCave(progress);
                }));
            }
        }

        private void FloweyCave(GenerationProgress progress)
        {
            progress.Message = "Howdy!";
            FloweyCaveBegin();
        }

        public void FloweyCaveBegin()
        {
            Point origin = new Point((int)(Main.maxTilesX * 0.3f), (Main.spawnTileY)); ;
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            FloweyCaveDelete delete = new FloweyCaveDelete();
            FloweyCave biome = new FloweyCave();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);
            WorldGen.PlaceTile(origin.X + 111, origin.Y + 176, (ushort)TileID.Gold);
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
