using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class Attack_Finder
{
    public List<Selected_Tile> BasicAttack(Unit_Control unit, int ID)  //hits 1 tile in unit.Range (all directions)
    {
        if(unit.Habilities == "BasicAttack")
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
        return null;
    }

    public List<Selected_Tile> XRange(Unit_Control unit) // function to pick diferent shapes of attack
    {
        return null;
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
