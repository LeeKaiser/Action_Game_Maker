# HERO SHOOTER MAKER (HSM)

Hero Shooter Maker is a template for a custom hero shooter game creation. (Work in progress)

Hero Shooter refers to a genre of games where there are teams composed of some number of players who pick a playable character, and fight each other to complete an objective.
While the primary purpose of this project is to serve as a starting point for creating hero shooters, this can also function as starting point for any game that has some features of hero shooters.

Examples of existing Hero Shooters includes: OVERWATCH by Blizzard, TEAM FORTRESS 2 by Valve, MARVEL RIVALS by NetEase Games, VALORANT by Riot Games, DEADLOCK by Valve, and more.
You can expect HSM to provide tools to replicate core features of these games.

## Player Focused Feature
- Movement & camera controls (COMPLETE)
  - Use the movement input to make the player move. Move the mouse to control the player camera.

- Player core (hitpoints, damage multiplier, etc.) (COMPLETE)
  - holds player stats in a scriptable object, and updates other scripts based on player's stats, and handle some player events

- Ability array (COMPLETE)
  - player press button to make something happen. has internal cooldown that controls usage

- Passive array 
  - listen for events and act accordingly

- Inventory array 
  - holds items

- Status effect array (nearly complete)
  - handle temporary modifications of a player

- Player Event Manager (COMPLETE)
  - parts of player such as player core, abilities, ability manager, etc all call events through an event object inside of player event manager.
  - This allows game mechanics that operate based on player events to be able to reference this player event manager.

- AI players
  - Primarily for making players who are not controlled by a client. Should also be applicable for making other AI controlled entity such as 

## Match Focused Features
- Multiplayer
  - A match of a hero shooter game needs to be hosted from a server and accessed by clients, who control an individual character.

- Teams (in progress)
  - Team objects hold players that belong to the same team, and handle team wide functions

- Objective/win condition
  - Various template objectives, such as a "payload", "king of the hill", "generic scoreing system", etc.

- Game manager 
  - Manage game wide events, match timer, etc.
