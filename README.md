# Project _Blood Cells NPC_

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Jackson Heim
-   Section: 02

## Simulation Design

A bunch of different blood cells cells working and moving around in a petri dish. White blood cells attack the foreign objects (which in this case are the pollen cells), and red blood cells move around at random.

### Controls
    
-    User can click the screen to add new cells to the pretri dish
       - Clicking the right mouse button adds pollen cells that the white blood cells search out and attack.
       - Clicking the left mouse button adds more red blood cells that just move around randomly.
     
## _Agent 1:Red blood cell_

A red blood cell that floats around randomly and avoids obstacles.

#### Steering Behaviors

- Wander: wanders around aimlessly using wander
- Stay in bounds: Stays inside the petri dish
   - If it gets too close to the edge, it will move back to the center.
   - If this does not happen fast enough, it will bounce off the edge.

- Obstacles - Avoids blood clots with obstacle avoidance
- Seperation - Seperates from all other agents using speration

## _Agent 2: White blood cell_

Cells that wander around by themselves until they come acorss a foreign object to seek out and kill.

### _Wandering_

**Objective:** Wanders around the petri dish aimlessly.

#### Steering Behaviors

- Wander: Wanders around aimlessly using wander
- Stay in bounds: Stays inside the petri dish
   - If it gets too close to the edge, it will move back to the center.
   - If this does not happen fast enough, it will bounce off the edge.

- Obstacles - Avoids blood clots with obstacle avoidance
- Seperation - Seperates from all other agents using speration
   
#### State Transistions

  - When there are no pollen cells, it will wander.
   
### _Seeking_

**Objective:** Seeks out and kills the pollen cells it sees.

#### Steering Behaviors

- Seek: Chases the pollen cells
   - Seeks nearest pollen cell to its current loaction
   - Once caught it kills it
- Stay in bounds: Stays inside the petri dish
   - If it gets too close to the edge, it will move back to the center.
   - If this does not happen fast enough, it will bounce off the edge.

- Obstacles - Avoids blood clots with obstacle avoidance
- Seperation - Seperates from all other agents using speration
   
#### State Transistions

If there are any pollen cells spawned in, it will seek them out and chase them.

## _Agent 3: Pollen cell_

Foreign objects that wander around the pretri dish and flee from the white blood cells.

### _Wandering_

**Objective:** Wanders around the petri dish aimlessly.

#### Steering Behaviors

- Wander: Wanders around aimlessly using wander
- Stay in bounds: Stays inside the petri dish
   - If it gets too close to the edge, it will move back to the center.
   - If this does not happen fast enough, it will bounce off the edge.

- Obstacles - Avoids blood clots with obstacle avoidance
- Seperation - Seperates from all other agents using speration
   
#### State Transistions

  - When it is far away from all whote blood cells, it just wanders.
   
### _Fleeing_

**Objective:** Runs away from the white blood cells and tries to survive.

#### Steering Behaviors

- Flee: Runs away from white blood cells
   - Flees from the nearest white blood cell
   - Once caught it gets killed
- Stay in bounds: Stays inside the petri dish
   - If it gets too close to the edge, it will move back to the center.
   - If this does not happen fast enough, it will bounce off the edge.

- Obstacles - Avoids blood clots with obstacle avoidance
- Seperation - Seperates from all other agents using speration
   
#### State Transistions

When it is within a certain range of a white blood cell, it flees from the cell.

## Sources

-   https://www.piskelapp.com/ : used to make all the assets

## Make it Your Own

-  I drew all my own assets for the cells and blood clots and animate the white blood cell.
-  Also added states to the white blood cell and pollen cell.

## Known Issues

Sometimes the cells get stuck on the blood clots and just spin around. This happens to the white blood cells a lot. The pollen cells also sometimes go right through the obstacle when they are running from the white blood cells.

### Requirements not completed

I think I did them all. At least I hope so.

