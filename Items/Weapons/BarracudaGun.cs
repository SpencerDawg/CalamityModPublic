using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons
{
    public class BarracudaGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Barracuda Gun");
			Tooltip.SetDefault("Fires two barracudas that latch onto enemies");
		}

	    public override void SetDefaults()
	    {
	        item.damage = 76;
	        item.channel = true;
	        item.ranged = true;
	        item.width = 54;
	        item.height = 28;
	        item.useTime = 20;
	        item.useAnimation = 20;
	        item.useStyle = 5;
	        item.noMelee = true;
	        item.knockBack = 1f;
            item.value = Item.buyPrice(0, 80, 0, 0);
            item.rare = 8;
	        item.UseSound = SoundID.Item10;
	        item.autoReuse = true;
	        item.shootSpeed = 15f;
	        item.shoot = mod.ProjectileType("MechanicalBarracuda");
	    }

	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

	    public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	    {
			float SpeedA = speedX;
	   		float SpeedB = speedY;
	        int num6 = Main.rand.Next(2, 3);
	        for (int index = 0; index < num6; ++index)
	        {
	      	 	float num7 = speedX;
	            float num8 = speedY;
	            float SpeedX = speedX + (float) Main.rand.Next(-10, 11) * 0.05f;
	            float SpeedY = speedY + (float) Main.rand.Next(-10, 11) * 0.05f;
	            Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage), knockBack, player.whoAmI, 0.0f, 0.0f);
	        }
	        return false;
		}

	    public override void AddRecipes()
	    {
	        ModRecipe recipe = new ModRecipe(mod);
	        recipe.AddIngredient(ItemID.PiranhaGun);
	        recipe.AddIngredient(null, "CoreofCalamity", 2);
            recipe.AddIngredient(null, "Tenebris", 5);
            recipe.AddIngredient(ItemID.SharkFin, 2);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.SetResult(this);
	        recipe.AddRecipe();
	    }
	}
}
