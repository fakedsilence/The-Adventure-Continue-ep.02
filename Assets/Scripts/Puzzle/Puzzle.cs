using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public NumberBox boxPrefab;

    public NumberBox[,] boxes = new NumberBox[3, 3];

    public Sprite[] sprites;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        int n = 0;
        for (int y = 2; y >= 0; y--)
        {
            for (int x = 0; x < 3; x++)
            {
                NumberBox box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                box.Init(x, y, n + 1, sprites[n], ClickSwap);
                boxes[x, y] = box;
                n++;
            }
        }
    }

    void ClickSwap(int x, int y)
    {
        int dx = getDx(x, y);
        int dy = getDy(x, y);

        var from = boxes[x, y];
        var target = boxes[x + dx, y + dy];

        boxes[x, y] = target;
        boxes[x + dx, y + dy] = from;

        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);
    }

    int getDx(int x, int y)
    {
        if (x < 2 && boxes[x + 1, y].IsEmpty())
            return 1;
        if (x > 0 && boxes[x - 1, y].IsEmpty())
            return -1;
        return 0;
    }
    int getDy(int x, int y)
    {
        if (y < 2 && boxes[x, y + 1].IsEmpty())
            return 1;
        if (y > 0 && boxes[x, y - 1].IsEmpty())
            return -1;
        return 0;
    }
}
