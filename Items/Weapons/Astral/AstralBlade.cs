﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Astral
{
    public class AstralBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Blade");
        }

        public override void SetDefaults()
        {
            item.damage = 135;
            item.crit += 25;
            item.melee = true;
            item.width = 60;
            item.height = 60;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useTurn = true;
            item.useStyle = 1;
            item.knockBack = 5f;
            item.value = Item.buyPrice(0, 95, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AstralBar", 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust d = CalamityGlobalItem.MeleeDustHelper(player, Main.rand.Next(2) == 0 ? mod.DustType("AstralOrange") : mod.DustType("AstralBlue"), 0.7f, 55, 110, -0.07f, 0.07f);
                if (d != null)
                {
                    d.customData = 0.03f;
                }
            }
        }
    }
}
