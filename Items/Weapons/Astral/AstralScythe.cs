using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Astral
{
    public class AstralScythe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Astral Scythe");
			Tooltip.SetDefault("Shoots a scythe ring that accelerates over time");
		}

		public override void SetDefaults()
		{
			item.width = 56;
			item.height = 60;
			item.damage = 95;
			item.melee = true;
			item.useTurn = true;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.useTime = 20;
			item.knockBack = 6f;
			item.UseSound = SoundID.Item71;
			item.autoReuse = true;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 7;
            item.shoot = mod.ProjectileType("AstralScytheProjectile");
			item.shootSpeed = 5f;
		}
	}
}
