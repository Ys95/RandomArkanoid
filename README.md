
https://user-images.githubusercontent.com/80332947/143636900-633035a8-f48a-4e6b-824a-31ee512bd7e4.mp4


# About

Arkanoid clone made in Unity. Features random level generation and online scoreboard. Can be played on PC and Android.

# Links

[Itch.io (Playable in browser)](https://ys95.itch.io/randomarkanoid)

[Scripts folder](https://github.com/Ys95/RandomArkanoid/tree/main/Assets/Scripts)

# Game features

## Random level generation

Levels are generated based on difficulty - game starts at 1 difficulty and this number rises by 1 everytime player clears level.
Level can contain up to 256 bricks of various types. Bricks spawn in clusters of 8, for every difficulty level 1-2 clusters will spawn.
What kind of brick will spawn is also based on difficulty - every brick type has a base spawn rate that rises by defined amount until it reaches its max spawn rate.

![Screenshot_39](https://user-images.githubusercontent.com/80332947/143623554-cb0465af-4b0c-4de1-a9cf-75d5e0aa7bed.png)

### Important classes

[BricksPositionRandomizer](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/LevelGeneration/BricksPositionsRandomizer.cs) - Manages level layout and positions in which bricks are allowed to spawn.

[BricksTypeRandomizer]() - Decides type of every brick that spawns.

[LevelBuilder]() - Controls whole process of building a new level.


## Online leaderboard

Leaderboards with www.lootlocker.io backend. Players are able to upload their best score and see highest scores of other players.

![Screenshot_41](https://user-images.githubusercontent.com/80332947/143624864-496877da-225e-42ff-8233-62760672a25e.png)

### Important classes

[OnlineLeaderboardSystem]() - Handles communication with backend - fectching scores, uploading them etc.

[InLeaderboardMenuState]() - Handles displaying leaderboards in UI.


## Game states

Whole game is made in one scene which is controlled by events and state system. 
Every state can display UI, handle input differently and define what happens when state is entered and exited.
Example of events when players clear level:

![image](https://user-images.githubusercontent.com/80332947/143625272-68e67e2b-7fc1-4ee3-9071-b89cac2ec763.png)

### Important classes

[GameState]() - Abstract class, every game state derives from it.

[GameStateController]() - Controls game states. 

[InGameState]() - Example of state, handles what happens during gameplay - for examples, after destroying all bricks GameManager invokes OnLevelCleared event which switches to different state and displays UI.

[GameManager]() - Singleton, contains important static events that controlls whole game.

##Powerups

Player can pickup powerups that change how racket or ball behaves (how it handles collision etc). For example, fire ball will detroy not only the brick it hits but also all brick around. Powerups are dropped randomly.

### Important classes

[BallType]() - Defines default behaviour od a ball, all other ball types derive from it and can override certain behaviours.
[FireBallType]() - Defines behaviour of a fire ball.
[BallModel]() - Handles ball gameobject - how it looks, what sounds and partciles it can use etc. Every ball type has ball model.


Powerups examples:

https://user-images.githubusercontent.com/80332947/143627979-5418d263-6696-41d4-a611-3cc7810ae456.mp4


https://user-images.githubusercontent.com/80332947/143630131-f8ea8c85-13f3-4264-b08c-26e6437b3bf8.mp4
















