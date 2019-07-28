using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons
{
    public class Quagmire : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Quagmire");
			Tooltip.SetDefault("Fires spore clouds");
		}

	    public override void SetDefaults()
	    {
	    	item.CloneDefaults(ItemID.HelFire);
	        item.damage = 52;
	        item.useTime = 22;
	        item.useAnimation = 22;
	        item.useStyle = 5;
	        item.channel = true;
	        item.melee = true;
	        item.knockBack = 3.5f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 7;
	        item.autoReuse = true;
	        item.shoot = mod.ProjectileType("QuagmireProjectile");
	    }

	    public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
	        recipe.AddIngredient(null, "DraedonBar", 6);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.SetResult(this);
	        recipe.AddRecipe();
		}
	}
}
