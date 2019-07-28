﻿using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems
{
    public class Knowledge40 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Sentinels of the Devourer");
			Tooltip.SetDefault("Signus. The Void. The Weaver.\n" +
				"Each represent one of the Devourer’s largest spheres of influence.\n" +
				"Dispatching them has most likely invoked its anger and marked you as a target for destruction.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = 10;
			item.consumable = false;
			item.GetGlobalItem<CalamityGlobalItem>(mod).postMoonLordRarity = 13;
		}

		public override bool CanUseItem(Player player)
		{
			return false;
		}
	}
}
