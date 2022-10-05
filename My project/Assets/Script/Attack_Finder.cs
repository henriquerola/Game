using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class Attack_Finder
{
    public List<Selected_Tile> BasicAttack(Selected_Tile unittile, int range) 
    {
        Map_Manager map_manager = GameObject.Find("Grid").GetComponentInChildren<Map_Manager>();

        var inrangetiles = new List<Selected_Tile>(); // all the tiles tha are in range
        int stepcount = 0; // steps away from starting tile

        inrangetiles.Add(unittile);

        var tilepriviousstep = new List<Selected_Tile>();
        tilepriviousstep.Add(unittile);

        while (stepcount < range)
        {
            var surroundingtiles = new List<Selected_Tile>();

            foreach (var tile in tilepriviousstep) // makes so you dont have to search tiles that you already searched
            {
                surroundingtiles.AddRange(map_manager.GetNeighborTiles(tile, new List<Selected_Tile>()));
            }

            inrangetiles.AddRange(surroundingtiles);
            tilepriviousstep = surroundingtiles.Distinct().ToList(); // add alll tiles from last step
            stepcount++;
        }

        return inrangetiles.Distinct().ToList();
    }
}
