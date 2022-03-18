# ReadyUp

Risk of Rain 2 mod - provides a "ready up" system for the teleporter.

### How it works

Mod will lock players from starting teleporter (or portals) until voting is passed. To prevent stalling, if half or majority players voted, countdown will begin after which teleporter will be unlocked. Players can vote by writing "r" in chat or by interacting with a portal or teleporter for the first time. Adequate communication in the game is already made for the player to understand how voting works.

**This mod was designed for my own friend group. I'm happy if it works for you too, but I don't plan on putting much effort into it.**

### Default Config Settings

| Setting                        | Default Value |
| :----------------------------- | :-----------: |
| PlayerIsReadyMessages          | "r,rdy,ready" |
| MajorityVotesCountdownTimeBoss |      10       |
| MajorityVotesCountdownTime     |      60       |
| LevelTimeWarningMinutes        |       4       |

### Changelog

#### 1.0.0

- Initial release, forked from [TPVoting by Mordrog](https://github.com/Mordrog/RoR2-TPVoting)
- Rebuilt the mod so that it works with the newest version of the game (1.2.2.0)
- Added an additional "ready" vote for progressing to the next level, after the boss fight
  - This has its own separate majority timer
- Fixed some spelling/grammar
- Changed dead players to not count toward voting anymore
- Added a simple timer that warns players when they've been in a level for so many minutes
