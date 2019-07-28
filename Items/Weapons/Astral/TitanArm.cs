using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Astral
{
    public class TitanArm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Titan Arm");
			Tooltip.SetDefault("Slap Hand but better");
		}

		public override void SetDefaults()
		{
			item.width = 46;
			item.height = 58;
			item.damage = 60;
			item.crit += 96; //more knockback huehue
			item.melee = true;
			item.useTurn = true;
			item.useAnimation = 12;
			item.useStyle = 1;
			item.useTime = 12;
			item.knockBack = 9001f;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 7;
        }
	}
}
