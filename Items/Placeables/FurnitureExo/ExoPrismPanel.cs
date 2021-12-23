using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureExo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityMod.Items.Placeables.FurnitureExo
{
    public class ExoPrismPanel : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<ExoPrismPanelTile>();
            item.Calamity().customRarity = CalamityRarity.Violet;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.GetTexture("CalamityMod/Items/Placeables/FurnitureExo/ExoPrismPanel_Glow"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Glass, 200);
            recipe.AddIngredient(ModContent.ItemType<ExoPrism>());
            recipe.SetResult(this, 200);
            recipe.AddTile(ModContent.TileType<DraedonsForge>());
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<ExoPrismPlatform>(), 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
