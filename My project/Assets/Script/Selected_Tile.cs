using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected_Tile : MonoBehaviour
{
    
    public float APcost; // apcost mostra o custo para andar desse tile para outro
    public bool Hasunit; // has a unit above it
    // variables needed for pathfinding
    public int G;
    public int H;
    public int F { get { return G + H; } }
    public bool IsBlocked;
    public Selected_Tile Previoustile;

    public Vector3Int gridlocation;

    private bool is_selected;

    private void Start() {
        hidetile();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            hidetile();
            is_selected = false;
        } 
    }
    
    public void showtile() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        is_selected = true;
    }

    public void hidetile() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
    }
}

