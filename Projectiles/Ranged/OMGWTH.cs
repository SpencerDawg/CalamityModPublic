using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged
{
    public class OMGWTH : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("WTH");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 360;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}

		public override void AI()
		{
			Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.25f) / 255f, ((255 - projectile.alpha) * 0.01f) / 255f, ((255 - projectile.alpha) * 0.01f) / 255f);
			for (int num136 = 0; num136 < 3; num136++)
			{
				float x2 = projectile.position.X - projectile.velocity.X / 10f * (float)num136;
				float y2 = projectile.position.Y - projectile.velocity.Y / 10f * (float)num136;
				int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 235, 0f, 0f, 0, default(Color), 0.75f);
				Main.dust[num137].alpha = projectile.alpha;
				Main.dust[num137].position.X = x2;
				Main.dust[num137].position.Y = y2;
				Main.dust[num137].velocity *= 0f;
				Main.dust[num137].noGravity = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
