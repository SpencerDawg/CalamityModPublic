﻿using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories
{
    public class MoonstoneCrown : ModItem
    {
        // Base damage of lunar flares on stealth strikes. Increased by rogue damage stats, but not stealth damage.
        internal const int BaseDamage = 85;

        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Mod.AddEquipTexture(this, EquipType.Head, "CalamityMod/Items/Accessories/MoonstoneCrown_Face");
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonstone Crown");
            Tooltip.SetDefault("15% increased rogue projectile velocity\n" +
                "Stealth strikes summon lunar flares on enemy hits\n" +
                "Rogue projectiles very occasionally summon moon sigils behind them");

            int equipSlot = Mod.GetEquipSlot(Name, EquipType.Head);
            ArmorIDs.Head.Sets.DrawFullHair[equipSlot] = false;
            ArmorIDs.Head.Sets.DrawHatHair[equipSlot] = false;
        }

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 40;
            Item.value = CalamityGlobalItem.Rarity11BuyPrice;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.Calamity();
            modPlayer.throwingVelocity += 0.15f;
            modPlayer.moonCrown = true;
            if (!hideVisual)
                modPlayer.moonCrownDraw = true; //this bool is just used for drawing
        }

        public override void UpdateVanity(Player player)
        {
            player.Calamity().moonCrownDraw = true; //this bool is just used for drawing
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<FeatherCrown>()
                .AddIngredient(ItemID.LunarBar, 5)
                .AddIngredient<GalacticaSingularity>(5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
