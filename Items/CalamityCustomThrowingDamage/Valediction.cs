using Terraria;
using Terraria.ID;

namespace CalamityMod.Items.CalamityCustomThrowingDamage
{
    public class Valediction : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Valediction");
			Tooltip.SetDefault("Throws a homing reaper scythe");
		}

		public override void SafeSetDefaults()
		{
			item.width = 80;
			item.damage = 405;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = 1;
			item.knockBack = 7f;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.height = 64;
            item.value = Item.buyPrice(1, 40, 0, 0);
            item.rare = 10;
            item.shoot = mod.ProjectileType("Valediction");
			item.shootSpeed = 20f;
			item.GetGlobalItem<CalamityGlobalItem>(mod).rogue = true;
			item.GetGlobalItem<CalamityGlobalItem>(mod).postMoonLordRarity = 13;
		}
	}
}
