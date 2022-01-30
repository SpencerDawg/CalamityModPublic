﻿using System;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using static CalamityMod.CalamityUtils;
using CalamityMod.Items.Weapons.Melee;

namespace CalamityMod.DataStructures
{
    public enum AttunementID : byte
    {
        Default, Hot, Cold, Tropical, Evil,  //Broken biome blade
        TrueDefault, TrueHot, TrueCold, TrueTropical, TrueEvil, Holy, Astral, Marine, //Biome blade
        Whirlwind, FlailBlade, SuperPogo, Shockwave, //True biome blade
        Phoenix, Aries, Polaris, Andromeda //Galaxia
    }

    public abstract class Attunement
    {
        public static Attunement[] attunementArray;

        public static void Load()
        {
             attunementArray = new Attunement[] {
                  new DefaultAttunement(), new HotAttunement(), new ColdAttunement(), new TropicalAttunement(), new EvilAttunement(),
                  new TrueDefaultAttunement(), new TrueHotAttunement(), new TrueColdAttunement(), new TrueTropicalAttunement(), new TrueEvilAttunement(), new HolyAttunement(), new AstralAttunement(), new MarineAttunement(),
                  new WhirlwindAttunement(), new FlailBladeAttunement(), new SuperPogoAttunement(), new ShockwaveAttunement(),
                  new PhoenixAttunement(), new AriesAttunement(), new PolarisAttunement(), new AndromedaAttunement(),
                  null
             };
        }

        public static void Unload()
        {
            attunementArray = null;
        }

        public AttunementID id;

        public string name;
        public string function_description;
        public string function_description_extra; //Used for Biome blade and onwards
        public string passive_name; //Used for galaxia
        public string passive_description; //Used for True Biome blade and onwards
        public Color tooltipColor;
        public Color tooltipColor2;
        public Color tooltipPassiveColor;

        public Color energyParticleEdgeColor;
        public Color energyParticleCenterColor;

        public virtual void ApplyStats(Item item)
        {
        }

        /// <summary>
        /// What does the sword do when trying to shoot and having the current attunement. Return false to stop the sword from shooting its default projectile
        /// </summary>
        /// <param name="player"></param>
        /// <param name="position"></param>
        /// <param name="speedX"></param>
        /// <param name="speedY"></param>
        /// <param name="type"></param>
        /// <param name="damage"></param>
        /// <param name="knockBack"></param>
        /// <param name="Combo">Used by a different attunements to check the combo state of the weapon</param>
        /// <param name="CanLunge">>Used by evil attunements to check if a regular dash is avaialble</param>
        /// <param name="PowerLungeCounter">Used by evil attunements to check if a strong dash is avaialble</param>
        /// <returns></returns>
        public virtual bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter) => true;

        public virtual void PassiveEffect(Player player, ref int UseTimer, ref bool Procced, Projectile projectile = null)
        {
        }
    }


    #region Broken Biome Blade Attunements
    public class DefaultAttunement : Attunement
    {
        public DefaultAttunement()
        {
            id = AttunementID.Default;
            name = "Pure Clarity";
            function_description = "Fires a weak projectile that crushes enemy defenses";
            tooltipColor = new Color(171, 180, 73);

            energyParticleEdgeColor = new Color(117, 126, 72);
            energyParticleCenterColor = new Color(200, 184, 136);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = BiomeBlade.DefaultAttunement_BaseDamage;
            item.channel = false;
            item.noUseGraphic = false;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shoot = ProjectileType<PurityProjection>();
            item.shootSpeed = 12f;
            item.UseSound = SoundID.Item1;
            item.noMelee = false;
        }
    }
    public class HotAttunement : Attunement
    {
        public HotAttunement()
        {
            id = AttunementID.Hot;
            name = "Arid Grandeur";
            function_description = "Conjures searing blades in front of you that get larger and stronger the more you hit enemies. The blades can also be used to bounce off tiles when in the air";
            tooltipColor = new Color(238, 156, 73);

            energyParticleEdgeColor = new Color(137, 32, 0);
            energyParticleCenterColor = new Color(209, 154, 0);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = BiomeBlade.HotAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<AridGrandeur>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }
    }
    public class ColdAttunement : Attunement
    {
        public ColdAttunement()
        {
            id = AttunementID.Cold;
            name = "Biting Embrace";
            function_description = "Perform a 3 strike combo with a glacial blade. The final strike freezes foes for a split second";
            tooltipColor = new Color(165, 235, 235);

            energyParticleEdgeColor = new Color(165, 235, 235);
            energyParticleCenterColor = new Color(58, 110, 141);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = BiomeBlade.ColdAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<BitingEmbrace>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
        {
            switch (Combo)
            {
                case 0:
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ProjectileType<BitingEmbrace>(), damage, knockBack, player.whoAmI, 0, 15);
                    break;

                case 1:
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ProjectileType<BitingEmbrace>(), damage, knockBack, player.whoAmI, 1, 20);
                    break;

                case 2:
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ProjectileType<BitingEmbrace>(), damage, knockBack, player.whoAmI, 2, 50);
                    break;
            }
            Combo++;
            if (Combo > 2)
                Combo = 0;
            return false;
        }
    }

    public class TropicalAttunement : Attunement
    {
        public TropicalAttunement()
        {
            id = AttunementID.Tropical;
            name = "Grovetender's Touch";
            function_description = "Throw out the blade using a vine whip. Striking enemies with the tip of the whip as it cracks guarantees a critical hit. The whip will also propel you towards struck tiles";
            tooltipColor = new Color(162, 200, 85);

            energyParticleEdgeColor = new Color(53, 112, 4);
            energyParticleCenterColor = new Color(131, 173, 39);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = BiomeBlade.TropicalAttunement_BaseDamage;
            item.channel = false;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shoot = ProjectileType<GrovetendersTouch>();
            item.shootSpeed = 30;
            item.UseSound = null;
            item.noMelee = true;
        }
    }

    public class EvilAttunement : Attunement
    {
        public EvilAttunement()
        {
            id = AttunementID.Evil;
            name = "Decay's Retort";
            function_description = "Lunge forward using a ghostly rapier projection that leeches life off any struck foes. You also get bounced away from hit targets";
            tooltipColor = new Color(211, 64, 147);

            energyParticleEdgeColor = new Color(112, 4, 35);
            energyParticleCenterColor = new Color(195, 42, 200);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = BiomeBlade.EvilAttunement_BaseDamage;
            item.channel = false;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.shoot = ProjectileType<DecaysRetort>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
        {
            Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ProjectileType<DecaysRetort>(), damage, knockBack, player.whoAmI, 26, (float)CanLunge);
            CanLunge = 0;
            return false;
        }
    }
    #endregion

    #region Biome Blade Attunements
    public class TrueDefaultAttunement : Attunement
    {
        public TrueDefaultAttunement()
        {
            id = AttunementID.TrueDefault;
            name = "Pure Clarity";
            function_description = "Fires a weak projectile that crushes enemy defenses";
            function_description_extra = "Landing true melee hits places a sigil of purity over the enemy. Your projectiles home onto the marked foes";
            tooltipColor = new Color(171, 180, 73);

            energyParticleEdgeColor = new Color(117, 126, 72);
            energyParticleCenterColor = new Color(200, 184, 136);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = TrueBiomeBlade.DefaultAttunement_BaseDamage;
            item.channel = false;
            item.noUseGraphic = false;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shoot = ProjectileType<TruePurityProjection>();
            item.shootSpeed = 12f;
            item.UseSound = SoundID.Item1;
            item.noMelee = false;
        }
    }
    public class TrueHotAttunement : Attunement
    {
        public TrueHotAttunement()
        {
            id = AttunementID.TrueHot;
            name = "Arid Grandeur";
            function_description = "Conjures searing blades in front of you that get larger and stronger the more you hit enemies. The blades can also be used to bounce off tiles when in the air";
            function_description_extra = "Releasing the attack button after the blades are fully charged sends them hurtling forwards to shred your foes";
            tooltipColor = new Color(238, 156, 73);

            energyParticleEdgeColor = new Color(137, 32, 0);
            energyParticleCenterColor = new Color(209, 154, 0);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = TrueBiomeBlade.HotAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<TrueAridGrandeur>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }
    }
    public class TrueColdAttunement : Attunement
    {
        public TrueColdAttunement()
        {
            id = AttunementID.TrueCold;
            name = "Biting Embrace";
            function_description = "Perform a 3 strike combo with a glacial blade. The final strike freezes foes for a split second";
            function_description_extra = "Lingering glacial mist is released on each swing, dealing damage to enemies wandering into it";
            tooltipColor = new Color(165, 235, 235);

            energyParticleEdgeColor = new Color(165, 235, 235);
            energyParticleCenterColor = new Color(58, 110, 141);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = TrueBiomeBlade.ColdAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<TrueBitingEmbrace>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
        {
            switch (Combo)
            {
                case 0:
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ProjectileType<TrueBitingEmbrace>(), damage, knockBack, player.whoAmI, 0, 15);
                    break;

                case 1:
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ProjectileType<TrueBitingEmbrace>(), damage, knockBack, player.whoAmI, 1, 20);
                    break;

                case 2:
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ProjectileType<TrueBitingEmbrace>(), damage, knockBack, player.whoAmI, 2, 50);
                    break;
            }
            Combo++;
            if (Combo > 2)
                Combo = 0;
            return false;
        }
    }

    public class TrueTropicalAttunement : Attunement
    {
        public TrueTropicalAttunement()
        {
            id = AttunementID.TrueTropical;
            name = "Grovetender's Touch";
            function_description = "Throw out the blade using a vine whip. Striking enemies with the tip of the whip as it cracks guarantees a critical hit. The whip will also propel you towards struck tiles";
            function_description_extra = "Spreading vines sprout from critical strikes. These vines can hit up to three targets";
            tooltipColor = new Color(162, 200, 85);

            energyParticleEdgeColor = new Color(53, 112, 4);
            energyParticleCenterColor = new Color(131, 173, 39);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = TrueBiomeBlade.TropicalAttunement_BaseDamage;
            item.channel = false;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shoot = ProjectileType<TrueGrovetendersTouch>();
            item.shootSpeed = 30;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
        {
            Projectile whipProj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), ProjectileType<TrueGrovetendersTouch>(), damage, knockBack, player.whoAmI, 0, 0);
            if (whipProj.modProjectile is TrueGrovetendersTouch whip)
                whip.flipped = Combo == 0 ? 1 : -1;
            Combo++;
            if (Combo > 1)
                Combo = 0;
            return false;
        }
    }

    public class TrueEvilAttunement : Attunement
    {
        public TrueEvilAttunement()
        {
            id = AttunementID.TrueEvil;
            name = "Decay's Retort";
            function_description = "Lunge forward using a ghostly rapier projection that leeches life off any struck foes. You also get bounced away from hit targets";
            function_description_extra = "Landing three strikes without touching the ground changes your next attack into an empowered dash thrust";
            tooltipColor = new Color(211, 64, 147);

            energyParticleEdgeColor = new Color(112, 4, 35);
            energyParticleCenterColor = new Color(195, 42, 200);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = TrueBiomeBlade.EvilAttunement_BaseDamage;
            item.channel = false;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.shoot = ProjectileType<TrueDecaysRetort>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
        {
            bool powerLungeAvailable = false;
            if (PowerLungeCounter == 3)
            {
                powerLungeAvailable = true;
                PowerLungeCounter = 0;
            }
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), ProjectileType<TrueDecaysRetort>(), damage, knockBack, player.whoAmI, 26f, CanLunge > 0 ? 1f : 0f);
            if (proj.modProjectile is TrueDecaysRetort rapier)
                rapier.ChargedUp = powerLungeAvailable;
            CanLunge--;
            if (CanLunge < 0)
                CanLunge = 0;
            return false;
        }
    }

    public class HolyAttunement : Attunement
    {
        public HolyAttunement()
        {
            id = AttunementID.Holy;
            name = "Heaven's Might";
            function_description = "Hold the attack button to swing the sword around you, accelerating over time";
            function_description_extra = "Releasing the attack button hurls the sword towards your cursor. The sword then quickly returns to your hand";
            tooltipColor = new Color(220, 143, 255);

            energyParticleEdgeColor = new Color(62, 55, 110);
            energyParticleCenterColor = new Color(255, 143, 255);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = TrueBiomeBlade.HolyAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<HeavensMight>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }
    }

    public class AstralAttunement : Attunement
    {
        public AstralAttunement()
        {
            id = AttunementID.Astral;
            name = "Extant Abhorrence";
            function_description = "Hold the attack button to charge a star-piercing lunge, release to unleash the devastating blow";
            function_description_extra = "Striking the earth with the lunge will cause a massive impact which raises otherworldly monoliths";
            tooltipColor = new Color(91, 73, 196);

            energyParticleEdgeColor = new Color(58, 56, 165);
            energyParticleCenterColor = new Color(153, 120, 255);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = TrueBiomeBlade.AstralAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.shoot = ProjectileType<ExtantAbhorrence>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }
    }

    public class MarineAttunement : Attunement
    {
        public MarineAttunement()
        {
            id = AttunementID.Marine;
            name = "Gesture for the Drowned";
            function_description = "Shoot out two water balls at your sides. On ground contact the water balls will turn into small waves that ride along the ground";
            function_description_extra = "Waves fired while underwater or that touch water will become much larger and faster";
            tooltipColor = new Color(91, 73, 196);

            energyParticleEdgeColor = new Color(27, 59, 101);
            energyParticleCenterColor = new Color(27, 112643, 255);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = TrueBiomeBlade.MarineAttunement_BaseDamage;
            item.channel = false;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.shoot = ProjectileType<GestureForTheDrowned>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }
    }
    #endregion

    #region True Biome Blade Attunements
    public class WhirlwindAttunement : Attunement
    {
        public WhirlwindAttunement()
        {
            id = AttunementID.Whirlwind;
            name = "Swordsmith's Pride";
            function_description = "Hold the attack button to swing the sword around you, accelerating over time. Sword beams are fired wildly while swinging";
            function_description_extra = "Releasing the attack button hurls the sword towards your cursor. Sword beams home onto enemies struck by the thrown sword";
            passive_description = "All attacks have a chance to fire sword beams";
            tooltipColor = new Color(220, 105, 197);
            tooltipColor2 = new Color(171, 239, 113);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = OmegaBiomeBlade.WhirlwindAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<SwordsmithsPride>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override void PassiveEffect(Player player, ref int UseTimer, ref bool Procced, Projectile projectile = null)
        {
            if (UseTimer % 30 == 29 && Main.rand.Next(2) == 0)
            {
                Main.PlaySound(SoundID.Item78);
                Projectile beamSword = Projectile.NewProjectileDirect(player.Center, player.DirectionTo(Main.MouseWorld) * 15f, ProjectileType<SwordsmithsPrideBeam>(), (int)(OmegaBiomeBlade.WhirlwindAttunement_PassiveBaseDamage * player.MeleeDamage()), 10f, player.whoAmI, 1f);
                beamSword.timeLeft = 50;
                UseTimer++;
            }
        }
    }

    public class FlailBladeAttunement : Attunement
    {
        public FlailBladeAttunement()
        {
            id = AttunementID.FlailBlade;
            name = "Lamentations of the Chained";
            function_description = "Lash out with a flurry of chained blades. Striking enemies with the tip of the blades guarantees a critical hit";
            function_description_extra = "Critical strikes send out extra phantom chains which latch onto multiple targets";
            passive_description = "An enchanted chain hook floats near you. Striking enemies makes the hook whirl around you, freezing enemies"; //No way sentient meat hook
            tooltipColor = new Color(113, 239, 177);
            tooltipColor2 = new Color(169, 207, 255);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = OmegaBiomeBlade.FlailBladeAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<LamentationsOfTheChained>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override void PassiveEffect(Player player, ref int UseTimer, ref bool Procced, Projectile projectile = null)
        {
            if (Procced)
            {
                if (projectile.modProjectile is ChainedMeatHook hook && hook.Twirling == 0f)
                {
                    hook.Twirling = 1f;
                    hook.projectile.timeLeft = (int)ChainedMeatHook.MaxTwirlTime;
                }
                Procced = false;
            }
        }
    }

    public class SuperPogoAttunement : Attunement
    {
        public SuperPogoAttunement()
        {
            id = AttunementID.SuperPogo;
            name = "Sanguine Fury";
            function_description = "Conjures molten blades in front of you that get larger and stronger the more you hit enemies. The blades can also be used to bounce off tiles when in the air";
            function_description_extra = "Releasing the attack button sends the charged blades hurtling forwards like a wheel. Attacking again immediately thereafter dashes to the wheel, shredding anything in the way";
            passive_description = "Striking enemies rarely steals a small amount of life";
            tooltipColor = new Color(216, 55, 22);
            tooltipColor2 = new Color(216, 131, 22);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = OmegaBiomeBlade.SuperPogoAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<SanguineFury>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override void PassiveEffect(Player player, ref int UseTimer, ref bool Procced, Projectile projectile = null)
        {
            if (Procced)
            {
                player.statLife += OmegaBiomeBlade.SuperPogoAttunement_PassiveLifeSteal;
                player.HealEffect(OmegaBiomeBlade.SuperPogoAttunement_PassiveLifeSteal);

                Procced = false;
            }
        }
    }

    public class ShockwaveAttunement : Attunement
    {
        public ShockwaveAttunement()
        {
            id = AttunementID.Shockwave;
            name = "Mercurial Tides";
            function_description = "Hold the attack button to charge a heaven-shattering lunge, release to unleash the devastating blow. Shock waves are emitted as the sword charges up";
            function_description_extra = "Striking the earth with the lunge will cause a massive shock wave on impact which raises a flood of monoliths";
            passive_description = "Periodically emit shock waves while attacking";
            tooltipColor = new Color(132, 109, 233);
            tooltipColor2 = new Color(122, 213, 233);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = OmegaBiomeBlade.ShockwaveAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<MercurialTides>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override void PassiveEffect(Player player, ref int UseTimer, ref bool Procced, Projectile projectile = null)
        {
            if (UseTimer % 120 == 119)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileType<MercurialTidesBlast>(), (int)(OmegaBiomeBlade.ShockwaveAttunement_PassiveBaseDamage * player.MeleeDamage()), 10f, player.whoAmI, 1f);
                UseTimer++;
            }
        }
    }

    #endregion

    #region Galaxia Attunements

    public class PhoenixAttunement : Attunement
    {
        public PhoenixAttunement()
        {
            id = AttunementID.Phoenix;
            name = "Phoenix's Pride";
            function_description = "Hold the attack button to swing Galaxia around you, accelerating over time. The swirling nebula constantly emits homing cosmic bolts";
            function_description_extra = "Releasing the attack button hurls the sword towards your cursor alongside a burst of 6 gigantic cosmic bolts. The sword then quickly returns to your hand";
            tooltipColor = new Color(255, 87, 0);
            tooltipColor2 = new Color(255, 143, 0);

            passive_name = "Capricorn's Blessing";
            passive_description = "Periodically releases a ring of weakening stars around your cursor when attacking\nThe ring is repelled away from you at first, before slowing down over time";
            tooltipPassiveColor = new Color(76, 137, 237);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = FourSeasonsGalaxia.PhoenixAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<PhoenixsPride>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override void PassiveEffect(Player player, ref int UseTimer, ref bool Procced, Projectile projectile = null)
        {
            if (UseTimer % 500 == 449)
            {
                Main.PlaySound(SoundID.Item78);
                Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ProjectileType<GalaxiaTropicRing>(), 0, 0f, player.whoAmI, 1f);
                UseTimer++;
            }
        }
    }

    public class AriesAttunement : Attunement
    {
        public AriesAttunement()
        {
            id = AttunementID.Aries;
            name = "Aries's Wrath";
            function_description = "Throw Galaxia to your cursor, holding it in place with constellations. Galaxia follows your cursor, rapidly spinning to shred enemies";
            function_description_extra = "Striking enemies causes nebulous explosions which spawn homing cosmic bolts";
            tooltipColor = new Color(196, 89, 201);
            tooltipColor2 = new Color(255, 0, 0);

            passive_name = "Capricorn's Blessing";
            passive_description = "Periodically releases a ring of weakening stars around your cursor when attacking\nThe ring is repelled away from you at first, before slowing down over time";
            tooltipPassiveColor = new Color(76, 137, 237);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = FourSeasonsGalaxia.AriesAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<AriesWrath>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override void PassiveEffect(Player player, ref int UseTimer, ref bool Procced, Projectile projectile = null)
        {
            if (UseTimer % 500 == 449)
            {
                Main.PlaySound(SoundID.Item78);
                Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ProjectileType<GalaxiaTropicRing>(), 0, 0f, player.whoAmI, 1f);
                UseTimer++;
            }
        }
    }

    public class PolarisAttunement : Attunement
    {
        public PolarisAttunement()
        {
            id = AttunementID.Polaris;
            name = "Polaris's Gaze"; //It carries the mark of the Northern Star
            function_description = "Hold the attack button to channel the Northern Star's power into a tight vortex of galactic energies, bound by concentric rings of stars. The vortex powers up over time and when hitting enemies";
            function_description_extra = "Release the attack button to fire off the charged star. Attacking again immediately thereafter dashes to the star, emitting a nova of cosmic bolts";
            tooltipColor = new Color(128, 189, 255);
            tooltipColor2 = new Color(255, 128, 140);

            passive_name = "Cancer's Blessing";
            passive_description = "Periodically releases a ring of lifestealing stars around yourself when attacking\nThe ring is attracted towards you at first, before slowing down over time";
            tooltipPassiveColor = new Color(203, 25, 119);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = FourSeasonsGalaxia.PolarisAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<PolarisGaze>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override void PassiveEffect(Player player, ref int UseTimer, ref bool Procced, Projectile projectile = null)
        {
            if (UseTimer % 500 == 449)
            {
                Main.PlaySound(SoundID.Item78);
                Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileType<GalaxiaTropicRing>(), FourSeasonsGalaxia.CancerPassiveDamage, 0f, player.whoAmI, 0f);
                UseTimer++;
            }
        }
    }

    public class AndromedaAttunement : Attunement
    {
        public AndromedaAttunement()
        {
            id = AttunementID.Andromeda;
            name = "Andromeda's Stride"; //EHEEHEHEHE GOD ERASING BECAUSE THE ANDROMEDA BOSS WAS SCRAPPED (ALSO KNOWN AS A "GOD" BEING "ERASED") EHEHEHE
            function_description = "Hold the attack button to charge a god-erasing lunge, release to unleash the devastating blow. Galaxia emits cosmic bolts while charging";
            function_description_extra = "Striking the earth with the lunge will cause a massive impact which summons an apocalyptic storm of cosmic energies";
            tooltipColor = new Color(132, 128, 255);
            tooltipColor2 = new Color(194, 166, 255);

            passive_name = "Cancer's Blessing";
            passive_description = "Periodically releases a ring of lifestealing stars around yourself when attacking\nThe ring is attracted towards you at first, before slowing down over time";
            tooltipPassiveColor = new Color(203, 25, 119);
        }

        public override void ApplyStats(Item item)
        {
            item.damage = FourSeasonsGalaxia.AndromedaAttunement_BaseDamage;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<AndromedasStride>();
            item.shootSpeed = 12f;
            item.UseSound = null;
            item.noMelee = true;
        }

        public override void PassiveEffect(Player player, ref int UseTimer, ref bool Procced, Projectile projectile = null)
        {
            if (UseTimer % 500 == 449)
            {
                Main.PlaySound(SoundID.Item78);
                Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileType<GalaxiaTropicRing>(), FourSeasonsGalaxia.CancerPassiveDamage, 0f, player.whoAmI, 0f);
                UseTimer++;
            }
        }
    }

    #endregion
}