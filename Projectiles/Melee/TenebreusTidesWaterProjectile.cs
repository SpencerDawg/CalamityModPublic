using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityMod.Projectiles.Melee
{
    public class TenebreusTidesWaterProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Spear");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = true;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 10;
			projectile.tileCollide = false;
        }

        public override void AI()
        {
            Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0f, 0f, (255 - projectile.alpha) * 1f / 255f);
            int water = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 33, 0f, 0f, 100, default, 0.4f);
            Main.dust[water].noGravity = true;
            Main.dust[water].velocity *= 0.5f;
            Main.dust[water].velocity += projectile.velocity * 0.1f;

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + MathHelper.ToRadians(135f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			if (projectile.timeLeft == 300)
				return false;
            CalamityGlobalProjectile.DrawCenteredAndAfterimage(projectile, lightColor, ProjectileID.Sets.TrailingMode[projectile.type], 1);
            return false;
        }

        public override Color? GetAlpha(Color lightColor) => new Color(50, 50, 255, projectile.alpha);

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.position = projectile.Center;
            projectile.width = projectile.height = 64;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            for (int dustIndex = 0; dustIndex <= 30; dustIndex++)
            {
				Vector2 dustVel = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                float dustSpeed = (float)Main.rand.Next(3, 9);
                float dist = dustVel.Length();
                dist = dustSpeed / dist;
                dustVel.X *= dist;
                dustVel.Y *= dist;
                int water = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 33, 0f, 0f, 100, default, 1.2f);
                Dust dust = Main.dust[water];
                dust.noGravity = true;
                dust.position.X = projectile.Center.X;
                dust.position.Y = projectile.Center.Y;
                dust.position.X += (float)Main.rand.Next(-10, 11);
                dust.position.Y += (float)Main.rand.Next(-10, 11);
                dust.velocity = dustVel;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<CrushDepth>(), 240);
            SwordSpam(target, damage, knockback, crit);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<CrushDepth>(), 240);
            SwordSpamPvp(target, damage, crit);
        }

        // Spawns a storm of water projectiles on-hit.
        public void SwordSpam(NPC target, int damage, float knockback, bool crit)
        {
            int projAmt = 3;
            for (int i = 0; i < projAmt; ++i)
            {
				int type = Main.rand.NextBool() ? ModContent.ProjectileType<TenebreusTidesWaterSword>() : ModContent.ProjectileType<TenebreusTidesWaterSpear>();
                float startOffsetX = Main.rand.NextFloat(1000f, 1400f) * (Main.rand.NextBool() ? -1f : 1f);
                float startOffsetY = Main.rand.NextFloat(80f, 900f) * (Main.rand.NextBool() ? -1f : 1f);
                Vector2 startPos = new Vector2(target.Center.X + startOffsetX, target.Center.Y + startOffsetY);
                Vector2 projVel = target.Center - startPos;

                // Add some randomness / inaccuracy to the projectile target location
                projVel.X += Main.rand.NextFloat(-5f, 5f);
                projVel.Y += Main.rand.NextFloat(-5f, 5f);
                float speed = Main.rand.NextFloat(25f, 35f);
                float dist = projVel.Length();
                dist = speed / dist;
                projVel.X *= dist;
                projVel.Y *= dist;
                if (projectile.owner == Main.myPlayer)
                {
                    Projectile.NewProjectile(startPos, projVel, type, projectile.damage / 2, projectile.knockBack * 0.5f, projectile.owner, 0f, 0f);
                }
            }
        }

        // Spawns a storm of water projectiles on-hit.
        public void SwordSpamPvp(Player target, int damage, bool crit)
        {
            int projAmt = 3;
            for (int i = 0; i < projAmt; ++i)
            {
				int type = Main.rand.NextBool() ? ModContent.ProjectileType<TenebreusTidesWaterSword>() : ModContent.ProjectileType<TenebreusTidesWaterSpear>();
                float startOffsetX = Main.rand.NextFloat(1000f, 1400f) * (Main.rand.NextBool() ? -1f : 1f);
                float startOffsetY = Main.rand.NextFloat(80f, 900f) * (Main.rand.NextBool() ? -1f : 1f);
                Vector2 startPos = new Vector2(target.Center.X + startOffsetX, target.Center.Y + startOffsetY);
                Vector2 projVel = target.Center - startPos;

                // Add some randomness / inaccuracy to the projectile target location
                projVel.X += Main.rand.NextFloat(-5f, 5f);
                projVel.Y += Main.rand.NextFloat(-5f, 5f);
                float speed = Main.rand.NextFloat(25f, 35f);
                float dist = projVel.Length();
                dist = speed / dist;
                projVel.X *= dist;
                projVel.Y *= dist;
                if (projectile.owner == Main.myPlayer)
                {
                    Projectile.NewProjectile(startPos, projVel, type, projectile.damage / 2, projectile.knockBack * 0.5f, projectile.owner, 0f, 0f);
                }
            }
        }
    }
}
