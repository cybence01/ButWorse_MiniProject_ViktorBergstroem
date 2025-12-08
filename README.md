# Game overview
This is the mini project for the course PI3DW (Programming Interactive 3D Worlds). We were tasked with recreating a game or game mechanic that we like. After some thought I settled on the Zombie mode found in various Call Of Duty games.
In this mode the player has to survive as many rounds as they can while being chased and attacked by zombies. As the player advances in rounds the amount of zombies are increased. 
With mouse and keyboard controls the player moves around the map, avoiding and killing zombies. 

## Main parts of the game
- Player - The player can use the WASD keys to move around, jump on SPACEBAR and sprint on SHIFT. The left mouse click is used to shoot.
- Zombies - Zombies are spawned each round. They chase the player and attack once they get close - dealing 20 damage. By using a NavMesh Agent they chase the player in a satisfying way.
- Map - The map is heavily inspired, and designed after, the original zombie map: Nacht Der Untoten. A plane acts as the floor while cubes with various scale transformations make up the walls.
- Health - The player has 100 health points. Once the player reaches 0 health they get sent back to the main menu screen. After being hit a cooldown is starting. After the cooldown the player slowly regains health.
- Rounds - The rounds act both as the goal of the game, as the player aims for the highest round possible, but also as a difficulty indicator. As the rounds increases so does the amount of zombies. This is done with this formula: 6*(1.15)^x. 6 being the base amount of zombies to spawn in. 1.15 being the scaling factor. x being the current number of rounds - 1 (because we don't want to apply the scale on the first round)

- ## Game concept
