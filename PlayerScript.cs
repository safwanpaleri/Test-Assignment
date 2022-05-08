using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public PathfinderAlgo pathfinder;
    public bool isStartExplored = false;
    public Cube cube;
    public bool isMoving = false;
    float Speed = 0.5f;
    public bool isInput = true;
    Raycast rays;
    RaycastHit hit;
    Vector3 newpos;
    public bool sendPos = false;

    private void Awake()
    {
        pathfinder = GetComponent<PathfinderAlgo>();
        rays = FindObjectOfType<Raycast>();
    }

    void Update()
    {
        //If the Mouse right button is pressed and Input is allowed
        if (Input.GetMouseButtonDown(0) && isInput)
        {
            //Get the Raycast Hit object from the Raycast Script.
            hit = rays.SendHit();

            //If the Raycast hit object is not obstacle
            if (hit.transform.tag != "Obstacle")
            {
                //if the Raycast hit is a Grid Cube and it does not contain obstacle.
                //Making double sure the grid cube does not contain obstacle.
                if (hit.transform.GetComponent<Cube>() && !hit.transform.GetComponent<Cube>().isObst)
                {
                    //Find the Shortest Path towards the Selected Grid Cube.
                    pathfinder.ResetPath();
                    pathfinder.Endpoint = hit.transform.GetComponent<Cube>();
                    pathfinder.GetPath();

                    //A bool for indicating the player started to move.
                    isMoving = true;
                }
            }
        }

        //If the player started movement and the shortest path has been found
        if(isMoving && pathfinder.isSuccess)
        {

            //bool for indicating restricting Input while the player is moving
            isInput = false;
            isMoving = false;

            //Calling Coroutine to Move the Player to the Endpoint or the Grid Cube Pressed.
            StartCoroutine(MovetoEnd());
        }
        //if The path cannot be found.
        //it means the player got trapped by the Enemy.
        //Sending UI confirmation to the Script which contains UI information.
        else if(!pathfinder.isSuccess)
            rays.isPlayerTrapped = true;
    }

    //Coroutine Function to Move towards Endpoint ( The Grid Cube pressed).
    IEnumerator MovetoEnd()
    {
        //The cube Which the player is standing is considered as the cube with obstacle,
        //to avoid the overlapping between enemy and other pathfinding object.
        //So when the player moves that cube is changed into normal cube.
        pathfinder.Startingpoint.isObst = false;

        //The path is saved in a List,
        foreach(Cube cube in pathfinder.path)
        {
            //Assigning new to Location to move untill the list is empty , i.e after reaching Endpoint.
            //In a equal interval time or Speed of the player.
            newpos = new Vector3(cube.transform.position.x, transform.position.y, cube.transform.position.z);
            transform.position = newpos;
            pathfinder.Startingpoint = cube;
            yield return new WaitForSeconds(Speed);
        }

        //After reaching the Endpoint ( The Grid Cube Pressed ).
        //Input is re-established.
        //The current cube is considered as the cube with obstacle.
        //Path is resetted.
        isInput = true;
        sendPos = true;
        pathfinder.Startingpoint.isObst = true;
        pathfinder.ResetPath();
    }


    //The First Starting point of the player is Detected automatically,
    //rather than assigning it manually.
    private void OnCollisionEnter(Collision collision)
    {
        //If the first cube not detected yet and the currently player is standing on cube
        if (!isStartExplored && collision.gameObject.tag == "Cube")
        {
            cube = collision.gameObject.GetComponent<Cube>();
            pathfinder.Startingpoint = collision.gameObject.GetComponent<Cube>();
            pathfinder.Startingpoint.exploredFrom = pathfinder.Startingpoint;
            isStartExplored = true;
        }
    }

    //The function returns the current position of the Player.
    public Cube SendPos()
    {
        return pathfinder.Startingpoint;
    }

}
