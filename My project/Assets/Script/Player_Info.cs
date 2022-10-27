using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Info : MonoBehaviour // information about the player units, completion of the game and achievements
{
    public List<int> player_units = new List<int>();
    public GameObject Unitprefab;
    public int unitsingame = 0;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            player_units.Add(i+2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
