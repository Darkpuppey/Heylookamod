using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace BaseMod
{
	public abstract class ParentProjectile : ModProjectile
	{
		public void SetAI(float[] ai, int aiType) { }
		public Vector4 GetFrameV4(){ return new Vector4(0, 0, 1, 1); }		
	}
}
