using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObstacleGenrator : EditorWindow
{
    GameObject Grid;
    ObstacleManager obstacle;
    Vector2 Scrollpos;
    bool isadd = false;
    bool isexpand = false;
    bool istoggle = false;
    int rows;
    int col;

    [MenuItem("Tools/Obstacle Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ObstacleGenrator));
    }

    private void OnGUI()
    {
        //Field for adding ObstacleManager.
        obstacle = EditorGUILayout.ObjectField("Obstacle", obstacle, typeof(ObstacleManager), true) as ObstacleManager;

        //if AddObstacle button is Pressed,
        //Show options to add Grid and Obstacle.
        if(GUILayout.Button("Add obstacle"))
            isadd = true;
        if(isadd)
            AddGridAndObstacle();
        
    }

    //Function to Show Options to add Grid and Obstacle
    private void AddGridAndObstacle()
    {
        //Field for selecting the gird where the obstacles have to be added.
        obstacle.Grid = EditorGUILayout.ObjectField("Grid", obstacle.Grid, typeof(GameObject), true) as GameObject;

        //Field for selecting the obstacle to be added in the selected Grid.
        obstacle.Sphere = EditorGUILayout.ObjectField("Obstacle", obstacle.Sphere, typeof(GameObject), true) as GameObject;

        //if the button Expand is pressed,
        //Show the details of the Grid Selected.
        if (GUILayout.Button("Expand"))
            isexpand = true;
        if (isexpand)
            ExapanGrid();
    }

    //Function to Show the details of the grid selected.
    private void ExapanGrid()
    {
        //Get the number of Rows and Coloums of the Grid Selected.
        //Saved in a varible because have to use multiple times.
        rows = obstacle.Grid.transform.childCount;
        col = obstacle.Grid.transform.GetChild(0).childCount;

        //Show the number of Rows and Columns in the Grid Selected in an Integer Field.
        rows = EditorGUILayout.IntField("Rows", rows);
        col = EditorGUILayout.IntField("Columns", col);

        //Press the button to view toggling View of the Grid to add obstacle.
        if (GUILayout.Button("Add Obstacle"))
            istoggle = true;
        if (istoggle)
            TogglingView();
    }

    //Funtion to view the toggling view of the Grid.
    private void TogglingView()
    {
        //Scrolling option so that all the Grid Cube's can be seen and modified in a Better view.
        Scrollpos = EditorGUILayout.BeginScrollView(Scrollpos, false, true);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < col; j++)
            {
                //Creating a toggling Editor UI for every Cube present in the Grid.
                obstacle.Grid.transform.GetChild(i).GetChild(j).GetComponent<Cube>().isObst =
                    EditorGUILayout.Toggle(obstacle.Grid.transform.GetChild(i).GetChild(j).GetComponent<Cube>().name,
                                            obstacle.Grid.transform.GetChild(i).GetChild(j).GetComponent<Cube>().isObst);
            }
        }

        //If the button is pressed Spawn Obstacles in the Toggled Grid Cubes.
        //Using the ObstacleManger attached at first.
        if (GUILayout.Button("Done"))
            obstacle.SpawnObstacle();

        EditorGUILayout.EndScrollView();
    }
}
