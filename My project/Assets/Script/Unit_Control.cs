using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit_Control : MonoBehaviour
{
    // display information for player
    public string Name;
    public int MaxHP;
    public int CurrentHP;
    public int Damage;
    public string Type;
    public List<string> Habilities;

    // information for internal use
    public bool SelectedUnit = false;
    public bool Hover = false;
    public bool ally = false;

    Map_Manager map_manager;

    Mouse_Controler mouse;

    void Awake() {
        CurrentHP = MaxHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        
        if(this.transform.parent.name == "AllyUnits") {
            this.ally = true;
        }
        if(this.transform.parent.name == "EnemyUnits") {
            this.ally = false;
        }
        
        if(Hover) { // deixar brilhante ou mostrar informções basicas aqui

        }

        if(SelectedUnit) { // abrir a possibilidade de movimento ou ataque para unidades aliadas e mostrar informações de unidades inimigas
            if(Input.GetMouseButtonDown(0) && this.ally) { // click com uma unidade selecionada, logo ela se move pra onde vc clicar.
                Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // posisaco 2d do mouse
                Vector2 newposition = unitmoviment();

                map_manager = GameObject.FindGameObjectWithTag("map_manager").GetComponent<Map_Manager>(); // get map_manager

                if(map_manager.IsInMap(mouseposition, newposition))
                {
                    Vector3Int gridposition = map_manager.map.WorldToCell(newposition);
                    Selected_Tile destinationtile = map_manager.GetTileObject(gridposition, map_manager.GetComponentInChildren<Tilemap>()); // get the selected tile in the destination
                    // sees if it can move the unit to the tile. if it has another unit in it or not.
                    if (destinationtile != null) 
                    {
                        if(destinationtile.Hasunit)
                        {
                            Debug.Log("invalid location, already has a unit");
                        }
                        else
                        {
                            transform.position = newposition;
                        }
                    }
                }
            }
        }

        if(Input.GetMouseButtonDown(0)) { //seleciona a unidade q ta em cima do mouse
            if(Hover) { 
                SelectedUnit = true;
                GetUnitStats(this);
            } else {
                SelectedUnit = false;
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D collider) { // mouse ta em cima de uma unidade
        Hover = true;
    }

    private void OnTriggerExit2D(Collider2D collider) { // mouse saiu de cima da unidade
        Hover = false;
    }

    // gets the mouse position has a vector2
    private Vector2 unitmoviment() {
        mouse = GameObject.Find("Cursor").GetComponent<Mouse_Controler>();
        Vector2 newpos = new Vector2(mouse.transform.position.x,mouse.transform.position.y);
        return newpos; 
    }

    private void GetUnitStats(Unit_Control unit) {
        Debug.Log(unit.Name);
        Debug.Log(unit.MaxHP);
        Debug.Log(unit.CurrentHP);
        Debug.Log(unit.Damage);
        Debug.Log(unit.Type);
        Debug.Log(unit.Habilities);
    }
}
