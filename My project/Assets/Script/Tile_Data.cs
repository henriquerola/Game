using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu] // cria um menu no unity pra eu poder ver as coisas dessa classe
public class Tile_Data : ScriptableObject
{
    public TileBase[] tiles;

    public string Name; // name of terrain
    public float APcost; // apcost mostra o custo para andar desse tile para outro
    public bool Hasunit = false; // has a unit above it

    void Update() {
    }
}
