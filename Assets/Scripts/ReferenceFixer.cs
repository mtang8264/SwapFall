using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceFixer : MonoBehaviour
{
    public Column fix;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fix == null)
        {
            Destroy(this);
        }
        //fix.cells[6] = gameObject.GetComponent<Cell>();
        Destroy(this);
    }
}
