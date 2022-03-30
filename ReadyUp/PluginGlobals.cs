﻿using RoR2;
using System.Collections.Generic;

namespace jbpaul
{
    public static class PluginGlobals
    {
        //Lol is there better way to store/check stages?!?
        public static List<string> IgnoredStages = new List<string>
        {
            "arena", //void
            "moon",
            "moon2",
            "limbo",
            "outro",
        };

        public static List<string> PortalOnlyStages = new List<string>
        {
            "bazaar",
            "goldshores",
            "artifactworld",
            "mysteryspace",
        };

        public static uint CurrentCountdownTime;
    }
}
