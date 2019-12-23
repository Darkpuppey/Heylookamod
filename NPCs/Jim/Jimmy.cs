using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Heylookamod.NPCs.Jim
{
    internal class JimmyHead : Jimmy
    {
        public override void SetDefaults()
        {
            // Head is 10 defence, body 20, tail 30.
            npc.CloneDefaults(NPCID.DiggerHead);
            npc.aiStyle = -1;
            npc.width = 30;
            npc.height = 28;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath5;
            npc.lifeMax = 250;
            npc.defense = 20;
            npc.value = 1250f;
            npc.damage = 15;
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

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (HeylookamodPlayer.Vulcanite)
            {
                return 1f;
            }
            else
                return 0f;
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

        //public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        //{
        // {
        //SpriteEffects spriteEffects = SpriteEffects.None;
        //spriteBatch.Draw(mod.GetTexture("NPCs/JimmyHeadGlow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
        //npc.frame, Color.White, npc.rotation,
        //new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
        //}
        //}

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

                        int projectile = Projectile.NewProjectile(npc.Center, npc.velocity * 1.2f, mod.ProjectileType("JimothyBall"), 50, 0, Main.myPlayer);
                        Main.PlaySound(SoundID.DD2_FlameburstTowerShot, npc.Center);
                        attackCounter = 180;
                        npc.netUpdate = true;
                    }
                }
                if (npc.life <= 0)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimmyHead"), 1f);
                }
            }
        }
    }

    internal class JimmyBody : Jimmy
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.aiStyle = -1;
            npc.width = 14;
            npc.height = 14;
            npc.HitSound = SoundID.NPCHit3;
            npc.lifeMax = 250;
            npc.defense = 20;
            npc.damage = 15;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        //public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        //{
        //{
        //SpriteEffects spriteEffects = SpriteEffects.None;
        //spriteBatch.Draw(mod.GetTexture("NPCs/JimmyBodyGlow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
        //npc.frame, Color.White, npc.rotation,
        //new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
        //}
        //}
        public override void CustomBehavior()
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimmyBody"), 1f);
            }
        }
    }

    internal class JimmyTail : Jimmy
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerTail);
            npc.aiStyle = -1;
            npc.width = 12;
            npc.height = 12;
            npc.HitSound = SoundID.NPCHit3;
            npc.lifeMax = 250;
            npc.defense = -10;
            npc.damage = 15;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        // public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        //{
        //{
        //SpriteEffects spriteEffects = SpriteEffects.None;
        //spriteBatch.Draw(mod.GetTexture("NPCs/JimmyTailGlow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
        //npc.frame, Color.White, npc.rotation,
        //new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
        //}
        //}

        public override void CustomBehavior()
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JimmyTail"), 1f);
            }
        }

        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }

    // I made this 2nd base class to limit code repetition.
    public abstract class Jimmy : Worm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jimmy");
        }

        public int Jimmylife = 1;

        public override void Init()
        {
            minLength = 3;
            maxLength = 3;
            tailType = ModContent.NPCType<JimmyTail>();
            bodyType = ModContent.NPCType<JimmyBody>();
            headType = ModContent.NPCType<JimmyHead>();
            speed = 15.5f;
            turnSpeed = 0.145f;
        }
    }
}