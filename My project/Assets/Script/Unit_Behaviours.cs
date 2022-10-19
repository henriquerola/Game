using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit_Behaiviours
{
    private Path_Finder pathfinder;
    private Range_Finder rangefinder;
    private Attack_Finder attackfinder;

    private List<Selected_Tile> inrangetiles = new List<Selected_Tile>();
    private List<Selected_Tile> inattackrange = new List<Selected_Tile>();
    public List<Selected_Tile> path = new List<Selected_Tile>();

    //pathfinder = new Path_Finder();
    //rangefinder = new Range_Finder();
    //attackfinder = new Attack_Finder();


    public void EnemyMoviment(Unit_Control unit) // takes care of enemy moviment
    {
        if (unit.Behaviour == "Brute") // brute behaviour
        {
            var target = ClosestTarget(unit);

            if(target != null)
            {
                var cursor = GameObject.Find("Cursor").GetComponent<Mouse_Controler>(); 
                pathfinder = new Path_Finder();
                rangefinder = new Range_Finder();

                var attacktiles = rangefinder.GetTilesInRange(target.activetile, 1); // get closest tiles
                List<Selected_Tile> bestpath = new List<Selected_Tile>();
                int bestmoviment = 1000;
                foreach (var tile in attacktiles)
                {
                    if(!tile.Hasunit && inrangetiles.Contains(tile))
                    {
                        var Distance = pathfinder.FindPath(unit.activetile, tile, inrangetiles);
                        if(Distance.Count < bestmoviment && Distance.Count != 0)
                        {
                            bestmoviment = Distance.Count;
                            bestpath = Distance;
                        }
                    }
                }
                if(bestmoviment != 1000) // check best path
                {
                    cursor.path = bestpath;
                    while(cursor.path.Count > 0)
                    {
                        if(cursor.path.Count == 1)
                        {
                            cursor.path[0].Hasunit = true;
                        }
                        cursor.MoveAlongPath(unit);
                    }

                }
            }
            AttackEnemy(unit,target);
        }
    }

    public Unit_Control ClosestTarget(Unit_Control unit) // get closest unit in attack range
    {
        var cursor = GameObject.Find("Cursor").GetComponent<Mouse_Controler>();
        var map_manager = GameObject.Find("Grid").GetComponent<Map_Manager>();
        var groundtile = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        pathfinder = new Path_Finder();

        if (unit.Behaviour == "Brute") // closest unit to brute (meele)
        {
            rangefinder = new Range_Finder();

            unit.Moviment += 1; // get units that are 1 away from moviment range
            inrangetiles = rangefinder.GetTilesInRange(unit.activetile, unit.Moviment);
            unit.Moviment -= 1;

            var besttarget = new Unit_Control();
            var bestvalue = -1;
            foreach (var tiles in inrangetiles) // look at all possible moviments
            {
                if (tiles.Hasunit)
                {
                    var a = map_manager.GetUnit(tiles.gridlocation, groundtile);
                    if(a != null)
                    {
                        var targetobject = a.GetComponent<Unit_Control>();

                        if (targetobject.ally) // is an player unit
                        {
        
                            List<Selected_Tile> Distance = pathfinder.FindPath(unit.activetile, tiles, inrangetiles);
                            var value = unit.Moviment - Distance.Count; // menor distancia == moviment - distancia dela pra unidade 
                            if (value > bestvalue)
                            {
                                besttarget = targetobject;
                                bestvalue = value;
                            }
                        }
                    }
                }
            }
            if (bestvalue >= 0 )
            {
                return besttarget;
            } else
            {
                inrangetiles = rangefinder.GetTilesInRange(unit.activetile, 100);
                besttarget = new Unit_Control();
                bestvalue = -1;
                foreach (var tiles in inrangetiles) // look at all possible moviments
                {
                    if (tiles.Hasunit)
                    {
                        var a = map_manager.GetUnit(tiles.gridlocation, groundtile);
                        if(a != null)
                        {
                            var targetobject = a.GetComponent<Unit_Control>();

                            if (targetobject.ally) // is an player unit
                            {
            
                                List<Selected_Tile> Distance = pathfinder.FindPath(unit.activetile, tiles, inrangetiles);
                                var value = unit.Moviment - Distance.Count; // menor distancia == moviment - distancia dela pra unidade 
                                if (value > bestvalue)
                                {
                                    besttarget = targetobject;
                                    bestvalue = value;
                                }
                            }
                        }
                    }
                }
                if (bestvalue >= 0 )
                {
                    return besttarget;
                }
            }
        }
        return null;
    }

    public List<Selected_Tile> FindMovimentPath(Unit_Control unit, Unit_Control target) // put find moviments in here
    {
        return new List<Selected_Tile>();
    }

    private void AttackEnemy(Unit_Control unit, Unit_Control target)
    {
        if(unit.Behaviour == "Brute")
        {
            attackfinder = new Attack_Finder();
            inattackrange = attackfinder.BasicAttack(unit, unit.ID);
            if(unit != null && target != null)
            {
                if(inattackrange.Contains(target.activetile))
                {
                    attackfinder.AttackAction(unit, target.activetile);
                }
            }
        }
    }
}
