using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units_Info : MonoBehaviour
{
    public List<List<int>> Units;

    public Dictionary<int,string> Types = new Dictionary<int,string>() 
    {
        {1,"Slime"}
    };

    public Dictionary<int,SpriteRenderer> Image;
    // public Dictionary<int,Skills> Skills;
    //  List<Unit> unit = [HP,Damage,Image, Mov, Type, hab1, hab2,...];


    // Units.Add(new List<int>() {3,1,1,4,1}); // Blue Slime
}
