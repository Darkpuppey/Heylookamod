using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;
using System.Collections.Generic;
namespace Heylookamod
{
    class Heylookamod : Mod
    {
        internal static Heylookamod instance;
        public static Heylookamod self = null;
        public static IDictionary<string, Texture2D> Textures = null;
        public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();
        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                // Here is where you define stuff
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
    GetTexture("Healthbars/JimBarStart"),
    GetTexture("Healthbars/JimBarMiddle"),
    GetTexture("Healthbars/JimBarEnd"),
    GetTexture("Healthbars/Phil"));
                yabhb.Call("hbSetMidBarOffset", -29, 10);
                yabhb.Call("hbSetBossHeadCentre", 78, 31);
                yabhb.Call("hbSetFillDecoOffsetSmall", 10);
                yabhb.Call("hbFinishSingle", NPCType("JimHead"));
            }
            if (bossChecklist != null)
            {
                //bossChecklist.Call(....
                // To include a description:
                bossChecklist.Call("AddBossWithInfo", "Jim", 10.5f, (Func<bool>)(() => HeylookamodWorld.downedJim), "Use a [i:" + ItemType("JimSpawner") + "] in Hell after Plantera has been defeated.");
            }
        }
        public Heylookamod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
            instance = this;
        }
    }
}
