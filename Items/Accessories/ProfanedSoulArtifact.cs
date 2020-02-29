﻿using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Placeables.Ores;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories
{
    public class ProfanedSoulArtifact : ModItem //My precious babs <3 ~Amber
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Profaned Soul Artifact");
            Tooltip.SetDefault("Purity\n" +
                "Summons a healer guardian which heals for a certain amount of health every few seconds\n" +
                "Summons a defensive guardian if you have at least 10 minion slots, which boosts your movement speed and your damage resistance\n" +
                "Summons an offensive guardian if you are wearing the tarragon summon set (or stronger), which boosts your summon damage and your minion slots\n" +
                "If you get hit, their effects will disappear for 5 seconds");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 6));
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 40;
            item.value = Item.buyPrice(1, 40, 0, 0);
            item.rare = 10;
            item.accessory = true;
            item.Calamity().customRarity = CalamityRarity.Dedicated;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.Calamity();
            modPlayer.pArtifact = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Cinderplate>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CoreofCalamity>());
            recipe.AddIngredient(ModContent.ItemType<DivineGeode>(), 10);
            recipe.AddIngredient(ModContent.ItemType<ExodiumClusterOre>(), 15);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
