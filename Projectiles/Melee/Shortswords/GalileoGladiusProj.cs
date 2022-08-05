﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.Buffs.DamageOverTime;
using System;

using CalamityMod.Projectiles.BaseProjectiles;

namespace CalamityMod.Projectiles.Melee.Shortswords
{
    public class GalileoGladiusProj : BaseShortswordProjectile
    {
        public override string Texture => "CalamityMod/Items/Weapons/Melee/GalileoGladius";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galileo Gladius"); // TODO: Make it function like Gladius, not like PiercingStarlight.
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(24);
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = TrueMeleeDamageClass.Instance;
            Projectile.ownerHitCheck = true;
            Projectile.timeLeft = 360;
            Projectile.extraUpdates = 1;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
        }

        public override Action<Projectile> EffectBeforePullback => (proj) =>
        {
            int moonDamage = (int)(Projectile.damage * 0.3333f);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 10f, ModContent.ProjectileType<GalileosMoon>(), moonDamage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        };

        public override void SetVisualOffsets()
        {
            const int HalfSpriteWidth = 42 / 2;
            const int HalfSpriteHeight = 46 / 2;

            int HalfProjWidth = Projectile.width / 2;
            int HalfProjHeight = Projectile.height / 2;

            DrawOriginOffsetX = 0;
            DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
            DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
        }

        public override void ExtraBehavior()
        {
            if (Main.rand.NextBool(5))
            {
                int num250 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, (Main.rand.NextBool(2) ? 20 : 176), (float)(Main.player[Projectile.owner].direction * 2), 0f, 150, default, 1.3f);
                Main.dust[num250].velocity *= 0.2f;
                Main.dust[num250].noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Nightwither>(), 300);
            SpawnMeteor(Main.player[Projectile.owner]);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Nightwither>(), 300);
            SpawnMeteor(Main.player[Projectile.owner]);
        }

        private void SpawnMeteor(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                var source = player.GetSource_FromThis();
                if (player.Calamity().galileoCooldown <= 0)
                {
                    int damage = player.GetWeaponDamage(player.ActiveItem()) * 2;
                    CalamityUtils.ProjectileRain(source, player.Center, 400f, 100f, 500f, 800f, 25f, ModContent.ProjectileType<GalileosPlanet>(), damage, 15f, player.whoAmI);
                    player.Calamity().galileoCooldown = 15;
                }
            }
        }
    }
}