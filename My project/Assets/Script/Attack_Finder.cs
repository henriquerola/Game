using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class Attack_Finder
{
    public List<Selected_Tile> BasicAttack(Unit_Control unit, int ID, int index = 0)  //hits 1 tile in unit.Range (all directions)
    {
        if(unit.Habilities[index] == "BasicAttack")
        {
            Map_Manager map_manager = GameObject.Find("Grid").GetComponentInChildren<Map_Manager>();

            var inrangetiles = new List<Selected_Tile>(); // all the tiles tha are in unit.Range
            int stepcount = 0; // steps away from starting tile

            inrangetiles.Add(unit.activetile);

            var tilepriviousstep = new List<Selected_Tile>();
            tilepriviousstep.Add(unit.activetile);

            while (stepcount < unit.Range)
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
            inrangetiles.Remove(unit.activetile);
            return inrangetiles.Distinct().ToList();
        }

        if(unit.Habilities[index] == "YAttack")
        {
            Map_Manager map_manager = GameObject.Find("Grid").GetComponentInChildren<Map_Manager>();
            var inrangetiles = new List<Selected_Tile>();

            var startingtiles = map_manager.GetNeighborTiles(unit.activetile, new List<Selected_Tile>()); //tiles where the attack start
            inrangetiles.AddRange(startingtiles);
            foreach(var tile in startingtiles)
            {
                if(unit.activetile.transform.position.x + 0.5 == tile.transform.position.x)
                {
                    if(unit.activetile.transform.position.y  + 0.25 == tile.transform.position.y) // right
                    {
                        inrangetiles.AddRange(map_manager.GetLineTiles(tile, 1, 0));
                    }
                    else // down
                    {
                        inrangetiles.AddRange(map_manager.GetLineTiles(tile, 0, -1));
                    }
                }
                else if(unit.activetile.transform.position.x - 0.5 == tile.transform.position.x)
                {
                    if(unit.activetile.transform.position.y  + 0.25 == tile.transform.position.y) // up
                    {
                        inrangetiles.AddRange(map_manager.GetLineTiles(tile, 0, 1));
                    }
                    else
                    {
                        inrangetiles.AddRange(map_manager.GetLineTiles(tile, -1, 0)); // left
                    }
                }
            }
            
            return inrangetiles.Distinct().ToList();
        }
        return new List<Selected_Tile>();
    }

    public void AttackAction(Unit_Control unit, Selected_Tile targettile) // attack process
    {
        Map_Manager map_manager = GameObject.Find("Grid").GetComponentInChildren<Map_Manager>();
        var groundtile = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();

        if(targettile.Hasunit)
        {
            var target = map_manager.GetUnit(targettile.gridlocation, groundtile);

            if(target != null)
            {
                Unit_Control targetunit = target.GetComponent<Unit_Control>();
                if(!targetunit.ally) // attack
                {
                    AttackAnimation();
                    targetunit.CurrentHP -= unit.Damage;
                }
            }
            // process attack
        }
    }

    public void AttackAnimation()
    {
        // animation of attacks
    }
}
