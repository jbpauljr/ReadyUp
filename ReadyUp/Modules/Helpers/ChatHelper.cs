namespace Valter
{
    public static class ChatHelper
    {
        private const string GrayColor = "7e91af";
        private const string RedColor = "ff0000";
        private const string YellowColor = "ffff00";
        private const string GreenColor = "32cd32";
        private const string SilverColor = "c0c0c0";


        public static void UserIsReady(string userName, int numberOfUsersWhoVoted, int numberOfVotingUsers)
        {
            var message = $"<color=#{GreenColor}>{userName}</color> is ready! <color=#{GrayColor}>({numberOfUsersWhoVoted}/{numberOfVotingUsers})</color>";
            RoR2.Chat.SendBroadcastChat(new RoR2.Chat.SimpleChatMessage { baseToken = message });
        }

        public static void TPUnlocked()
        {
            var message = $"<color=#{RedColor}>Teleporter</color> is unlocked! <color=#{GreenColor}>Players are free to start it.</color>";
            RoR2.Chat.SendBroadcastChat(new RoR2.Chat.SimpleChatMessage { baseToken = message });
        }

        public static void PlayersNotReady()
        {
            var message = $"<color=#{RedColor}>Players are not ready.</color> Ready up by writing <color=#{SilverColor}>\"r\"</color> in chat or activating the Teleporter.";
            RoR2.Chat.SendBroadcastChat(new RoR2.Chat.SimpleChatMessage { baseToken = message });
        }

        public static void VotingInstruction()
        {
            var message = $"Ready up for the <color=#{RedColor}>Teleporter</color> by writing <color=#{SilverColor}>\"r\"</color> in chat.";
            RoR2.Chat.SendBroadcastChat(new RoR2.Chat.SimpleChatMessage { baseToken = message });
        }

        public static void TPCountdown(uint unlockTime)
        {
            var message = $"Starting <color=#{RedColor}>Teleporter</color> unlock <color=#{YellowColor}>countdown!</color> <color=#{GrayColor}>({unlockTime} seconds left)</color>";
            RoR2.Chat.SendBroadcastChat(new RoR2.Chat.SimpleChatMessage { baseToken = message });
        }

        public static void BossDefeated()
        {
            var message = $"Boss was defeated. Starting new vote!";
            RoR2.Chat.SendBroadcastChat(new RoR2.Chat.SimpleChatMessage { baseToken = message });
        }

        public static void TimeWarning(uint minutes)
        {
            var message = $"<color=#{RedColor}>{minutes} minute warning!</color> You should be going for the <color=#{RedColor}>Teleporter</color> now!";
            RoR2.Chat.SendBroadcastChat(new RoR2.Chat.SimpleChatMessage { baseToken = message });
        }
    }
}
