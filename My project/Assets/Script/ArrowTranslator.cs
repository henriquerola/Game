using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTranslator
{
    public enum ArrowDirection {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
        Topright = 5,
        Bottomright = 6,
        Topleft = 7,
        Bottomleft = 8,
        UpFinished = 9,
        DownFinished = 10,
        LeftFinished = 11,
        Rightfinished = 12
    }

    public ArrowDirection TranslateDirection(Selected_Tile previoustile, Selected_Tile currenttile, Selected_Tile futuretile)
    {
        bool isfinal = futuretile == null; // if futuretile is null isfinal is true

        Vector2Int pastdirection = previoustile != null ? currenttile.grid2Dlocation - previoustile.grid2Dlocation: new Vector2Int(0,0); // based on previoustile give the direction of last arrow
        Vector2Int futuredirection = futuretile != null ? futuretile.grid2Dlocation - currenttile.grid2Dlocation: new Vector2Int(0,0);
        Vector2Int direction = pastdirection != futuredirection ? pastdirection + futuredirection : futuredirection;

        if (direction == new Vector2Int(0,1) && !isfinal)
        {
            return ArrowDirection.Up;
        }
        if (direction == new Vector2Int(0,-1) && !isfinal)
        {
            return ArrowDirection.Down;
        }
        if (direction == new Vector2Int(1,0) && !isfinal)
        {
            return ArrowDirection.Right;
        }
        if (direction == new Vector2Int(-1,0) && !isfinal)
        {
            return ArrowDirection.Left;
        }

        if (direction == new Vector2Int(1,1))
        {
            if (pastdirection.y < futuredirection.y)
            {
                return ArrowDirection.Bottomleft;
            } else 
            {
                return ArrowDirection.Topright;
            }
        }
        if (direction == new Vector2Int(-1,1))
        {
            if (pastdirection.y < futuredirection.y)
            {
                return ArrowDirection.Bottomright;
            } else 
            {
                return ArrowDirection.Topleft;
            }
        }
        if (direction == new Vector2Int(1,-1))
        {
            if (pastdirection.y > futuredirection.y)
            {
                return ArrowDirection.Topleft;
            } else 
            {
                return ArrowDirection.Bottomright;
            }
        }
        if (direction == new Vector2Int(-1,-1))
        {
            if (pastdirection.y > futuredirection.y)
            {
                return ArrowDirection.Topright;
            } else 
            {
                return ArrowDirection.Bottomleft;
            }
        }

        if (direction == new Vector2Int(0,1) && isfinal)
        {
            return ArrowDirection.UpFinished;
        }
        if (direction == new Vector2Int(0,-1) && isfinal)
        {
            return ArrowDirection.DownFinished;
        }
        if (direction == new Vector2Int(1,0) && isfinal)
        {
            return ArrowDirection.Rightfinished;
        }
        if (direction == new Vector2Int(-1,0) && isfinal)
        {
            return ArrowDirection.LeftFinished;
        }

        return ArrowDirection.None;
    }
}
