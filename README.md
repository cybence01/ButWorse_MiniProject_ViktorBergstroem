# Game overview
This is the mini project for the course PI3DW (Programming Interactive 3D Worlds). We were tasked with recreating a game or game mechanic that we like. After some thought I settled on the Zombie mode found in various Call Of Duty games.
In this mode the player has to survive as many rounds as they can while being chased and attacked by zombies. As the player advances in rounds the amount of zombies are increased. 
With mouse and keyboard controls the player moves around the map, avoiding and killing zombies. 

# Main parts of the game
- Player - The player can use the WASD keys to move around, jump on SPACEBAR and sprint on SHIFT. The left mouse click is used to shoot.
- Zombies - Zombies are spawned each round. They chase the player and attack once they get close - dealing 20 damage. By using a NavMesh Agent they chase the player in a satisfying way.
- Map - The map is heavily inspired, and designed after, the original zombie map: Nacht Der Untoten. A plane acts as the floor while cubes with various scale transformations make up the walls.
- Health - The player has 100 health points. Once the player reaches 0 health they get sent back to the main menu screen. After being hit a cooldown is started. After the cooldown the player slowly regains health.
- Rounds - The rounds act both as the goal of the game, as the player aims for the highest round possible, but also as a difficulty indicator. As the rounds increases so does the amount of zombies. This is done with this formula: 6*(1.15)^x. 6 being the base amount of zombies to spawn in. 1.15 being the scaling factor. x being the current number of rounds - 1 (because we don't want to apply the scale on the first round)

# Game features
- The player can move around and shoot the zombies to kill them.
- Zombies are spawned around the map, based on set points.
- After every zombie is killed in the current round, a new round starts.
# Video link


# Running it
1. Download Unity >= 6000.0.37f1
2. Clone or download the project from Github
3. The game requires keyboard and mouse

# Project Parts
## Scripts
- Crosshair - Used for creating the crosshair the player uses to aim
- DDOL (DontDestroyOnLoad) - Used to make the sound ambience play in both scenes
- EnemyBehaviour - Used for enemy movement, attacks, enemy health.
- EnemySpawner - Used to spawn the zomies around the map
- GunController - Used to create the gun mechanic - dealing damage to enemies and creating particle effects at the start and end of the raycasts.
- MainMenu - Used to handle the Main Menu
- PauseMenu - Used to handle the Pause menu if the player presses the ESC key
- PlayerHealth - Holds the player's health. It handles if the player takes damage and handles the cooldown for regaining health
- PlayerMovement - Used to move the character around with the WASD keys and looking around the the mouse
- RoundManager - Handles the round system and calculates the amount of enemies that should be spawned in each round.

## Models and Prefabs
- The zombie prefabs were found on the Unity Asset Store: https://assetstore.unity.com/packages/3d/characters/humanoids/free-shirtless-zombie-276762
- The staircase were found on the Unity Asset Store: https://assetstore.unity.com/packages/3d/props/industrial/modular-industrial-catwalk-kit-free-287064
- Barrel and crate decorations were found on the Unity Asset Store: https://assetstore.unity.com/packages/3d/props/exterior/pbr-barrels-and-crates-34608
- The blood splatter was found on the Unity Asset Store: https://assetstore.unity.com/packages/2d/textures-materials/blood-splatter-decal-package-7518
- Different small decorations were found on the Unity Asset Store: https://assetstore.unity.com/packages/3d/environments/alchemist-house-112442
- The gun was found on the Unity Asset Store: https://assetstore.unity.com/packages/3d/props/guns/stylized-m4-assault-rifle-with-scope-complete-kit-with-gunshot-v-178197
- The picture frames were found on the Unity Asset Store: https://assetstore.unity.com/packages/3d/props/furniture/picture-frames-301169
- The pictures in the frame was made with Google Gemini (AI). The pictures depict fellow students dressed as WW2 generals. This was made with their consent.

## Time table
| Task  | Time Spent |
| ------------- | ------------- |
| Initial Setup  | 0.5 hours  |
| Scripting Object Behaviours  | 6 hours |
| Searching for Models and Prefabs | 2 hours |
| Creating the map | 1.5 hours |
| Designing the map | 3 hours |
| UI Design | 1 Hour |
| Playtesting and bugfixing | 1 hour |
| Final touches | 1 hour |
| Total time spent | 16 hours |

# References
FIRST PERSON MOVEMENT in 10 MINUTES - Unity Tutorial: https://www.youtube.com/watch?v=f473C43s8nE
Shooting with Raycasts - Unity Tutorial: https://www.youtube.com/watch?v=THnivyG0Mvo&t

## AI
- claude.ai: Used for improving scripts
- Google Gemini: USed for improving scripts and manipulating pictures of fellow students. 
