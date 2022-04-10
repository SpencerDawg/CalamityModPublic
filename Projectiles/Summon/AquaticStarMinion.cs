﻿using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon
{
    public class AquaticStarMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Star");
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CalamityPlayer modPlayer = player.Calamity();
            bool flag64 = Projectile.type == ModContent.ProjectileType<AquaticStarMinion>();
            player.AddBuff(ModContent.BuffType<AquaticStar>(), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.aStar = false;
                }
                if (modPlayer.aStar)
                {
                    Projectile.timeLeft = 2;
                }
            }
            Projectile.rotation += Projectile.velocity.X * 0.04f;

            Projectile.ChargingMinionAI(1200f, 1500f, 2200f, 150f, 0, 40f, 8f, 4f, new Vector2(0f, -60f), 40f, 9.5f, false, false);
        }

        public override bool OnTileCollide(Vector2 oldVelocity) => false;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 5;
        }
    }
}
