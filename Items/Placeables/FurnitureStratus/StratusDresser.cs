using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStratus
{
    public class StratusDresser : ModItem
	{
		public override void SetStaticDefaults()
        {
        }

		public override void SetDefaults()
        {
            item.width = 26;
			item.height = 26;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("StratusDresser");
		}

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StratusBricks", 16);
            recipe.SetResult(this, 1);
            recipe.AddTile(412);
            recipe.AddRecipe();
        }
	}
}
