using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// take care of the pathfinding of units
public class Path_Finder
{
    Map_Manager map_manager;
    public List<Selected_Tile> FindPath(Selected_Tile Start, Selected_Tile End) 
    {
        List<Selected_Tile> Openlist = new List<Selected_Tile>(); // list with all tiles that are not yet checked
        List<Selected_Tile> Closelist = new List<Selected_Tile>(); // list with all the tiles checked

        Openlist.Add(Start);

        while(Openlist.Count > 0)
        {
            Selected_Tile Currenttile = Openlist.OrderBy(x => x.F).First(); // get the lowest f value of a tile by using linq

            Openlist.Remove(Currenttile);
            Closelist.Add(Currenttile);

            if(Currenttile == End)
            {
                // finalize the loop
                return GetFinishedList(Start, End);
            }

            var Neighbortiles = map_manager.GetNeighborTiles(Currenttile); // get neightbor tiles

            foreach(var neighbor in Neighbortiles)
            {
                if(neighbor.IsBlocked || Closelist.Contains(neighbor))
                {
                    continue;
                }

                neighbor.G = GetManhattanDistance(Start, neighbor);
                neighbor.H = GetManhattanDistance(End, neighbor);

                neighbor.Previoustile = Currenttile;

                if(!Openlist.Contains(neighbor))
                {
                    Openlist.Add(neighbor);
                }
            }
        }
        return new List<Selected_Tile>();
    }

    private int GetManhattanDistance(Selected_Tile Start,Selected_Tile neighbor)
    {
        return Mathf.Abs(Start.gridlocation.x - neighbor.gridlocation.x) + Mathf.Abs(Start.gridlocation.y - neighbor.gridlocation.y);
    }

    private List<Selected_Tile> GetFinishedList(Selected_Tile Start,Selected_Tile End) 
    {
        List<Selected_Tile> Finishedlist = new List<Selected_Tile>();

        Selected_Tile currenttile = End;

        while (currenttile != Start)
        {
            Finishedlist.Add(currenttile);
            currenttile = currenttile.Previoustile;
        }

        Finishedlist.Reverse();
        return Finishedlist;
    }
}
