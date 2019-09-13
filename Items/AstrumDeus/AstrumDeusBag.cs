using Terraria;
using Terraria.ModLoader;
using CalamityMod.Utilities;

namespace CalamityMod.Items.AstrumDeus
{
	public class AstrumDeusBag : ModItem
	{
		public override int BossBagNPC => mod.NPCType("AstrumDeusHeadSpectral");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.expert = true;
			item.rare = 9;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			player.TryGettingDevArmor();

			// Materials
			DropHelper.DropItem(player, mod.ItemType("Stardust"), 60, 90);

			// Weapons
			DropHelper.DropItemChance(player, mod.ItemType("Starfall"), 4);
			DropHelper.DropItemChance(player, mod.ItemType("Quasar"), DropHelper.RareVariantDropRateInt);

			// Equipment
			float f = Main.rand.NextFloat();
			bool replaceWithRare = f <= DropHelper.RareVariantDropRateFloat; // 1/40 chance of getting Hide of Astrum Deus
			DropHelper.DropItemCondition(player, mod.ItemType("AstralBulwark"), !replaceWithRare);
			DropHelper.DropItemCondition(player, mod.ItemType("HideofAstrumDeus"), replaceWithRare);

			// Vanity
			DropHelper.DropItemChance(player, mod.ItemType("AstrumDeusMask"), 7);
		}
	}
}
