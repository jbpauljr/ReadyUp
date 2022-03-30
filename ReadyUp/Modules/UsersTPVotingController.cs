using UnityEngine.Networking;
using RoR2;
using System.Linq;
using System.Collections;
using UnityEngine;

namespace Valter
{
    public class UsersTPVotingController : NetworkBehaviour
    {
        private UsersVoting usersTPVoting = new UsersVoting();
        private IEnumerator majorityTPVotingTimer;
        private Coroutine levelTimer = null;
        private bool majorityTPVotingRunning;

        public delegate void TPVotingRestart();
        public event TPVotingRestart OnTPVotingStarted;

        public delegate void TPVotingEnd();
        public event TPVotingEnd OnTPVotingEnded;

        public void Awake()
        {
            usersTPVoting.OnVotingStarted += UsersTPVoting_OnVotingStarted;
            usersTPVoting.OnVotingEnded += UsersTPVoting_OnVotingEnded;
            usersTPVoting.OnUserVoted += UsersTPVoting_OnUserVoted;
            usersTPVoting.OnUserRemoved += UsersTPVoting_OnUserRemoved;
            usersTPVoting.OnVotingUpdate += UsersTPVoting_OnVotingUpdate;

            On.RoR2.Run.OnServerSceneChanged += Run_OnServerSceneChanged;
            On.RoR2.CharacterMaster.OnBodyDeath += CharacterMaster_OnBodyDeath;
            On.RoR2.Run.OnUserRemoved += Run_OnUserRemoved;

            On.RoR2.Chat.SendBroadcastChat_ChatMessageBase += Chat_SendBroadcastChat_ChatMessageBase;
        }

        public void OnDestroy()
        {
            usersTPVoting.OnVotingStarted -= UsersTPVoting_OnVotingStarted;
            usersTPVoting.OnVotingEnded -= UsersTPVoting_OnVotingEnded;
            usersTPVoting.OnUserVoted -= UsersTPVoting_OnUserVoted;
            usersTPVoting.OnUserRemoved -= UsersTPVoting_OnUserRemoved;
            usersTPVoting.OnVotingUpdate -= UsersTPVoting_OnVotingUpdate;

            On.RoR2.Run.OnServerSceneChanged -= Run_OnServerSceneChanged;
            On.RoR2.CharacterMaster.OnBodyDeath -= CharacterMaster_OnBodyDeath;
            On.RoR2.Run.OnUserRemoved -= Run_OnUserRemoved;

            On.RoR2.Chat.SendBroadcastChat_ChatMessageBase -= Chat_SendBroadcastChat_ChatMessageBase;
        }

        public bool HasUserVoted(NetworkUser user)
        {
            return user && usersTPVoting.CheckIfUserVoted(user);
        }

        public bool Vote(NetworkUser user)
        {
            return user && usersTPVoting.Vote(user);
        }

        public void RestartVoting(bool countDead)
        {
            if (majorityTPVotingRunning)
                StopCoroutine(majorityTPVotingTimer);
            majorityTPVotingRunning = false;
            usersTPVoting.AbandonVoting();

            if (UsersHelper.IsOneUserOnly() || !CheckIfCurrentStageQualifyForTPVoting())
                return;

            if (countDead)
            {
                usersTPVoting.StartVoting(NetworkUser.readOnlyInstancesList);
            }
            else
            {
                usersTPVoting.StartVoting(NetworkUser.readOnlyInstancesList.Where(user =>
                    !user.master.IsDeadAndOutOfLivesServer()).ToList());
            }
        }

        private void UsersTPVoting_OnVotingStarted()
        {
            OnTPVotingStarted?.Invoke();
        }

        private void UsersTPVoting_OnVotingEnded()
        {
            OnTPVotingEnded?.Invoke();
        }

        private void UsersTPVoting_OnUserVoted(NetworkUser user)
        {
            var usersVotes = usersTPVoting.UsersVotes;
            ChatHelper.UserIsReady(user.userName, usersVotes.Count(ks => ks.Value == true), usersVotes.Count);
        }

        private void UsersTPVoting_OnUserRemoved()
        {

        }

        private void UsersTPVoting_OnVotingUpdate()
        {
            if (!usersTPVoting.IsVotingStarted)
                return;

            if (usersTPVoting.CheckIfAllUsersVoted() || usersTPVoting.CheckIfOnlyOneUserLeft())
            {
                usersTPVoting.EndVoting();
            }
            else if (usersTPVoting.CheckIfHalfOrMoreVoted())
            {
                if (!majorityTPVotingRunning)
                {
                    var unlockTime = PluginGlobals.CurrentCountdownTime;

                    ChatHelper.TPCountdown(unlockTime);
                    StartCoroutine(majorityTPVotingTimer = WaitAndEndVoting());

                    IEnumerator WaitAndEndVoting()
                    {
                        majorityTPVotingRunning = true;
                        yield return new UnityEngine.WaitForSeconds(unlockTime);
                        usersTPVoting.EndVoting();
                        majorityTPVotingRunning = false;
                    }
                }
            }
        }

        private void Run_OnServerSceneChanged(On.RoR2.Run.orig_OnServerSceneChanged orig, Run self, string sceneName)
        {
            orig(self, sceneName);

            PluginGlobals.CurrentCountdownTime = PluginConfig.MajorityVotesCountdownTimeBoss.Value;
            RestartVoting(true);

            if (levelTimer != null)
            {
                StopCoroutine(levelTimer);
            }
            levelTimer = StartCoroutine(WaitAndEndVoting());

            IEnumerator WaitAndEndVoting()
            {
                yield return new UnityEngine.WaitForSeconds(PluginConfig.LevelTimeWarningMinutes.Value * 60);
                ChatHelper.TimeWarning(PluginConfig.LevelTimeWarningMinutes.Value);
            }
        }

        private void CharacterMaster_OnBodyDeath(On.RoR2.CharacterMaster.orig_OnBodyDeath orig, CharacterMaster self, CharacterBody body)
        {
            orig(self, body);

            if (!usersTPVoting.IsVotingStarted)
            {
                return;
            }

            if (self.IsDeadAndOutOfLivesServer())
            {
                var user = UsersHelper.GetUser(self);

                if (user)
                {
                    usersTPVoting.RemoveVoter(user);
                }
            }
        }

        private void Run_OnUserRemoved(On.RoR2.Run.orig_OnUserRemoved orig, RoR2.Run self, RoR2.NetworkUser user)
        {
            orig(self, user);

            if (!usersTPVoting.IsVotingStarted)
            {
                return;
            }

            usersTPVoting.RemoveVoter(user);
        }

        private void CharacterBody_Update(On.RoR2.CharacterBody.orig_Update orig, CharacterBody self)
        {
            orig(self);
            if (self.hasAuthority && self.isPlayerControlled && self.master
                && !LocalUserManager.readOnlyLocalUsersList[0].isUIFocused
                && Input.GetKeyDown(KeyCode.Y))
        
            if (!usersTPVoting.IsVotingStarted)
            {
                return;
            }
                    
                        if (usersTPVoting.Vote(user))
                            return;
                    
                

        }

        private bool CheckIfReadyMessage(string message)
        {
            return PluginConfig.PlayerIsReadyMessages.Value.Split(',').Contains(message);
        }

        private bool CheckIfCurrentStageQualifyForTPVoting()
        {
            return !PluginGlobals.IgnoredStages.Contains(SceneCatalog.GetSceneDefForCurrentScene().baseSceneName);
        }
    }
}
