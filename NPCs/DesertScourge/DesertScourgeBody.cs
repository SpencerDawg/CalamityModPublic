using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DesertScourge
{
	public class DesertScourgeBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Scourge");
        }

        public override void SetDefaults()
        {
			npc.GetNPCDamage();
			npc.npcSlots = 5f;
            npc.width = 32;
            npc.height = 36;
            npc.defense = 6;
			npc.DR_NERD(0.05f);
            npc.LifeMaxNERB(2600, 3000, 1650000);
            double HPBoost = CalamityConfig.Instance.BossHealthBoost * 0.01;
            npc.lifeMax += (int)(npc.lifeMax * HPBoost);
            npc.aiStyle = 6;
            aiType = -1;
            npc.knockBackResist = 0f;
            npc.alpha = 255;
            npc.boss = true;
            music = CalamityMod.Instance.GetMusicFromMusicMod("DesertScourge") ?? MusicID.Boss1;
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.canGhostHeal = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
            npc.dontCountMe = true;

			if (CalamityWorld.death || BossRushEvent.BossRushActive || CalamityWorld.malice)
				npc.scale = 1.25f;
			else if (CalamityWorld.revenge)
				npc.scale = 1.15f;
			else if (Main.expertMode)
				npc.scale = 1.1f;
		}

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void AI()
        {
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
				npc.TargetClosest(true);

			Player player = Main.player[npc.target];
			bool enraged = npc.Calamity().enraged > 0;
			bool malice = CalamityWorld.malice || BossRushEvent.BossRushActive;
			bool revenge = CalamityWorld.revenge || malice;
			bool death = CalamityWorld.death || malice;
			float burrowTimeGateValue = death ? 420f : 540f;
			bool burrow = Main.npc[(int)npc.ai[2]].Calamity().newAI[0] >= burrowTimeGateValue;

			// Check if other segments are still alive, if not, die
			bool shouldDespawn = true;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<DesertScourgeHead>())
				{
					shouldDespawn = false;
					break;
				}
			}
			if (!shouldDespawn)
			{
				if (npc.ai[1] > 0f)
					shouldDespawn = false;
				else if (Main.npc[(int)npc.ai[1]].life > 0)
					shouldDespawn = false;
			}
			if (shouldDespawn)
			{
				npc.life = 0;
				npc.HitEffect(0, 10.0);
				npc.checkDead();
				npc.active = false;
			}

			if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                npc.alpha -= 42;
                if (npc.alpha < 0)
                    npc.alpha = 0;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient && revenge && !burrow)
            {
                npc.localAI[0] += malice ? 4f : Main.rand.Next(4);
                if (npc.localAI[0] >= Main.rand.Next(1400, 26001))
                {
                    npc.localAI[0] = 0f;
                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                    {
                        Vector2 vector104 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
                        float num942 = player.position.X + (float)player.width * 0.5f - vector104.X;
                        float num943 = player.position.Y + (float)player.height * 0.5f - vector104.Y;
                        float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
                        int projectileType = ModContent.ProjectileType<SandBlast>();
                        float num941 = enraged ? 9f : 6f;
                        num944 = num941 / num944;
                        num942 *= num944;
                        num943 *= num944;
                        vector104.X += num942 * 5f;
                        vector104.Y += num943 * 5f;
                        if (Main.rand.NextBool(2) || enraged)
                        {
                            Projectile.NewProjectile(vector104.X, vector104.Y, num942, num943, projectileType, npc.GetProjectileDamage(projectileType), 0f, Main.myPlayer, 0f, 0f);
                        }
                        npc.netUpdate = true;
                    }
                }
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= npc.lifeMax * 0.75f && NPC.CountNPCS(ModContent.NPCType<DriedSeekerHead>()) < 3)
            {
                if (Main.rand.NextBool(10) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                    int seeker = NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DriedSeekerHead>());
                    if (Main.netMode == NetmodeID.Server && seeker < 200)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, seeker, 0f, 0f, 0f, 0, 0, 0);
                    }
                    npc.netUpdate = true;
                }
            }
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, hitDirection, -1f, 0, default, 1f);
            }
            if (npc.life <= 0)
            {
                float randomSpread = (float)(Main.rand.Next(-100, 100) / 100);
                Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/ScourgeBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/ScourgeBody2"), 1f);
                Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/ScourgeBody3"), 1f);
                for (int k = 0; k < 10; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, hitDirection, -1f, 0, default, 1f);
                }
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Bleeding, 120, true);
        }
    }
}
