using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public Cell[] cells;
    public int height;
    public GameObject cellPrefab;
    public float offset;

    // Start is called before the first frame update
    void Awake()
    {
        cells = new Cell[height];
        for (int i = 0; i < height; i++)
        {
            GameObject obj = Instantiate(cellPrefab, transform);
            cells[i] = obj.GetComponent<Cell>();
            obj.transform.localPosition = new Vector3(0, i + offset);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fall()
    {
        if (!HasGap())
            return;
        Debug.Log("Fall");
        int bot = -1;
        for (int i = 0; i < height; i++)
        {
            if (cells[i] == null)
            {
                bot = i;
                break;
            }
        }
        int top = height;
        for (int i = bot; i < height; i++)
        {
            if(cells[i] != null)
            {
                top = i;
                break;
            }
        }
        if(top == height)
        {
            GameObject obj = Instantiate(cellPrefab, transform);
            obj.GetComponent<Cell>().RandomizeColor();
            obj.transform.localPosition = new Vector3(0, 4);
            cells[height - 1] = obj.GetComponent<Cell>();
            top = height - 1;
        }
        cells[top].MoveTo(new Vector2(transform.position.x, bot + offset));
        cells[bot] = cells[top];
        if(bot != top)
            cells[top] = null;
    }

    bool HasGap()
    {
        for (int y = 0; y < height; y++)
        {
            if (cells[y] == null)
                return true;
        }
        return false;
    }

    public void RandomizeColumn()
    {
        for (int i = 0; i < height; i++)
        {
            cells[i].RandomizeColor();
        }
    }
}
