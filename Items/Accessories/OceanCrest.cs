using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories
{
    public class OceanCrest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Crest");
            Tooltip.SetDefault("Most ocean enemies become friendly and provides waterbreathing");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 26;
            item.value = CalamityGlobalItem.Rarity2BuyPrice;
            item.rare = ItemRarityID.Green;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.npcTypeNoAggro[NPCID.Shark] = true;
            player.npcTypeNoAggro[NPCID.SeaSnail] = true;
            player.npcTypeNoAggro[NPCID.PinkJellyfish] = true;
            player.npcTypeNoAggro[NPCID.Crab] = true;
            player.npcTypeNoAggro[NPCID.Squid] = true;
            player.gills = true;
        }
    }
}
