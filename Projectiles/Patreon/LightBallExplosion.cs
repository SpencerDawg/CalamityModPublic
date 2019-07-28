﻿using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Patreon
{
    public class LightBallExplosion : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Explosion");
		}

        public override void SetDefaults()
        {
            projectile.width = 300;
            projectile.height = 300;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 5;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 3;
        }
    }
}
