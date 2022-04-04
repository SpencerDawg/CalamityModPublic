﻿using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace CalamityMod.Items.Accessories
{
    public class SandCloak : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Cloak");
            Tooltip.SetDefault("+1 defense and 5% increased movement speed\n" +
                "TOOLTIP LINE HERE\n" +
                "This effect has a 30 second cooldown before it can be used again");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 44;
            Item.value = CalamityGlobalItem.Rarity2BuyPrice;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            string hotkey = CalamityKeybinds.SandCloakHotkey.GetAssignedKeys().Aggregate((x, y) => x + ", " + y);
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "Tooltip1")
                {
                    line2.text = "Press " + hotkey + " to consume 25% of your maximum stealth to create a protective dust veil which provides +6 defense and +2 life regen";
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 1;
            player.moveSpeed += 0.05f;
            player.Calamity().sandCloak = true;
        }
    }
}
