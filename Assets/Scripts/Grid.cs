using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Column[] grid;
    public int width;
    public float offset;
    public GameObject columnPrefab;


    // Start is called before the first frame update
    void Awake()
    {
        GenerateGrid();
        RandomizeGrid();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Match CheckForMatch()
    {
        return null;
    }
    void GenerateGrid()
    {
        grid = new Column[width];
        for (int i = 0; i < width; i++)
        {
            GameObject obj = Instantiate(columnPrefab, transform);
            grid[i] = obj.GetComponent<Column>();
            obj.transform.localPosition = new Vector3(i + offset, 0);
        }
    }
    void RandomizeGrid()
    {
        for (int i = 0; i < width; i++)
        {
            grid[i].RandomizeColumn();
        }
    }
}

public struct Match
{
    public int x;
    public int y;
    public enum Direction { HOR, VER };
    public Direction d;
    public Match(int a, int b, Direction c)
    {
        x = a;
        y = b;
        d = c;
    }
}