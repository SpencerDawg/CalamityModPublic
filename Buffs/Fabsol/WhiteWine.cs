﻿using Terraria;
using Terraria.ModLoader;
using CalamityMod.CalPlayer;

namespace CalamityMod.Buffs.Fabsol
{
    public class WhiteWine : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("White Wine");
			Description.SetDefault("Magic damage boosted, life regen and defense reduced");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			longerExpertDebuff = false;
            canBeCleared = false;
        }

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer>(mod).whiteWine = true;
		}
	}
}
