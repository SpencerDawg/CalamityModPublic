﻿using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems
{
    public class Knowledge25 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plantera");
			Tooltip.SetDefault("Well done, you killed a plant.\n" +
                "It was used as a vessel to house the spirits of those unfortunate enough to find their way down here.\n" +
                "I wish you luck in dealing with the fallout.\n" +
				"Place in your inventory to gain a boost to your item grab range.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = 6;
			item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(mod);
			modPlayer.planteraLore = true;
		}
	}
}
