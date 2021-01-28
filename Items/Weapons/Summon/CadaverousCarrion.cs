using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon
{
    public class CadaverousCarrion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cadaverous Carrion");
            Tooltip.SetDefault("Summons a gross Old Duke head on the ground");
        }

        public override void SetDefaults()
        {
            item.damage = 416;
            item.mana = 32;
            item.summon = true;
            item.sentry = true;
            item.width = 54;
            item.height = 56;
            item.useTime = item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.buyPrice(1, 40, 0, 0);
            item.Calamity().postMoonLordRarity = 13;
			item.rare = 10;
            item.UseSound = SoundID.NPCDeath13;
            item.shoot = ModContent.ProjectileType<OldDukeHeadCorpse>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			//CalamityUtils.OnlyOneSentry(player, type);
			Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, type, damage, knockBack, player.whoAmI);
			player.UpdateMaxTurrets();
            return false;
        }
    }
}
