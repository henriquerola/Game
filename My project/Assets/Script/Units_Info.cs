using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units_Info
{
    // in order 
    public string[] Name = {
        "BlueSlime",
        "GreenSlime",
        "Novice Adventurer",
        "Novice Archer",
        "Novice Knight",
        "Novice Mage",
        "Veteran Adventurer",
    };

    public int[] MaxHP = {
        2,
        2,
        4
    };
    public int[] Damage = {
        1,
        1,
        1
    };
    public int[] Range = {
        1,
        1,
        1
    };
    public string[] Type = {
        "Slime",
        "Slime",
        "Human"
    };
    public int[] MaxMoviment = {
        3,
        3,
        4,
    };
    public string[] Behaviour = {
        "Brute",
        "Coward"
    };
    // not in order
    public string[,] Habilities = new string[3,2]{ {"BasicAttack","XAttack"}, {"BasicAttack","YAttack"}, {"BasicAttack", "Nothing"}};  
}
