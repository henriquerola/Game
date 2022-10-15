using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Behaiviours
{
    private Path_Finder pathfinder;
    private Range_Finder rangefinder;
    private Attack_Finder attackfinder;

    private List<Selected_Tile> inrangetiles = new List<Selected_Tile>();
    private List<Selected_Tile> inattackrange = new List<Selected_Tile>();

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

        if (unit.Behaviour == "Brute") // closest unit to brute (meele)
        {
            rangefinder = new Range_Finder();

            unit.Moviment += 1; // get units that are 1 away from moviment range
            inrangetiles = rangefinder.GetTilesInRange(unit.activetile, unit.Moviment);
            unit.Moviment -= 1; // reset moviment range

            Debug.Log(inrangetiles.Count);

            foreach (var tiles in inrangetiles) // look at all possible moviments
            {
                if (tiles.Hasunit) // TODO: ve se tem uma unidade, e aliada e pega a distancia dela pra unidade principal, devolve o path pra unidade mais perto
                {

                }
            } 
        }
        return null;
    }
}
