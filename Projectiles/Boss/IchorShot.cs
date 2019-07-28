﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss
{
    public class IchorShot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ichor Shot");
			Main.projFrames[projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.hostile = true;
			projectile.timeLeft = 420;
			projectile.penetrate = 1;
		}

		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter > 4)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
			}
			if (projectile.frame > 5)
			{
				projectile.frame = 0;
			}
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			int num469 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 170, 0f, 0f, 100, default(Color), 0.5f);
			Main.dust[num469].noGravity = true;
			Main.dust[num469].velocity *= 0f;
			projectile.velocity.Y = projectile.velocity.Y + 0.06f;
			projectile.velocity.X *= 0.995f;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Ichor, 180);
		}
	}
}
