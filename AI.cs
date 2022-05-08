using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public PathfinderAlgo pathfinder;
    public PlayerScript player;
    public bool isStartExplored = false;
    public Vector2Int[] Neighbours = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    public Dictionary<Vector2Int, Cube> Grid = new Dictionary<Vector2Int, Cube>();
    public float movementspeed = 0.5f;

    //Assing the starting point Automatically, rather than adding manually at first.
    public void AssignStartingPoint(Collision collision)
    {
        //if starting point is not yet assigned,
        //and currently standing on Cube, then add as Stari=ting point.
        if (!isStartExplored && collision.gameObject.tag == "Cube")
        {
            var cube = collision.gameObject.GetComponent<Cube>();
            pathfinder.Startingpoint = cube;
            isStartExplored = true;
        }
    }
}
