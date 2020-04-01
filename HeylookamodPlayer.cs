using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod
{
	// ModPlayer classes provide a way to attach data to Players and act on that data. HeylookamodPlayer has a lot of functionality related to 
	// several effects and items in ExampleMod. See SimpleModPlayer for a very simple example of how ModPlayer classes work.
	public class HeylookamodPlayer : ModPlayer
	{
		private const int saveVersion = 0;
		public int score = 0;
		public int constantDamage = 0;
		public float percentDamage = 0f;
		public float defenseEffect = -1f;
		public bool badHeal = false;
		public int healHurt = 0;
		public bool friendPet = false;
		public bool infinity = false;
		public bool JimExpert = false;
		public bool JimExpertLava = false;
		public static bool EnteredOvergrowth = false;
		// These 5 relate to ExampleCostume.

		public bool Overgrowth = false;
		public static bool Vulcanite = false;

		public override void ResetEffects()
		{
			constantDamage = 0;
			percentDamage = 0f;
			defenseEffect = -1f;
			badHeal = false;
			healHurt = 0;
			friendPet = false;
			JimExpert = false;
			JimExpertLava = false;
		}

		// In MP, other clients need accurate information about your player or else bugs happen.
		// clientClone, SyncPlayer, and SendClientChanges, ensure that information is correct.
		// We only need to do this for data that is changed by code not executed by all clients, 
		// or data that needs to be shared while joining a world.
		// For example, friendPet doesn't need to be synced because all clients know that the player is wearing the friendPet item in an equipment slot. 
		// The friendPet bool is set for that player on every clients computer independently (via the Buff.Update), keeping that data in sync.
		// ExampleLifeFruits, however might be out of sync. For example, when joining a server, we need to share the exampleLifeFruits variable with all other clients.
		public override void clientClone(ModPlayer clientClone)
		{
			HeylookamodPlayer clone = clientClone as HeylookamodPlayer;
			// Here we would make a backup clone of values that are only correct on the local players Player instance.
			// Some examples would be RPG stats from a GUI, Hotkey states, and Extra Item Slots
			// clone.someLocalVariable = someLocalVariable;
		}

		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = mod.GetPacket();
			packet.Write((byte)player.whoAmI);
			packet.Send(toWho, fromWho);
		}

		public override void ModifyScreenPosition()
		{
			if (NPC.AnyNPCs(mod.NPCType("JimHead")))
			{
				if (player.position.Y <= (Main.maxTilesY * 13))
				{
					player.position.Y = (Main.maxTilesY * 13f);
				}
			}
		}

		public override void SendClientChanges(ModPlayer clientPlayer)
		{
			// Here we would sync something like an RPG stat whenever the player changes it.
			// So far, ExampleMod has nothing that needs this.
			// if (clientPlayer.someLocalVariable != someLocalVariable)
			// {
			//    Send a Mod Packet with the changes.
			// }
		}

		public override void UpdateDead()
		{
			badHeal = false;
		}
		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			score = reader.ReadInt32();
		}
		public override void UpdateBadLifeRegen()
		{
			if (healHurt > 0)
			{
				if (player.lifeRegen > 0)
				{
					player.lifeRegen = 0;
				}
				player.lifeRegenTime = 0;
				player.lifeRegen -= 120 * healHurt;
			}
		}
		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
			ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (constantDamage > 0 || percentDamage > 0f)
			{
				int damageFromPercent = (int)(player.statLifeMax2 * percentDamage);
				damage = Math.Max(constantDamage, damageFromPercent);
				customDamage = true;
			}
			else if (defenseEffect >= 0f)
			{
				if (Main.expertMode)
				{
					defenseEffect *= 1.5f;
				}
				damage -= (int)(player.statDefense * defenseEffect);
				if (damage < 0)
				{
					damage = 1;
				}
				customDamage = true;
			}
			constantDamage = 0;
			percentDamage = 0f;
			defenseEffect = -1f;
			return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
		}
		public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			if (JimExpert == true)
			{
				for (int i = 0; i < 3; i++)
				{
					// Random upward vector.
					Vector2 vel = new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-4, -3));
					Projectile.NewProjectile(player.Center, vel, mod.ProjectileType("JimShard"), 40, 3, player.whoAmI, 0, 1);
				}
				Main.PlaySound(SoundID.Shatter, player.position);
			}
		}
		public override void UpdateBiomes()
		{
			Overgrowth = (HeylookamodWorld.FloweyTiles > 50);
			Vulcanite = (HeylookamodWorld.NearVulcanite > 5 && player.ZoneRockLayerHeight);
			if (!EnteredOvergrowth & Overgrowth)
			{
				Main.NewText("The Overgrowth");
				Main.NewText("Theme of The Overgrowth: Fear - Instrumental Mix by Vetrom");
				EnteredOvergrowth = true;
			}
		}
		public override bool CustomBiomesMatch(Player other)
		{
			HeylookamodPlayer modOther = other.GetModPlayer<HeylookamodPlayer>();
			return Overgrowth == modOther.Overgrowth;
		}
		public override void CopyCustomBiomesTo(Player other)
		{
			HeylookamodPlayer modOther = other.GetModPlayer<HeylookamodPlayer>();
			modOther.Overgrowth = Overgrowth;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = Overgrowth;
			writer.Write(flags);
		}

		public override void ReceiveCustomBiomes(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			Overgrowth = flags[0];
		}
	}
}
