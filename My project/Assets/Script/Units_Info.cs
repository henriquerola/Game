using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units_Info
{
    // in order 
    public string[] Name = {
        "BlueSlime",
        "GreenSlime"
    };

    public int[] MaxHP = {
        3,
        3
    };
    public int[] Damage = {
        1,
        1
    };
    public int[] Range = {
        1,
        1
    };
    public string[] Type = {
        "Slime",
        "Slime"
    };
    public int[] MaxMoviment = {
        6,
        6
    };
    public string[] Behaviour = {
        "Brute",
        "Coward"
    };
    // not in order
    public string[,] Habilities = new string[2,2]{ {"BasicAttack","XAttack"}, {"BasicAttack","YAttack"} };  
}
