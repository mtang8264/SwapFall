using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    Cell[] cells;
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

    public void RandomizeColumn()
    {
        for (int i = 0; i < height; i++)
        {
            cells[i].RandomizeColor();
        }
    }
}
