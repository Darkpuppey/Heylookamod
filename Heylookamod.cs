using Terraria.ModLoader;

namespace Heylookamod
{
	public class Heylookamod : Mod
	{
        public static Heylookamod instance;

        public override void Load()
        {
            instance = this;
        }

        public override void Unload()
        {
            instance = null;
        }
    }
}
