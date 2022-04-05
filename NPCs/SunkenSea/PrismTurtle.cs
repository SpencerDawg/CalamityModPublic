using CalamityMod.Items.Placeables;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityMod.NPCs.SunkenSea
{
    public class PrismTurtle : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prism-Back");
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            NPC.noGravity = true;
            NPC.damage = Main.hardMode ? 40 : 20; //normal damage
            NPC.width = 72;
            NPC.height = 58;
            NPC.defense = Main.hardMode ? 25 : 10;
            NPC.DR_NERD(0.25f);
            NPC.lifeMax = Main.hardMode ? 1000 : 350;
            NPC.aiStyle = -1;
            AIType = -1;
            NPC.value = Main.hardMode ? Item.buyPrice(0, 0, 50, 0) : Item.buyPrice(0, 0, 5, 0);
            NPC.HitSound = SoundID.NPCHit24;
            NPC.DeathSound = SoundID.NPCDeath27;
            NPC.knockBackResist = 0.15f;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<PrismTurtleBanner>();
            NPC.chaseable = false;
            NPC.Calamity().VulnerableToHeat = false;
            NPC.Calamity().VulnerableToSickness = true;
            NPC.Calamity().VulnerableToElectricity = true;
            NPC.Calamity().VulnerableToWater = false;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(NPC.chaseable);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.chaseable = reader.ReadBoolean();
        }

        public override void AI()
        {
            if ((NPC.Center.Y + 10f) > Main.player[NPC.target].Center.Y)
            {
                if (CalamityWorld.death) //gotta do damage scaling directly
                {
                    NPC.damage = Main.hardMode ? 240 : 120;
                }
                else if (CalamityWorld.revenge)
                {
                    NPC.damage = Main.hardMode ? 168 : 84;
                }
                else if (Main.expertMode)
                {
                    NPC.damage = Main.hardMode ? 160 : 80;
                }
                else
                {
                    NPC.damage = Main.hardMode ? 80 : 40;
                }
            }
            else
            {
                if (CalamityWorld.death) //gotta do damage scaling directly
                {
                    NPC.damage = Main.hardMode ? 120 : 60;
                }
                else if (CalamityWorld.revenge)
                {
                    NPC.damage = Main.hardMode ? 84 : 42;
                }
                else if (Main.expertMode)
                {
                    NPC.damage = Main.hardMode ? 80 : 40;
                }
                else
                {
                    NPC.damage = Main.hardMode ? 40 : 20;
                }
            }
            Lighting.AddLight(NPC.Center, (255 - NPC.alpha) * 0f / 255f, (255 - NPC.alpha) * 0.75f / 255f, (255 - NPC.alpha) * 0.75f / 255f);
            CalamityAI.PassiveSwimmingAI(NPC, Mod, 2, 0f, 0f, 0f, 0f, 0f, 0.1f);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += NPC.wet ? 0.1f : 0f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
            Vector2 vector11 = new Vector2((float)(Main.npcTexture[NPC.type].Width / 2), (float)(Main.npcTexture[NPC.type].Height / Main.npcFrameCount[NPC.type] / 2));
            Vector2 vector = center - Main.screenPosition;
            vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityMod/NPCs/SunkenSea/PrismTurtleGlow").Width, (float)(ModContent.Request<Texture2D>("CalamityMod/NPCs/SunkenSea/PrismTurtleGlow").Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
            vector += vector11 * 1f + new Vector2(0f, 4f + NPC.gfxOffY);
            Color color = new Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Blue);
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityMod/NPCs/SunkenSea/PrismTurtleGlow"), vector,
                new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
            {
                return NPC.chaseable;
            }
            return null;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.Calamity().ZoneSunkenSea && spawnInfo.water && !spawnInfo.Player.Calamity().clamity)
            {
                return SpawnCondition.CaveJellyfish.Chance * 0.9f;
            }
            return 0f;
        }

        public override void NPCLoot()
        {
            DropHelper.DropItemCondition(NPC, ModContent.ItemType<PrismShard>(), DownedBossSystem.downedDesertScourge, 1, 1, 3);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hitDirection, -1f, 0, default, 1f);
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/PrismTurtle/PrismTurtleGore1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/PrismTurtle/PrismTurtleGore2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/PrismTurtle/PrismTurtleGore3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/PrismTurtle/PrismTurtleGore4"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/PrismTurtle/PrismTurtleGore5"), 1f);
                for (int k = 0; k < 25; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hitDirection, -1f, 0, default, 1f);
                }
            }
        }
    }
}
