using BepInEx;
using RoR2;

namespace Valter
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class ReadyUpPlugin : BaseUnityPlugin
    {
        public const string ModVer = "1.0.0";
        public const string ModName = "ReadyUp";
        public const string ModGuid = "com.Valter.ReadyUp";

        public TPLockerController TPLockerController { get; private set; }

        public ReadyUpPlugin()
        {
            InitConfig();
        }

        public void Awake()
        {
            On.RoR2.Run.Awake += Run_Awake;
            On.RoR2.Run.OnDestroy += Run_OnDestroy;
        }

        private void Run_Awake(On.RoR2.Run.orig_Awake orig, Run self)
        {
            TPLockerController = base.gameObject.AddComponent<TPLockerController>();

            orig(self);
        }

        private void Run_OnDestroy(On.RoR2.Run.orig_OnDestroy orig, Run self)
        {
            Destroy(TPLockerController);

            orig(self);
        }

        private void InitConfig()
        {
            PluginConfig.PlayerIsReadyMessages = Config.Bind<string>(
                "Settings",
                "PlayerIsReadyMessages",
                "r,rdy,ready",
                "The message the player has to write in the chat to confirm they are ready. Values must be separated by comma."
            );

            PluginConfig.MajorityVotesCountdownTimeBoss = Config.Bind<uint>(
                "Settings",
                "MajorityVotesCountdownTimeBoss",
                10,
                "Countdown in seconds to unlock the teleporter boss fight when half or most of the players are ready."
            );

            PluginConfig.MajorityVotesCountdownTime = Config.Bind<uint>(
                "Settings",
                "MajorityVotesCountdownTime",
                60,
                "Countdown in seconds to unlock the teleporter when half or most of the players are ready."
            );

            PluginConfig.LevelTimeWarningMinutes = Config.Bind<uint>(
                "Settings",
                "LevelTimeWarningMinutes",
                4,
                "Number of minutes from the beginning of the level to warn the player about."
            );
        }
    }
}
