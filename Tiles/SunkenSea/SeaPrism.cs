﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea
{
    public class SeaPrism : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            CalamityUtils.MergeWithGeneral(Type);
            CalamityUtils.MergeWithDesert(Type);

            TileID.Sets.ChecksForMerge[Type] = true;
            DustType = 33;
            ItemDrop = ModContent.ItemType<Items.Placeables.SeaPrism>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Sea Prism");
            AddMapEntry(new Color(0, 150, 200), name);
            MineResist = 3f;
            HitSound = SoundID.Tink;
            Main.tileSpelunker[Type] = true;
            TileID.Sets.DoesntGetReplacedWithTileReplacement[Type] = true; //TODO -- Temporary, allow tile to be swapped when DS is dead.
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return DownedBossSystem.downedDesertScourge;
        }

        public override bool CanExplode(int i, int j)
        {
            return DownedBossSystem.downedDesertScourge;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            TileFraming.CustomMergeFrame(i, j, Type, ModContent.TileType<Navystone>(), false, false, false, false, resetFrame);
            return false;
        }
    }
}
