using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.NPCs
{
    // This ModNPC serves as an example of a complete AI example.
    public class Jack : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flutter Slime"); // Automatic from .lang files
        }

        public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 34;
            npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1.
            npc.damage = 30;
            npc.defense = 10;
            npc.lifeMax = 750;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            //npc.alpha = 175;
            //npc.color = new Color(0, 80, 255, 100);
            npc.value = 50f;
            npc.knockBackResist = .01f;
        }

        //public override float SpawnChance(NPCSpawnInfo spawnInfo)
        //{
            // we would like this npc to spawn in the overworld.
            //return SpawnCondition.Underworld.Chance * 0.5f;
        //}

        // These const ints are for the benefit of the programmer. Organization is key to making an AI that behaves properly without driving you crazy.
        // Here I lay out what I will use each of the 4 npc.ai slots for.
        private const int AI_State_Slot = 0;
        private const int AI_Timer_Slot = 1;
        private const int AI_Jack_Time_Slot = 2;
        private const int AI_Wander_Slot = 3;
        private const int AI_Hop_Slot = 4;

        // npc.localAI will also have 4 float variables available to use. With ModNPC, using just a local class member variable would have the same effect.
        private const int Local_AI_Unused_Slot_0 = 0;
        private const int Local_AI_Unused_Slot_1 = 1;
        private const int Local_AI_Unused_Slot_2 = 2;
        private const int Local_AI_Unused_Slot_3 = 3;

        // Here I define some values I will use with the State slot. Using an ai slot as a means to store "state" can simplify things greatly. Think flowchart.
        private const int State_Wandering = 0;
        private const int State_Notice = 1;
        private const int State_Jump = 2;
        private const int State_Explode = 3;
        private const int State_Hop = 4;

        // This is a property (https://msdn.microsoft.com/en-us/library/x9fsa0sw.aspx), it is very useful and helps keep out AI code clear of clutter.
        // Without it, every instance of "AI_State" in the AI code below would be "npc.ai[AI_State_Slot]". 
        // Also note that without the "AI_State_Slot" defined above, this would be "npc.ai[0]".
        // This is all to just make beautiful, manageable, and clean code.
        public float AI_State
        {
            get => npc.ai[AI_State_Slot];
            set => npc.ai[AI_State_Slot] = value;
        }

        public float AI_Timer
        {
            get => npc.ai[AI_Timer_Slot];
            set => npc.ai[AI_Timer_Slot] = value;
        }

        public float AI_Jack
        {
            get => npc.ai[AI_Jack_Time_Slot];
            set => npc.ai[AI_Jack_Time_Slot] = value;
        }

        public float AI_Wander
        {
            get => npc.ai[AI_Wander_Slot];
            set => npc.ai[AI_Wander_Slot] = value;
        }
        public float AI_Hop
        {
            get => npc.ai[AI_Hop_Slot];
            set => npc.ai[AI_Hop_Slot] = value;
        }

        public bool Reset = false;

        // AdvancedFlutterSlime will need: float in water, diminishing aggo, spawn npcs.

        // Our AI here makes our NPC sit waiting for a player to enter range, jumps to attack, flutter mid-fall to stay afloat a little longer, then falls to the ground. Note that animation should happen in FindFrame
        public float JackTilePos;
        public override void AI()
        {
            JackTilePos = (npc.position.Y /= 16);
            npc.velocity *= 0.98f;
            if (npc.collideX && )
            {
                AI_Timer = 0;
                AI_State = State_Hop;
            }
            if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 100f && npc.velocity.Y == 0f)
            {
                AI_State = State_Jump;
                AI_Timer = 0;
            }
            Main.NewText(AI_State);
            npc.spriteDirection = npc.direction;
            // The npc starts in the asleep state, waiting for a player to enter range
            if (AI_State == State_Wandering)
            {
                npc.TargetClosest(false);
                // TargetClosest sets npc.target to the player.whoAmI of the closest player. the faceTarget parameter means that npc.direction will automatically be 1 or -1 if the targeted player is to the right or left. This is also automatically flipped if npc.confused
                AI_Wander = 5;
                if (AI_Timer <= 100)
                {
                    AI_Timer++;
                }
                if (AI_Timer >= 100)
                {
                    AI_Wander = Main.rand.Next(5, 9);
                    if (AI_Wander == 5 && AI_Wander == 6)
                    {
                        Main.NewText(AI_Wander);
                        AI_Timer++;
                        if (AI_Timer >= 150)
                        {
                            AI_Timer = 0;
                        }
                    }
                    if (AI_Wander == 7)
                    {
                        npc.direction = 1;
                        npc.velocity = new Vector2(3);
                        Main.NewText(AI_Wander);
                        AI_Timer = 0;
                    }
                    if (AI_Wander == 8)
                    {
                        npc.direction = -1;
                        npc.velocity = new Vector2(-3);
                        Main.NewText(AI_Wander);
                        AI_Timer = 0;
                    }
                }
                if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 150f)
                {
                    // Since we have a target in range, we change to the Notice state. (and zero out the Timer for good measure)
                    AI_Timer = 0;
                    AI_State = State_Notice;
                }
            }

            // In this state, a player has been targeted
            else if (AI_State == State_Notice)
            {
                npc.TargetClosest(true);
                if (AI_Timer == 0)
                {
                    npc.velocity = new Vector2(npc.direction * 2);
                    AI_Timer++;
                }
                /// If the targeted player is in attack range (250).
                // Here we use our Timer to wait .33 seconds before actually jumping. In FindFrame you'll notice AI_Timer also being used to animate the pre-jump crouch
                if (npc.velocity.X <= 5f)
                {
                    npc.TargetClosest(true);
                    npc.velocity += new Vector2(npc.direction * 2, 0f);
                    npc.velocity.X *= 1.05f;
                }
                AI_Timer++;
            }

            else if (AI_State == State_Hop)
            {
                npc.velocity = new Vector2(npc.direction * 2, -5f);
                AI_Timer++;
                if (AI_Timer == 2)
                {
                    AI_State = State_Notice;
                }

            }

            // In this state, we are in the jump. 
            if (AI_State == State_Jump)
            {
                if (Reset == false)
                {
                    AI_Timer = 0;
                    Reset = true;
                }
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 2f);
                AI_Timer++;
                if (AI_Timer == 1)
                {
                    // We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up. 
                    npc.velocity = new Vector2(npc.direction * 1.7f, -10f);
                    AI_Timer++;
                }
                if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 30f)
                {
                    AI_State = State_Explode;
                    AI_Timer = 0;
                }
                if (AI_Timer == 20)
                {
                    AI_State = State_Explode;
                    AI_Timer = 0;
                }
            }
            else if (AI_State == State_Explode)
            {
                // Set to transparent. This npc technically lives as  transparent for about 3 frames
                npc.alpha = 255;
                // change the hitbox size, centered about the original npc center. This makes the npc damage enemies during the explosion.
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 50;
                npc.height = 50;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                npc.damage = 150;
                Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, npc.position);
                // Smoke Dust spawn
                for (int i = 0; i < 50; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn
                for (int i = 0; i < 80; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                    dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 3f;
                }
                AI_Timer++;
                if (AI_Timer >= 2)
                {
                    npc.noTileCollide = true;
                    npc.velocity = new Vector2(0, -9999);
                }
            }
            if(npc.velocity.X >= 7f)
            {
                npc.velocity.X = 7f;
            }
            if (npc.velocity.X <= -7f)
            {
                npc.velocity.X = -7f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            // This makes the sprite flip horizontally in conjunction with the npc.direction.
            npc.spriteDirection = npc.direction;
        }
    }
}

/* Our texture is 32x32 with 2 pixels of padding vertically, so 34 is the vertical spacing.  These are for my benefit and the numbers could easily be used directly in the code below, but this is how I keep code organized.
 private const int Frame_Asleep = 0;
 private const int Frame_Notice = 1;
 private const int Frame_Falling = 2;
 private const int Frame_Flutter_1 = 3;
 private const int Frame_Flutter_2 = 4;
 private const int Frame_Flutter_3 = 5;

 // Here in FindFrame, we want to set the animation frame our npc will use depending on what it is doing.
 // We set npc.frame.Y to x * frameHeight where x is the xth frame in our spritesheet, counting from 0. For convenience, I have defined some consts above.
 public override void FindFrame(int frameHeight)
 {
     // This makes the sprite flip horizontally in conjunction with the npc.direction.
     npc.spriteDirection = npc.direction;

     // For the most part, our animation matches up with our states.
     if (AI_State == State_Asleep)
     {
         // npc.frame.Y is the goto way of changing animation frames. npc.frame starts from the top left corner in pixel coordinates, so keep that in mind.
         npc.frame.Y = Frame_Asleep * frameHeight;
     }
     else if (AI_State == State_Notice)
     {
         // Going from Notice to Asleep makes our npc look like it's crouching to jump.
         if (AI_Timer < 10)
         {
             npc.frame.Y = Frame_Notice * frameHeight;
         }
         else
         {
             npc.frame.Y = Frame_Asleep * frameHeight;
         }
     }
     else if (AI_State == State_Jump)
     {
         npc.frame.Y = Frame_Falling * frameHeight;
     }
     else if (AI_State == State_Hover)
     {
         // Here we have 3 frames that we want to cycle through.
         npc.frameCounter++;
         if (npc.frameCounter < 10)
         {
             npc.frame.Y = Frame_Flutter_1 * frameHeight;
         }
         else if (npc.frameCounter < 20)
         {
             npc.frame.Y = Frame_Flutter_2 * frameHeight;
         }
         else if (npc.frameCounter < 30)
         {
             npc.frame.Y = Frame_Flutter_3 * frameHeight;
         }
         else
         {
             npc.frameCounter = 0;
         }
     }
     else if (AI_State == State_Fall)
     {
         npc.frame.Y = Frame_Falling * frameHeight;
     }
 }
}
}
*/
