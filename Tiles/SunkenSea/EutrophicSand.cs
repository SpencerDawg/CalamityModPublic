﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea
{
    public class EutrophicSand : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            CalamityUtils.MergeWithGeneral(Type);
            CalamityUtils.MergeWithDesert(Type);

            Main.tileShine[Type] = 2000;
            Main.tileShine2[Type] = true;

            TileID.Sets.ChecksForMerge[Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;

            DustType = 108;
            ItemDrop = ModContent.ItemType<Items.Placeables.EutrophicSand>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Eutrophic Sand");
            AddMapEntry(new Color(92, 145, 167), name);
            MineResist = 2f;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            TileFraming.CustomMergeFrame(i, j, Type, TileID.Sandstone, true, false, false);
            return false;

            //return TileFraming.BrimstoneFraming(i, j, resetFrame);
        }
    }
}
