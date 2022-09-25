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
    public Vector2Int grid2Dlocation { get { return new Vector2Int(gridlocation.x, gridlocation.y); } }

    public bool is_selected;

    private void Start() {
        hidetile();
    }
    
    public void showtile() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
    }

    public void hidetile() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
    }
}

