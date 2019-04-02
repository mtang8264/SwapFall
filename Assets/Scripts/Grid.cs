using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    public Column[] grid;
    public int width;
    public float offset;
    public GameObject columnPrefab;
    public int movesLeft;
    public KeyCode[] left;
    public KeyCode[] up;
    public KeyCode[] right;
    public KeyCode[] down;
    public TextMeshPro counter;
    public TextMeshPro scoreText;
    public bool matchLock = false;
    public bool falling = false;
    private bool fallFlag = false;
    public int score;

    // Start is called before the first frame update
    void Awake()
    {
        movesLeft = 6;
        GenerateGrid();
        RandomizeGrid();
        while(CheckForMatch().x != -1)
        {
            Debug.Log("Match found at beginning");
            Match temp = CheckForMatch();
            grid[temp.x].cells[temp.y].RandomizeColor();
        }
        PlacePoint();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + score;

        SetCellPoints();

        if(movesLeft > 0 && !matchLock)
           CheckKeys();
        counter.text = "" + movesLeft;

        if (falling && !fallFlag)
        {
            if (AreThereGaps())
            {
                Debug.Log("Gotta fall cause there are gaps and the last fall is over");
                fallFlag = true;
                for (int x = 0; x < width; x++)
                {
                    grid[x].Fall();
                }
            }
            else
            {
                Debug.Log("This would be the else");
                falling = false;
                matchLock = false;
            }
        }
        if (CheckForMatch().x != -1 && !matchLock)
        {
            matchLock = true;
            Debug.Log("MATCH");
            DeleteMatches(CheckForMatch());
            falling = true;
        }
    }

    void SetCellPoints()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < grid[x].height; y++)
            {
                if (grid[x].cells[y] == null)
                    continue;
                grid[x].cells[y].x = x;
                grid[x].cells[y].y = y;
            }
        }
    }

    void DeleteMatches(Match match)
    {
        int x = match.x;
        int y = match.y;
        switch(match.d)
        {
            case Match.Direction.HOR:
                Destroy(grid[x].cells[y].gameObject);
                Destroy(grid[x + 1].cells[y].gameObject);
                Destroy(grid[x + 2].cells[y].gameObject);
                break;
            case Match.Direction.VER:
                Destroy(grid[x].cells[y].gameObject);
                Destroy(grid[x].cells[y+1].gameObject);
                Destroy(grid[x].cells[y+2].gameObject);
                break;
        }
        score += 3;
        movesLeft = 6;
    }

    bool AreThereGaps()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < grid[x].height; y++)
            {
                if (grid[x].cells[y] == null)
                {
                    Debug.Log("Gap Found In Grid at (" + x + ", " + y + ")");
                    return true;
                }
            }
        }
        return false;
    }

    void CheckKeys()
    {
        for (int i = 0; i < left.Length; i++)
        {
            if (Input.GetKeyDown(left[i]))
                PointLeft();
            if (Input.GetKeyDown(right[i]))
                PointRight();
            if (Input.GetKeyDown(up[i]))
                PointUp();
            if (Input.GetKeyDown(down[i]))
                PointDown();
        }
    }
    void PointLeft()
    {
        int x = (int)FindPoint().x;
        int y = (int)FindPoint().y;
        if (!InGrid(x-1, y))
            return;
        grid[x].cells[y].color = grid[x - 1].cells[y].color;
        grid[x - 1].cells[y].color = Cell.Col.POINT;
        movesLeft--;
    }
    void PointRight()
    {
        int x = (int)FindPoint().x;
        int y = (int)FindPoint().y;
        if (!InGrid(x + 1, y))
            return;
        grid[x].cells[y].color = grid[x + 1].cells[y].color;
        grid[x + 1].cells[y].color = Cell.Col.POINT;
        movesLeft--;
    }
    void PointUp()
    {
        int x = (int)FindPoint().x;
        int y = (int)FindPoint().y;
        if (!InGrid(x, y + 1))
            return;
        grid[x].cells[y].color = grid[x].cells[y + 1].color;
        grid[x].cells[y + 1].color = Cell.Col.POINT;
        movesLeft--;
    }
    void PointDown()
    {
        int x = (int)FindPoint().x;
        int y = (int)FindPoint().y;
        if (!InGrid(x, y - 1))
            return;
        grid[x].cells[y].color = grid[x].cells[y - 1].color;
        grid[x].cells[y - 1].color = Cell.Col.POINT;
        movesLeft--;
    }
    Vector2 FindPoint()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < grid[x].height; y++)
            {
                if (grid[x].cells[y].color == Cell.Col.POINT)
                    return new Vector2(x, y);
            }
        }
        return new Vector2(-1, -1);
    }
    void PlacePoint()
    {
        grid[width / 2].cells[grid[0].height / 2].color = Cell.Col.POINT;
    }
    Match CheckForMatch()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < grid[x].height; y++)
            {
                if (grid[x].cells[y] == null)
                    continue;
                if (grid[x].cells[y].color == Cell.Col.NONE || grid[x].cells[y].color == Cell.Col.POINT)
                    continue;
                Cell.Col col = grid[x].cells[y].color;
                if (InGrid(x + 1, y) && InGrid(x + 2, y))
                {
                    if (grid[x + 1].cells[y] == null || grid[x + 2].cells[y] == null)
                        continue;
                    if (grid[x + 1].cells[y].color == col && grid[x + 2].cells[y].color == col)
                    {
                        return new Match(x, y, Match.Direction.HOR);
                    }
                }
                if (InGrid(x, y + 1) && InGrid(x, y + 2))
                {
                    if (grid[x].cells[y + 1] == null || grid[x].cells[y + 2] == null)
                        continue;
                    if (grid[x].cells[y + 1].color == col && grid[x].cells[y+2].color == col)
                    {
                        return new Match(x, y, Match.Direction.VER);
                    }
                }
            }
        }
        return new Match(-1, -1, Match.Direction.HOR);
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
    bool InGrid(int x, int y)
    {
        if(x >= 0 && x < width)
        {
            if (y >= 0 && y < grid[x].height)
                return true;
        }
        return false;
    }

    public void Fallen()
    {
        fallFlag = false;
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

    public string ToString()
    {
        return "Match at (" + x + ", " + y + ")";
    }
}