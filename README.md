# Test-Assignment

Assignment 1.
-----------------------------------------------------------------------------------------------------------------------------------------------
For Assignment 1, Have a created a small tool 'Grid Genarator' for creating Grid of desired size and position.
Using this tool, the grid can be created easily, with desired cube or building block of Grid.

The cube contains a script called 'Cube' which contains its information, and also a function to send its Vector2Int position to other Scripts.
The Script also changes the name of the cube according to its postion, for easy identification and better readbility.

A script called 'Raycast' have been created for raycasting from the mouse and showing UI information about the cube currently hovering.
The UI coding has been done in the same script.

Assignment 2.
-----------------------------------------------------------------------------------------------------------------------------------------------
Created a Tool for adding obstacles in the Grid generated or created.
Have to add all the required items, like 'ObstacleManger', 'Grid', and 'Obstacle'
At first it will show number of rows and columns present in the 'Grid' and then show the toggling option for each and every 'cube' present in it.

This tool can be modified to edit a scriptable object, but didn't seem the neccessity of doing it so. And if required can be done it.
But need clear cut instruction of what should it edit.

The ObstacleManger actually spawns the 'Obstacle' in all the toggled on Grid Cubes.

Assignment 3.
-----------------------------------------------------------------------------------------------------------------------------------------------
Created a 'Player' and attached 'PlayerScript'
When a mouse is pressed on cube of Grid, it will find the shortest path to that cube and will move to that cube.
And the input is restricted while the player is moving.
Created a pathfinding algorihtm using Breadth Frist Algorithm which uses backtracking.
It does not overlapp with enemy or obstacle.

Assignment 4.
-----------------------------------------------------------------------------------------------------------------------------------------------
Created an 'Enemy' and attached 'EnemyScript' which is inherited from the script 'AI'
After player reaches it point the enemy will follow the player to its nearest neighbouring cube.
It also uses same pathfinding algorithm, and it will go the nearest neighbour.
If the player moved near to enemy, it doesnot move from the position.

Notes:
-----------------------------------------------------------------------------------------------------------------------------------------------
The AI script further modified to do pathfinding and Player and Enemy scripts can be inherited from it.
Because both uses the same technique.

The UI code is done in 'Raycast' which may confuse, but can be done in an seperate Scripts.

Some assignment has not been done 100%, because needed mure more clear instruction like, what should I change in the scriptable object
or what are the contents inside the scriptable object.

Raycasting can be changed into Mouse on hover(),

the codes are commented for better readibility and understanbility.
