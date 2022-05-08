using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderAlgo : MonoBehaviour
{
    public Queue<Cube> queue = new Queue<Cube>();
    public Dictionary<Vector2Int, Cube> Grid = new Dictionary<Vector2Int, Cube>();
    public Cube Startingpoint;
    public Cube Endpoint;
    public bool isSuccess = true;
    public List<Cube> path = new List<Cube>();
    bool doSearch = true;
    bool isGrid = false;

    List<Cube> emptylist = new List<Cube>();
    Cube search;
    Cube previous;
    Cube[] Cubes;

    Vector2Int[] Neighbours = { Vector2Int.right, Vector2Int.left , Vector2Int.up , Vector2Int.down };
    Vector2Int npos;
   
    

    private void Awake()
    {
        //Find the all the Grid Cubes and Load it.
        Cubes = FindObjectsOfType<Cube>();
        LoadCubes();
    }

    //The Function that Create Path.
    public void GetPath()
    {
        BreadFirstSearchAlgorithm();
        FindPath();
    }

    //The Function Loaded Cubes.
    private void LoadCubes()
    {
        //if The Grid is not loaded yet.
        if (!isGrid)
        {
            //Foreach Grid cubes present in the Cubes array,
            foreach (Cube cube in Cubes)
            {
                //Get The Vector2Int pos of the current cube.
                //And if it is not present the Grid Dictonary then add.
                var gridpos = cube.GetGridPos();
                if (!Grid.ContainsKey(gridpos))
                    Grid.Add(gridpos, cube);
            }
        }
        isGrid = true;
    }

    //Bread First Search Algorithm to find the shortest path
    //It is back tracking algrothim, explored by adjacent/neighbouring cube.
    private void BreadFirstSearchAlgorithm()
    {
        //Insert the Starting point in the Queue
        //And doSearch untill it is not Endpoint.
        queue.Enqueue(Startingpoint);
        while(queue.Count > 0 && doSearch)
        {
            //the first cube of the queue is used to search neighbours/adjacent cubes.
            //And if the search cube is the endpoint, stop searching.
            search = queue.Dequeue();
            if (search == Endpoint)
                doSearch = false;

            //Explore the neighbours of current search cube.
            //And save the cube as explored.
            ExploreNeighbours(search);
            search.isExplored = true;
        }
    }

    //Function for exploring neighbours.
    private void ExploreNeighbours(Cube search)
    {
        //if endpoint is reached, and don't have to search anymore, then return.
        if (!doSearch) { return; }

        //For all the nieghbours of the cube in Up,Down,Right,Left directions.
        foreach (Vector2Int neighbour in Neighbours)
        {
            //Get the nighbour and Check whether the Neighbouring is present in the Grid.
            npos = search.GetGridPos() + neighbour;
            if (Grid.ContainsKey(npos))
            {
                //if the neighbouring cube is present in the Grid,
                //And the nieghbouring cube is not explored yet, or it is not present in the queue,
                //or it contains obstacle.
                if (Grid[npos].isExplored || queue.Contains(Grid[npos]) || Grid[npos].isObst)
                {
                    //Do nothing
                }
                else
                {
                    //if the conditions are met, then add it into queue, 
                    //save the cube from where this cube was explored.
                    queue.Enqueue(Grid[npos]);
                    Grid[npos].exploredFrom = search;
                    Grid[npos].isPrevious = true;
                }
            }
        }
    }

    //The function for creation of the path
    private void FindPath()
    {
        //Firstly add the endpoint,
        //And check whether a successfull path has been found or not.
        path.Add(Endpoint);
        if (!Endpoint.isPrevious)
            isSuccess = false;
        
        //Save the cube from where this cube was explored into a variable.
        previous = Endpoint.exploredFrom;

        //while that varibale is not the starting point, and if successfull path has been found
        while (previous != Startingpoint && path.Count <= Cubes.Length && isSuccess)
        {
            //checking for null reference.
            if(!previous.isPrevious)
                isSuccess = false;

            //Add the variable into list.
            //Change the variable into the cube from which the variable cube was explored from.
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        //After adding all the paths, add Starting point.
        //And reverse the path.
        path.Add(Startingpoint);
        path.Reverse();
    }

    //Function for resetting values into default ones.
    public void ResetPath()
    {
        queue.Clear();
        doSearch = true;
        isSuccess = true;
        path.Clear();
        foreach(Cube cube in Cubes)
            cube.resetExploredFrom();
    }

    //The function returns the Path list if a successfull path is found.
    //else it will return an empty list.
    public List<Cube> SendPath()
    {
        if (isSuccess)
            return path;
        else
            return emptylist;
    }
}
