using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units_Info
{
    // in order 
    public string[] Name = {
        "Blue Slime",
        "Green Slime",
        "Novice Adventurer",
        "Novice Archer",
        "Novice Mage",
        "Novice Knight",
        "Veteran Adventurer",
    };

    public int[] MaxHP = {
        2,
        2,
        3,
        4,
        4,
        5,
        4
    };
    public int[] Damage = {
        1,
        1,
        1,
        1,
        1,
        2,
        1
    };
    public int[] Range = {
        1,
        1,
        1,
        3,
        1,
        1,
        1
    };
    public string[] Type = {
        "Slime",
        "Slime",
        "Human",
        "Human",
        "Human",
        "Human",
        "Human"
    };
    public int[] MaxMoviment = {
        3,
        3,
        3,
        4,
        4,
        3,
        4
    };
    public string[] Behaviour = {
        "Brute",
        "Brute",
        "Brute",
        "Brute",
        "Brute",
        "Brute",
        "Coward",
    };
    // not in order
    public string[,] Habilities = new string[7,2]{{"BasicAttack","Nothing"}, {"BasicAttack","Nothing"}, {"BasicAttack", "Nothing"}, {"BasicAttack", "Nothing"}, {"BasicAttack", "YAttack"}, {"BasicAttack","Nothing"}, {"BasicAttack","Nothing"}};  
}
