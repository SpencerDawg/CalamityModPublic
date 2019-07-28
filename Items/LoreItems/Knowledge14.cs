﻿using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems
{
    public class Knowledge14 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Hive Mind");
			Tooltip.SetDefault("A hive of clustered microbial-infected flesh.\n" +
                "I do not believe killing it will lessen the corruption here.\n" +
				"Place in your inventory for all of your projectiles to inflict cursed flames when in the corruption.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = 3;
			item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			if (player.ZoneCorrupt)
			{
				CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(mod);
				modPlayer.hiveMindLore = true;
			}
		}
	}
}
