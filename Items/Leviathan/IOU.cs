﻿using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Leviathan
{
    public class IOU : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("IOU an item");
			Tooltip.SetDefault("Use to craft any Leviathan weapon you want\nCombine with Living Shards from Plantera to get your item!");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.value = Item.buyPrice(0, 0, 10, 0);
			item.rare = 1;
		}
	}
}
