using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : AI
{
    List<List<Cube>> Paths = new List<List<Cube>>();
    bool noMove = false;
    List<Cube> sPath = new List<Cube>();
    List<Cube> templist = new List<Cube>();

   
    void Start()
    {
        pathfinder = GetComponent<PathfinderAlgo>();
        player = FindObjectOfType<PlayerScript>();
        Grid = player.GetComponent<PathfinderAlgo>().Grid;
    }

    void Update()
    {
        //If player reached Endpoint, it will Senf an Signal.
        if(player.sendPos)
        {
            noMove = false;
            sPath.Clear();

            Vector2Int enemyloc = new Vector2Int(Mathf.RoundToInt(transform.position.x),
                                                 Mathf.RoundToInt(transform.position.z));
            player.sendPos = false;

            //For ever neighbour near to Player, look for the shortest path.
            foreach (Vector2Int neighbour in Neighbours)
            {
                var temp = player.SendPos();
                var neigh = temp.GetGridPos() + neighbour;
                
                //if player is standing in one of enemy's nieghbour
                //then do look for the path.
                if (enemyloc == neigh)
                    noMove = true;

                //Get path for every neighboring cubes to player position.
                if (Grid.ContainsKey(neigh) && !Grid[neigh].isObst)
                {
                    pathfinder.ResetPath();
                    pathfinder.Endpoint = Grid[neigh];
                    pathfinder.GetPath();
                    templist = new List<Cube>(pathfinder.SendPath());

                    //if path is found successfully then, add it to list of paths.
                    if (templist.Count > 0 && pathfinder.isSuccess)
                        Paths.Add(templist);
                }
                
            }

            //for every paths in the list of path
            //take the shortest one.
            foreach (List<Cube> path in Paths)
            {
                if (sPath.Count == 0)
                    sPath = path;
                else if (sPath.Count > path.Count)
                    sPath = path;
            }

            //if path has been found and it is not in neighbouring to enemy then move towards player.
            //else don't move and reset the values into inital ones.
            if (sPath.Count != 0 && !noMove)
                StartCoroutine(MovetoPlayer());
            else if (noMove)
            {
                FindObjectOfType<Raycast>().isPlayerNear = true;
                pathfinder.ResetPath();
                pathfinder.Startingpoint = Grid[enemyloc];
                sPath.Clear();
                Paths.Clear();
                noMove = false;
            }
            //Gives UI confirmation if the Enemy got trapped by the player.
            else
                FindObjectOfType<Raycast>().isEnemyTrapped = true;
        }
    }

    //Coroutine funtion to move towards player.
    IEnumerator MovetoPlayer()
    {
        //The Cube where the enemy standind is considered as obstacle
        //for avoiding overlapping between player.
        pathfinder.Startingpoint.isObst = false;

        //Taking the path and moving towards the player with regular interval of time/speed.
        foreach (Cube cube in sPath)
        {
            Vector3 moveto = new Vector3(cube.transform.position.x, transform.position.y, cube.transform.position.z);
            transform.position = moveto;
            pathfinder.Startingpoint = cube;
            yield return new WaitForSeconds(movementspeed);
        }

        //After reaching endpoint, Considering the cube as obstacle,
        //and reseting the path.
        pathfinder.Startingpoint.isObst = true;
        pathfinder.ResetPath();
        Paths.Clear();
    }

    //Function for automatically adding starting point at start of the game.
    private void OnCollisionEnter(Collision collision)
    {
        AssignStartingPoint(collision);
    }

}
