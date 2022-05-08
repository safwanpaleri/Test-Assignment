using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Obstacles : ScriptableObject
{
    //obstacle data
    public GameObject Grid;
    public int id = 1;
    public bool isobst = false;
    public Vector3 location = new Vector3(0.0f, 0.0f, 0.0f);

}
