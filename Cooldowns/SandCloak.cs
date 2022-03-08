﻿using Microsoft.Xna.Framework;

namespace CalamityMod.Cooldowns
{
    public class SandCloak : CooldownHandler
    {
        public static string ID => "SandCloak";
        public SandCloak(CooldownInstance? c) : base(c) { }

        public override bool ShouldDisplay => true;
        public override string DisplayName => "Sand Cloak Cooldown";
        public override string Texture => "CalamityMod/Cooldowns/SandCloak";
        public override Color OutlineColor => new Color(209, 176, 114);
        public override Color CooldownStartColor => new Color(100, 64, 44);
        public override Color CooldownEndColor => new Color(132, 95, 54);
    }
}