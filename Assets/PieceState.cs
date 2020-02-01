using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceState
{
    public PieceInfo info;
    public int rotation;

    public PieceState(PieceInfo info,int rotation = 0)
    {
        this.info = info;
        this.rotation = rotation;
    }

    public int edge(int index)
    {
        return info.edges[(index + rotation) % 4];
    }

    public bool isSame(PieceState p)
    {
        for (int i = 0; i < 4; i++)
            if (edge(i) != p.edge(i))
                return false;
        return true;
    }

    //flat 0
    //in -1
    //out 1
    public bool isMatch(PieceState p, char direction)
    {
        if (direction == 'L')
        {
            if (edge(0) + p.edge(2) == 0)
                return true;
        }
        else if (direction == 'T')
        {
            if (edge(1) + p.edge(3) == 0)
                return true;
        }
        else if (direction == 'R')
        {
            if (edge(2) + p.edge(0) == 0)
                return true;
        }
        else if (direction == 'B')
        {
            if (edge(3) + p.edge(1) == 0)
                return true;
        }
        return false;
    }
}
