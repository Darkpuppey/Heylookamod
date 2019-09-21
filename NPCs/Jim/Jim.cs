using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.NPCs.Jim
{
    [AutoloadBossHead]
    internal class JimHead : Jim
    {
        public override void SetDefaults()
        {
            // Head is 10 defence, body 20, tail 30.
            npc.CloneDefaults(NPCID.DiggerHead);
            npc.aiStyle = -1;
            npc.width = 198;
            npc.height = 198;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath5;
            npc.lifeMax = 200000;
            npc.defense = 75;
            npc.value = 150000f;
            npc.damage = 150;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SolarStorm");
            bossBag = mod.ItemType("JimBag");
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The " + name;
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void NPCLoot()
        {
            Vector2 position = npc.position;
            Vector2 center = Main.player[npc.target].Center;
            float num4 = 1E+08f;
            Vector2 position2 = center;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == 134 || Main.npc[i].type == 135 || Main.npc[i].type == 136))
                {
                    float num5 = Math.Abs(Main.npc[i].Center.X - center.X) + Math.Abs(Main.npc[i].Center.Y - center.Y);
                    if (num5 < num4)
                    {
                        num4 = num5;
                        position2 = Main.npc[i].position;
                    }
                }
            }
            npc.position = position2;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LavaGlob"));
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimMask"));
                }
                int choice = Main.rand.Next(3);

                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimSpear"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimStaff"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimBow"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimSword"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimStaff"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimBow"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimSpear"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimSword"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JimBow"));
                }
                Item.NewItem(npc.getRect(), mod.ItemType("VulcaniteOre"), Main.rand.Next(20, 30));
            }
            npc.position = position;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.5f);
            npc.defense = 100;
        }
        private static int TilesMaxY
        {
            get
            {
                return Main.maxTilesY;
            }
        }

        private static int TilesMaxX
        {
            get
            {
                return Main.maxTilesX;
            }
        }

        public override void Init()
        {
            base.Init();
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
        public override void CustomBehavior()
        {
            Player player = Main.player[npc.target];
            if (Main.netMode != 1)
            {
                Player target = Main.player[npc.target];
                if (player.dead)
                {
                    npc.velocity = new Vector2(0, 50);
                }
                if (attackCounter > 0)
                {
                    attackCounter--;
                }
                if (attackCounter <= 0 && Vector2.Distance(npc.Center, target.Center) < 500 && Collision.CanHit(npc.Center, 1, 1, target.Center, 1, 1))
                {
                    Vector2 direction = (target.Center - npc.Center).SafeNormalize(Vector2.UnitX);
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(10));
                    int numberProjectiles = 4 + Main.rand.Next(5); // 4 or 5 shots
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(npc.velocity.X, npc.velocity.Y).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.                                                                                          // If you want to randomize the speed to stagger the projectiles
                        float scale = 1.4f - (Main.rand.NextFloat() * 1.5f);
                        perturbedSpeed = perturbedSpeed * scale;
                        int projectile = Projectile.NewProjectile(npc.Center, perturbedSpeed + npc.velocity, mod.ProjectileType("JimFlame"), 100, 0, Main.myPlayer);
                        if (Main.expertMode == true && Main.rand.Next(5) == 0)
                        {
                            Projectile.NewProjectile(npc.Center, perturbedSpeed + npc.velocity, mod.ProjectileType("JimothyBall"), 75, 0, Main.myPlayer);
                        }
                        Main.PlaySound(SoundID.DD2_FlameburstTowerShot, npc.Center);
                        //Main.NewText(player.position.X);
                        //Main.NewText(TilesMaxX);
                        attackCounter = 5;
                        if (Main.expertMode == true)
                        {
                            attackCounter = 10;
                        }

                        npc.netUpdate = true;
                    }
                }
                if (npc.life <= 0)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimHead"), 1f);
                   //Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimHead2"), 1f);
                   //Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimHead3"), 1f);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimHead4"), 1f);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimHead5"), 1f);
                }
                if (npc.Center.Y >= TilesMaxY * 17 && !player.dead)
                {
                    npc.velocity = new Vector2(npc.velocity.X, -10);
                    npc.alpha = 122;
                }
                if (npc.Center.X <= TilesMaxX / 15)
                {
                    npc.velocity = new Vector2(10, npc.velocity.Y);
                    npc.alpha = 122;
                }
                if (npc.Center.X >= TilesMaxX * 15.5)
                {
                    npc.velocity = new Vector2(-10, npc.velocity.Y);
                    npc.alpha = 122;
                }
                if (Vector2.Distance(npc.Center, target.Center) > 5000)
                {
                    speed = (Vector2.Distance(npc.Center, target.Center) / 10f);
                }
                else
                {
                    speed = 40.5f;
                }
                if (npc.alpha >= 0)
                {
                    npc.alpha--;
                    npc.alpha--;
                    npc.alpha--;
                    npc.alpha--;
                }
            }
        }

        //public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        //{
        // {
        //SpriteEffects spriteEffects = SpriteEffects.None;
        //spriteBatch.Draw(mod.GetTexture("NPCs/JimHeadGlow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
        //npc.frame, Color.White, npc.rotation,
        //new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
        //}
        //}
    }

    internal class JimBody : Jim
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.aiStyle = -1;
            npc.width = 130;
            npc.height = 130;
            npc.HitSound = SoundID.NPCHit3;
            npc.lifeMax = 200000;
            npc.defense = 50;
            npc.damage = 75;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SolarStorm");
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), mod.ItemType("VulcaniteOre"), Main.rand.Next(1, 3));
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.5f);
            npc.defense = 75;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public float AI_State = 0;
        public float AI_Timer = 0;
        private int attackCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

        public override void CustomBehavior()
        {
            Player player = Main.player[npc.target];
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimBody"), 1f);
                //Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimBody2"), 1f);
                //Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimBody3"), 1f);
                if (HeylookamodWorld.downedJim == false && player.dead == false)
                {
                    HeylookamodWorld.downedJim = true;
                }
            }
        }
        //public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        //{
        //{
        //SpriteEffects spriteEffects = SpriteEffects.None;
        //spriteBatch.Draw(mod.GetTexture("NPCs/JimBodyGlow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
        //npc.frame, Color.White, npc.rotation,
        //new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
        //}
        //}

    }

    internal class JimTail : Jim
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerTail);
            npc.aiStyle = -1;
            npc.width = 130;
            npc.height = 130;
            npc.HitSound = SoundID.NPCHit3;
            npc.lifeMax = 200000;
            npc.defense = -30;
            npc.damage = 50;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SolarStorm");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 2f);
            npc.defense = -10;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        // public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        //{
        //{
        //SpriteEffects spriteEffects = SpriteEffects.None;
        //spriteBatch.Draw(mod.GetTexture("NPCs/JimTailGlow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
        //npc.frame, Color.White, npc.rotation,
        //new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
        //}
        //}
        public float AI_State = 0;
        public float AI_Timer = 0;
        public override void Init()
        {
            base.Init();
            tail = true;
        }

        public override void CustomBehavior()
        {
            if (Main.netMode != 1)
            {
                Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                AI_Timer++;
                if (AI_Timer == 100)
                {
                    AI_Timer = 0;
                }
                if (AI_Timer == 90 && AI_State == 0)
                {
                    AI_State = Main.rand.NextBool() ? 1 : 2;
                    //Main.NewText(AI_State);
                }
                if (AI_State == 2)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("JimothyHead"));
                    if (Main.expertMode == true)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("JimmyHead"));
                    }
                    AI_State = 0;
                }
                if (AI_State == 1)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("JimmyHead"));
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("JimmyHead"));
                    if (Main.expertMode == true)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("JimothyHead"));
                    }
                    else
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("JimmyHead"));
                    }
                    AI_State = 0;
                }
                if (npc.life <= 0)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimTail"), 1f);
                    //Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimTail2"), 1f);
                }
            }
        }
    }

    // I made this 2nd base class to limit code repetition.
    public abstract class Jim : Worm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jim");
        }

        public override void Init()
        {
            minLength = 50;
            maxLength = 50; //75
            tailType = mod.NPCType<JimTail>();
            bodyType = mod.NPCType<JimBody>();
            headType = mod.NPCType<JimHead>();
            speed = 40.5f;
            turnSpeed = 0.25f;
        }
    }
}