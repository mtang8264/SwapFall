using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum Col {  RED, ORANGE, GREEN, BLUE, PURPLE, YELLOW, POINT, NONE };
    public Col color;
    public Vector2 startPos;
    public Vector2 goalPos;
    public float moveTimer;
    public float moveTime;

    // Private values
    SpriteRenderer rend;
    ColorRef cr;
    float defaultMoveTime = 0.5f;

    void Awake()
    {
        startPos = (Vector2)transform.position;
        rend = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (cr == null)
            cr = ColorRef.me;

        UpdateColor();
        if(moveTime > 0)
        {
            Move();
        }
    }

    public void MoveTo(Vector2 v)
    {
        moveTime = defaultMoveTime;
        goalPos = v;
    }

    void Move()
    {
        moveTimer += Time.deltaTime;
        transform.position = Vector2.Lerp(startPos, goalPos, moveTimer/moveTime);
        if (moveTimer > moveTime)
        {
            moveTimer = 0;
            moveTime = -1f;
        }
    }

    void UpdateColor()
    {
        switch(color)
        {
            case Col.RED:
                rend.color = cr.red;
                break;
            case Col.ORANGE:
                rend.color = cr.orange;
                break;
            case Col.GREEN:
                rend.color = cr.green;
                break;
            case Col.BLUE:
                rend.color = cr.blue;
                break;
            case Col.PURPLE:
                rend.color = cr.purple;
                break;
            case Col.YELLOW:
                rend.color = cr.yellow;
                break;
            case Col.NONE:
                rend.color = new Color(0, 0, 0, 0);
                break;
            case Col.POINT:
                rend.color = new Color(0, 0, 0, 0);
                break;
        }
    }
    public void RandomizeColor()
    {
        if (color == Col.POINT)
            return;
        int sel = Random.Range(0, 6);
        if(sel == 0)
            color = Col.RED;
        if (sel == 1)
            color = Col.ORANGE;
        if (sel == 2)
            color = Col.PURPLE;
        if (sel == 3)
            color = Col.YELLOW;
        if (sel == 4)
            color = Col.GREEN;
        if (sel == 5)
            color = Col.BLUE;
    }
}
