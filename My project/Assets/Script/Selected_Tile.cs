using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrowTranslator;

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

    public List<Sprite> arrows;

    private void Start() {
        hidetile();
    }
    
    public void showtile() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
    }

    public void hidetile() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        SetArrowSprite(ArrowDirection.None);
    }

    public void SetArrowSprite(ArrowDirection d)
    {
        // var arrow = GetComponentsInChildren<SpriteRenderer>(); // [1]
        if(d == ArrowDirection.None)
        {
            //arrow.color = new Color(1,1,1,0);
        }
        else
        {
            //arrow.color = new Color(1,1,1,1);
            //arrow.sprite = arrow[(int)d];
            //arrow.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }
    }
}

