https://user-images.githubusercontent.com/80332947/143651304-18eaa9c3-f69d-485a-86fa-38497c0ada2d.mp4

# About

Arkanoid clone made in Unity. Features random level generation, online scoreboard and powerup system. Can be played on PC and Android.

# Links

[Itch.io (Playable in browser)](https://ys95.itch.io/randomarkanoid)

[Scripts folder](https://github.com/Ys95/RandomArkanoid/tree/main/Assets/Scripts)

# Game features

## Random level generation

Levels are generated based on difficulty - game starts at 1 difficulty and this number rises by 1 everytime player clears level.
Level can contain up to 256 bricks of various types. Bricks spawn in clusters of 8, for every difficulty level 1-2 clusters will spawn.
What kind of brick will spawn is also based on difficulty - every brick type has a base spawn rate that rises by defined amount until it reaches its max spawn rate.

![Screenshot_46](https://user-images.githubusercontent.com/80332947/143654898-ed7a6808-75db-442a-9e64-c1a67e4fa587.png)


### Important classes

* [BricksPositionRandomizer](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/LevelGeneration/BricksPositionsRandomizer.cs) - Manages level layout and positions in which bricks are allowed to spawn.
* [BricksTypeRandomizer](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/LevelGeneration/BrickTypeRandomizer.cs) - Decides type of every brick that spawns.
* [LevelBuilder](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/LevelGeneration/LevelBuilder.cs) - Controls whole process of building a new level.


## Online leaderboard

Leaderboards with www.lootlocker.io backend. Players are able to upload their best score and see highest scores of other players.

![Screenshot_41](https://user-images.githubusercontent.com/80332947/143624864-496877da-225e-42ff-8233-62760672a25e.png)

### Important classes

* [OnlineLeaderboardSystem](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/GameSystems/OnlineLeaderboardSystem.cs) - Handles communication with backend - fectching scores, uploading them etc.
* [InLeaderboardMenuState](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/GameStates/States/InLeaderboardMenuState.cs) - Handles displaying leaderboards in UI.


## Game states

Whole game is made in one scene which is controlled by events and state system. 
Every state can display UI, handle input differently and define what happens when state is entered and exited.
Example of events when players clear level:

![image](https://user-images.githubusercontent.com/80332947/143625272-68e67e2b-7fc1-4ee3-9071-b89cac2ec763.png)

### Important classes

* [GameState](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/GameStates/States/GameState.cs) - Abstract class, every game state derives from it.
* [GameStateController](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/GameStates/GameStateController.cs) - Controls game states. 
* [InGameState](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/GameStates/States/InGameState.cs) - Example of state, handles what happens during gameplay - for examples, after destroying all bricks GameManager invokes OnLevelCleared event which switches to different state and displays UI.
* [GameManager](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/GameManager.cs) - Singleton, contains important static events that controlls whole game.

## Powerups

Player can pickup powerups that change how racket or ball behaves (how it handles collision etc). For example, fire ball will detroy not only the brick it hits but also all brick around. Powerups are dropped randomly.

![Screenshot_44](https://user-images.githubusercontent.com/80332947/143655244-a1a90e41-443a-4c9e-9255-19a0a9941ddc.png)

### Important classes

* [BallType](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/Player/BallTypes/BallType.cs) - Defines default behaviour od a ball, all other ball types derive from it and can override certain behaviours.
* [FireBallType](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/Player/BallTypes/FireBallType.cs) - Defines behaviour of a fire ball.
* [BallModel](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/Player/BallTypes/Models/BallModel.cs) - Handles ball gameobject - how it looks, what sounds and particles it can play etc. Every ball type has ball model.
* [PickupDropper](https://github.com/Ys95/RandomArkanoid/blob/main/Assets/Scripts/Pickups/PickupDropper.cs) - Handles dropping powerups(chances for drop etc).


Powerups examples:

https://user-images.githubusercontent.com/80332947/143627979-5418d263-6696-41d4-a611-3cc7810ae456.mp4


https://user-images.githubusercontent.com/80332947/143630131-f8ea8c85-13f3-4264-b08c-26e6437b3bf8.mp4









