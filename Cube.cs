using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Cube : MonoBehaviour
{
    Vector2Int gridinfo;
    public Cube exploredFrom;
    public bool isObst = false;
    public bool isExplored = false;
    public bool isPrevious = false;

    //Changes the name of the cube into readable and easily recognisable form.
    void Awake()
    {
        gridinfo = GetGridPos();
        string cube_name = "cube" + gridinfo.x + "." + gridinfo.y;
        gameObject.name = cube_name;
    }

    //Return the current postion of the cube in Vector2Int format
    public Vector2Int GetGridPos()
    {
        return new Vector2Int( Mathf.RoundToInt(transform.position.x),
                               Mathf.RoundToInt(transform.position.z) );
    }

    //Reset the values into default
    public void resetExploredFrom()
    {
        exploredFrom = null;
        isExplored = false;
        isPrevious = false;
    }
}
