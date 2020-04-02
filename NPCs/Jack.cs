using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.NPCs
{
	/// <summary>
	/// Jack is a Vulcan NPC. He has 5 stages, wandering, notice, hop, jump, and explode.
	/// </summary>
	public class Jack : ModNPC
	{
		// These const ints are for the benefit of the programmer. Organization is key to making an AI that behaves properly without driving you crazy.
		// Here I lay out what I will use each of the 4 npc.ai slots for.
		private const int AI_State_Slot = 0;
		private const int AI_Timer_Slot = 1;
		private const int AI_Jack_Time_Slot = 2;
		private const int AI_Wander_Slot = 3;
		private const int AI_Hop_Slot = 4;

		// Here I define some values I will use with the State slot. Using an ai slot as a means to store "state" can simplify things greatly. Think flowchart.
		private const int State_Wandering = 0;
		private const int State_Notice = 1;
		private const int State_Jump = 2;
		private const int State_Explode = 3;
		private const int State_Hop = 4;

		private const int HopFrame = 6;
		private const int JumpFrame = 7;

		// Also note that without the "AI_State_Slot" defined above, this would be "npc.ai[0]".
		// This is all to just make beautiful, manageable, and clean code.
		public float AIState { get => npc.ai[AI_State_Slot]; set => npc.ai[AI_State_Slot] = value; }

		public float AI_Timer { get => npc.ai[AI_Timer_Slot]; set => npc.ai[AI_Timer_Slot] = value; }

		public float AI_Jack { get => npc.ai[AI_Jack_Time_Slot]; set => npc.ai[AI_Jack_Time_Slot] = value; }

		public float AI_Wander { get => npc.ai[AI_Wander_Slot]; set => npc.ai[AI_Wander_Slot] = value; }

		public float AI_Hop { get => npc.ai[AI_Hop_Slot]; set => npc.ai[AI_Hop_Slot] = value; }

		public bool Reset = false;

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 15; //npc has 15 frames
		}

		public override void SetDefaults()
		{
			npc.width = 44;
			npc.height = 34;
			npc.damage = 30;
			npc.defense = 10;
			npc.lifeMax = 750;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 50f;
			npc.knockBackResist = 0.01f;
			npc.buffImmune[BuffID.OnFire] = true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			// we would like this npc to spawn in the overworld.
			return SpawnCondition.Underworld.Chance * 0.5f;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			//Make the npc explode
			AIState = State_Explode;
		}

		public override void AI()
		{
			//The default of npc.ai or AIState is 0, meaning the default state is wandering, so no need to set it.

			npc.velocity *= 0.98f;

			if (npc.collideX || npc.collideY)
			{
				AI_Timer = 0;
				AIState = State_Hop;
			}

			if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 100f && npc.velocity.Y == 0f)
			{
				AIState = State_Jump;
				AI_Timer = 0;
			}

			//If none of the requireents were met bove and the ai state wasent changed, proceed with the wandering state
			if (AIState == State_Wandering)
			{
				npc.TargetClosest(false);
				// TargetClosest sets npc.target to the player.whoAmI of the closest player. the faceTarget parameter means that npc.direction will automatically be 1 or -1 if the targeted player is to the right or left. This is also automatically flipped if npc.confused

				AI_Wander = 5;

				if (AI_Timer <= 100)
					AI_Timer++;

				if (AI_Timer >= 100)
				{
					AI_Wander = Main.rand.Next(5, 9);

					//Keep going
					if (AI_Wander == 5 || AI_Wander == 6)
					{
						//Main.NewText(AI_Wander);

						AI_Timer++;

						if (AI_Timer >= 150)
							AI_Timer = 0;
					}

					//Turn right
					if (AI_Wander == 7)
					{
						npc.direction = 1;
						npc.velocity = new Vector2(3);
						//Main.NewText(AI_Wander);
						AI_Timer = 0;
					}

					//Turn left
					if (AI_Wander == 8)
					{
						npc.direction = -1;
						npc.velocity = new Vector2(-3);
						//Main.NewText(AI_Wander);
						AI_Timer = 0;
					}
				}

				//If the npc is closer than 10 tiles away
				if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 10 * 16f)
				{
					// Since we have a target in range, we change to the Notice state. (and zero out the Timer for good measure)
					AI_Timer = 0;
					AIState = State_Notice;
				}
			}
			//If not in the wandering state anymore, a player has been targeted
			else if (AIState == State_Notice)
			{
				npc.TargetClosest(true);

				if (AI_Timer == 0)
				{
					npc.velocity = new Vector2(npc.direction * 2);
					AI_Timer++;
				}

				// If the targeted player is in attack range (250).
				// Here we use our Timer to wait .33 seconds before actually jumping. In FindFrame you'll notice AI_Timer also being used to animate the pre-jump crouch
				if (npc.velocity.X <= 5f)
				{
					npc.TargetClosest(true);
					npc.velocity += new Vector2(npc.direction * 2, 0f);
					npc.velocity.X *= 1.05f;
				}

				AI_Timer++;
			}
			//If not in target or wandering, we are in the hop state
			else if (AIState == State_Hop)
			{
				npc.velocity = new Vector2(npc.direction * 2, -5f);
				AI_Timer++;

				if (AI_Timer == 2)
					AIState = State_Notice;
			}
			//If not in the wandering, target or hop state, the npc is in the jump state
			if (AIState == State_Jump)
			{
				if (Reset == false)
				{
					AI_Timer = 0;
					Reset = true;
				}

				Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default, 2f);

				AI_Timer++;

				if (AI_Timer == 1)
				{
					// We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up. 
					npc.velocity = new Vector2(npc.direction * 1.7f, -10f);
					AI_Timer++;
				}

				if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 30f)
				{
					AIState = State_Explode;
					AI_Timer = 0;
				}

				if (AI_Timer == 20)
				{
					AIState = State_Explode;
					AI_Timer = 0;
				}
			}
			//If in the state ready to explode
			else if (AIState == State_Explode)
			{
				// Set to transparent. This npc technically lives as transparent for about 3 frames
				//npc.alpha = 255;

				// change the hitbox size, centered about the original npc center. This makes the npc damage enemies during the explosion.
				npc.position.X = npc.position.X + (npc.width / 2);
				npc.position.Y = npc.position.Y + (npc.height / 2);

				npc.width = 50;
				npc.height = 50;

				npc.position.X = npc.position.X - (npc.width / 2);
				npc.position.Y = npc.position.Y - (npc.height / 2);

				npc.damage = 150;

				Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, npc.position);

				// Smoke Dust spawn
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default, 2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}

				// Fire Dust spawn
				for (int i = 0; i < 80; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default, 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 5f;
					dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default, 2f);
					Main.dust[dustIndex].velocity *= 3f;
				}

				AI_Timer++;

				if (AI_Timer > 1)
				{
					npc.noTileCollide = true;
					npc.velocity = new Vector2(0, -9999);
				}
			}

			if (npc.velocity.X >= 7f)
				npc.velocity.X = 7f;

			
			if (npc.velocity.X <= -7f)
				npc.velocity.X = -7f;
		}

		public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			npc.spriteDirection = npc.direction;

			switch (AIState)
			{
				case State_Wandering:
					npc.frameCounter++;

					if (npc.frameCounter < 10)
						npc.frame.Y = WanderingFrame(1) * frameHeight;
					else if (npc.frameCounter < 20)
						npc.frame.Y = WanderingFrame(2) * frameHeight;
					else if (npc.frameCounter < 30)
						npc.frame.Y = WanderingFrame(3) * frameHeight;
					else if (npc.frameCounter < 40)
						npc.frame.Y = WanderingFrame(4) * frameHeight;
					else
						npc.frameCounter = 0;
					break;

				case State_Notice:
					if (AI_Timer < 10)
						npc.frame.Y = NoticeFrame(1) * frameHeight;
					else
						npc.frame.Y = NoticeFrame(2) * frameHeight;
					break;

				case State_Hop:
					npc.frame.Y = HopFrame * frameHeight;
					break;

				case State_Jump:
					npc.frame.Y = JumpFrame * frameHeight;
					break;

				case State_Explode:
					npc.frameCounter++;

					if (npc.frameCounter < 10)
						npc.frame.Y = ExplosionFrame(1) * frameHeight;
					else if (npc.frameCounter < 20)
						npc.frame.Y = ExplosionFrame(2) * frameHeight;
					else if (npc.frameCounter < 30)
						npc.frame.Y = ExplosionFrame(3) * frameHeight;
					else if (npc.frameCounter < 40)
						npc.frame.Y = ExplosionFrame(4) * frameHeight;
					else if (npc.frameCounter < 50)
						npc.frame.Y = ExplosionFrame(5) * frameHeight;
					else if (npc.frameCounter < 60)
						npc.frame.Y = ExplosionFrame(6) * frameHeight;
					else if (npc.frameCounter < 70)
						npc.frame.Y = ExplosionFrame(7) * frameHeight;
					break;
			}
		}

		private static int WanderingFrame(int number) => number < 5 && number > 0 ? number + 1 : -1;

		private static int NoticeFrame(int number)
		{
			switch (number)
			{
				case 1:
					return 4;
				case 2:
					return 5;
				default:
					return -1;
			}
		}

		private static int ExplosionFrame(int number)
		{
			switch (number)
			{
				case 1:
					return 8;
				case 2:
					return 9;
				case 3:
					return 10;
				case 4:
					return 11;
				case 5:
					return 12;
				case 6:
					return 13;
				case 7:
					return 14;
				default:
					return -1;
			}
		}
	}
}