using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRef : MonoBehaviour
{
    public static ColorRef me;
    public Color red;
    public Color orange;
    public Color green;
    public Color blue;
    public Color purple;
    public Color yellow;

    void Awake()
    {
        me = this;
    }

    private void Update()
    {
        
    }
}
