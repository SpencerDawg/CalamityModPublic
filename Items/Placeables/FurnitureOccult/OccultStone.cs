﻿using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureOccult
{
    public class OccultStone : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            item.SetNameOverride("Otherworldly Stone");
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("OccultStone");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 150);
			recipe.AddIngredient(mod.ItemType("DarkPlasma"));
			recipe.AddIngredient(mod.ItemType("ArmoredShell"));
			recipe.AddIngredient(mod.ItemType("TwistingNether"));
			recipe.AddIngredient(ItemID.Silk, 15);
            recipe.SetResult(this, 150);
            recipe.AddTile(null, "DraedonsForge");
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OccultStoneWall", 4);
            recipe.SetResult(this);
            recipe.AddTile(18);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OccultPlatform", 2);
            recipe.SetResult(this);
            recipe.AddTile(null, "DraedonsForge");
            recipe.AddRecipe();
        }
    }
}
