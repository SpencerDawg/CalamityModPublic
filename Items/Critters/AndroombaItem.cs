﻿using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Critters
{
    public class AndroombaItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 5;
            DisplayName.SetDefault("Androomba");
            Tooltip.SetDefault("Right click the roomba with a solution to insert it\n"+"While a solution is inserted, the roomba will start spreading its contents");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.value = Item.buyPrice(0, 0, 30, 0);
            Item.width = 36;
            Item.height = 16;
            Item.makeNPC = (short)ModContent.NPCType<AndroombaFriendly>();
            Item.rare = ModContent.RarityType<DarkOrange>();
        }
    }
}