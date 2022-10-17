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
            Debug.Log(unit.Moviment);
            inrangetiles = rangefinder.GetTilesInRange(unit.activetile, unit.Moviment);
            //; unit.Moviment -= 6; // reset moviment range

            var besttarget = new Unit_Control();  // PROBLEMA
            var bestvalue = -1;
            foreach (var tiles in inrangetiles) // look at all possible moviments
            {
                if (tiles.Hasunit) // TODO: ve se tem uma unidade, e aliada e pega a distancia dela pra unidade principal, devolve o path pra unidade mais perto
                {
                    var a = map_manager.GetUnit(tiles.gridlocation, groundtile);
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
            if (bestvalue >= 0 ) // attack
            {
                path = pathfinder.FindPath(unit.activetile, besttarget.activetile, inrangetiles);
                path.RemoveAt(path.Count - 1);
                cursor.path = path;
                unit.Moviment -= 1;
                Debug.Log(path.Count); 
                while (path.Count > 0)
                {
                    cursor.MoveAlongPath(unit);
                }
            }
        }
        return null;
    }
}
