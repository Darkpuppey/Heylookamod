using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.NPCs
{
	internal class JimothyHead : Jimothy
	{
		public override void SetDefaults()
		{
			// Head is 10 defence, body 20, tail 30.
			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.width = 64;
			npc.height = 66;
			npc.HitSound = SoundID.NPCHit3;
			npc.DeathSound = SoundID.NPCDeath5;
			npc.lifeMax = 1500;
			npc.defense = 50;
			npc.value = 1500f;
			npc.damage = 70;
		}

		public override void Init()
		{
			base.Init();
			if (NPC.AnyNPCs(mod.NPCType("JimHead")))
			{
				npc.lifeMax /= 2;
				npc.damage /= 2;
				npc.defense /= 2;
			}
			head = true;
		}

		private int attackCounter;
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(attackCounter);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			attackCounter = reader.ReadInt32();
		}

		//public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		//{
		// {
		//SpriteEffects spriteEffects = SpriteEffects.None;
		//spriteBatch.Draw(mod.GetTexture("NPCs/JimothyHeadGlow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
		//npc.frame, Color.White, npc.rotation,
		//new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
		//}
		//}

		bool SpawnedDuringJim = false;
		public override void CustomBehavior()
		{
			if (Main.netMode != 1)
			{
				if (attackCounter > 0)
				{
					attackCounter--;
				}
				if (npc.life >= 20)
				{
					Player target = Main.player[npc.target];
					if (attackCounter <= 0 && Vector2.Distance(npc.Center, target.Center) < 200 && Collision.CanHit(npc.Center, 1, 1, target.Center, 1, 1))
					{
						Vector2 direction = (target.Center - npc.Center).SafeNormalize(Vector2.UnitX);
						direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

						int projectile = Projectile.NewProjectile(npc.Center, npc.velocity * 2, mod.ProjectileType("JimothyBall"), npc.damage - 20, 0, Main.myPlayer);
						Main.PlaySound(SoundID.DD2_FlameburstTowerShot, npc.Center);
						attackCounter = 10;
						npc.netUpdate = true;
					}
				}
				if (npc.life <= 0)
				{
					Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimothyHead"), 1f);
				}
				if ((NPC.AnyNPCs(mod.NPCType("JimHead"))))
				{
					SpawnedDuringJim = true;
				}
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (!NPC.downedPlantBoss)
			{
				return 0f;
			}
			if (HeylookamodPlayer.Vulcanite)
			{
				return 0.5f;
			}
			if (SpawnCondition.Underworld.Chance > 0f)
			{
				return SpawnCondition.Underworld.Chance / 10f;
			}
			return SpawnCondition.Underworld.Chance;
		}
		public override void NPCLoot()
		{
			if (SpawnedDuringJim == false)
			{
				Item.NewItem(npc.getRect(), mod.ItemType("MoltenEssence"), Main.rand.Next(2, 4));
				if ((Main.rand.Next(20) < 1))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimothySword"));
				}
				if (HeylookamodWorld.downedJim)
				{
					Item.NewItem(npc.getRect(), mod.ItemType("VulcaniteOre"), Main.rand.Next(5, 10));
				}
			}
		}
	}

	internal class JimothyBody : Jimothy
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.width = 45;
			npc.height = 45;
			npc.HitSound = SoundID.NPCHit3;
			npc.lifeMax = 1500;
			npc.defense = 60;
			npc.damage = 60;
		}
		public override void Init()
		{
			base.Init();
			if (NPC.AnyNPCs(mod.NPCType("JimHead")))
			{
				npc.lifeMax /= 2;
				npc.damage /= 2;
				npc.defense /= 2;
			}
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		//public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		//{
		//{
		//SpriteEffects spriteEffects = SpriteEffects.None;
		//spriteBatch.Draw(mod.GetTexture("NPCs/JimothyBodyGlow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
		//npc.frame, Color.White, npc.rotation,
		//new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
		//}
		//}
		public override void CustomBehavior()
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimothyBody"), 1f);
			}
		}
	}

	internal class JimothyTail : Jimothy
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerTail);
			npc.aiStyle = -1;
			npc.width = 45;
			npc.height = 45;
			npc.HitSound = SoundID.NPCHit3;
			npc.lifeMax = 1500;
			npc.defense = -30;
			npc.damage = 70;
		}

		public override void Init()
		{
			base.Init();
			if (NPC.AnyNPCs(mod.NPCType("JimHead")))
			{
				npc.lifeMax /= 2;
				npc.damage /= 2;
				npc.defense /= 2;
			}
			tail = true;
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		// public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		//{
		//{
		//SpriteEffects spriteEffects = SpriteEffects.None;
		//spriteBatch.Draw(mod.GetTexture("NPCs/JimothyTailGlow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
		//npc.frame, Color.White, npc.rotation,
		//new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
		//}
		//}

		public override void CustomBehavior()
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimothyTail"), 1f);
			}
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class Jimothy : Worm
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jimothy");
		}

		public int Jimothylife = 1;

		public override void Init()
		{
			minLength = 10;
			maxLength = 10;
			tailType = ModContent.NPCType<JimothyTail>();
			bodyType = ModContent.NPCType<JimothyBody>();
			headType = ModContent.NPCType<JimothyHead>();
			speed = 20.5f;
			turnSpeed = 0.145f;
		}
	}
}