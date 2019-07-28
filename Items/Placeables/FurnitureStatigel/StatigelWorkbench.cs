using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStatigel
{
    public class StatigelWorkbench : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
        {
            item.SetNameOverride("Statigel Work Bench");
            item.width = 28;
			item.height = 14;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("StatigelWorkbench");
		}

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StatigelBlock", 10);
            recipe.SetResult(this, 1);
            recipe.AddTile(null, "StaticRefiner");
            recipe.AddRecipe();
        }
	}
}
