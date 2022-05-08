using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
 
    public GameObject Grid;
    public GameObject Sphere;
    Vector3 pos;

    //Spawn the obstacle in the Grid.
    public void SpawnObstacle()
    {
        //Get The number of rows and coloums present in the Grid attached.
        int rows = Grid.transform.childCount;
        int col = Grid.transform.GetChild(0).childCount;

        //for every cube/object in the Grid.
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < col; j++)
            {
                //Take the cube's one by one 
                //Check whether the Cube is toggled to be a obstacle
                //if yes then Instatiate that object as a child of that cube.
                var current = Grid.transform.GetChild(i).GetChild(j);
                if (current.GetComponent<Cube>().isObst)
                {
                    pos = new Vector3(current.position.x, current.position.y + 0.5f, current.position.z);
                    GameObject obj = Instantiate(Sphere, pos , Quaternion.identity) as GameObject;
                    obj.transform.parent = current.transform;
                }
            }
        }
    }
}
