using BepInEx.Configuration;
using System.Collections.Generic;

namespace jbpaul
{
    class PluginConfig
    {
        public static ConfigEntry<string>
            PlayerIsReadyMessages;

        public static ConfigEntry<uint>
            MajorityVotesCountdownTimeBoss;

        public static ConfigEntry<uint>
            MajorityVotesCountdownTime;

        public static ConfigEntry<uint>
        LevelTimeWarningMinutes;
    }
}
