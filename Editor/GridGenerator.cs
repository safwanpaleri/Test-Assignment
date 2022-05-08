using UnityEditor;
using UnityEngine;

public class GridGenerator : EditorWindow
{
    GameObject cube;
    Vector2Int GridSize = new Vector2Int(1, 1);
    Vector3 startingpos = new Vector3(0.0f, 0.0f, 0.0f);
    int Scale = 1;


    [MenuItem("Tools/Grid Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(GridGenerator));
    }

    private void OnGUI()
    {
        GUILayout.Label("Tool to create a Grid");

        //Assign Desired Grid Size, its starting postion, Scale, and Which object to spawn as Grid.
        GridSize = EditorGUILayout.Vector2IntField("Grid Size", GridSize);
        startingpos = EditorGUILayout.Vector3Field("Starting position", startingpos);
        Scale = EditorGUILayout.IntField("Scale", Scale);
        cube = EditorGUILayout.ObjectField("Prefab Cube", cube, typeof(GameObject), false) as GameObject;

        if(GUILayout.Button("Create Grid"))
        {
            CreateGrid();
        }
    }

    private void CreateGrid()
    {
        //New empty gameobject created to assign as Parent Transform.
        GameObject grid = new GameObject("Grid");

        //Creating grid is Nested For loop.
        float temp = startingpos.z;
        for(int i = 1; i <= GridSize.x; i++ )
        {
            //Creating a new gameobject row to assign as Parent Transfrom.
            //Done for Better structure in inspector and finding and easy readbility of Grid.
            string row = "row" + i;
            GameObject newrow = new GameObject(row);
            
            for(int j=1; j <= GridSize.y; j++ )
            {
                var col = Instantiate(cube, startingpos, Quaternion.identity);
                col.transform.parent = newrow.transform;
                startingpos.z += Scale;
            }
            startingpos.x += Scale;
            startingpos.z = temp;
            newrow.transform.parent = grid.transform;
        }
    }
}
