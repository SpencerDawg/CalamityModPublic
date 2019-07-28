using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.RareVariants
{
    public class Norfleet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Norfleet");
		}

	    public override void SetDefaults()
	    {
            item.damage = 1280;
            item.knockBack = 15f;
            item.shootSpeed = 30f;
            item.useStyle = 5;
            item.useAnimation = 75;
            item.useTime = 75;
            item.reuseDelay = 0;
            item.width = 140;
            item.height = 42;
            item.UseSound = SoundID.Item92;
            item.shoot = mod.ProjectileType("Norfleet");
            item.value = Item.buyPrice(1, 80, 0, 0);
            item.rare = 10;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.ranged = true;
            item.channel = true;
            item.useTurn = false;
            item.useAmmo = 75;
            item.autoReuse = true;
			item.GetGlobalItem<CalamityGlobalItem>(mod).postMoonLordRarity = 22;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Norfleet"), 0, 0f, player.whoAmI);
            return false;
		}
	}
}
