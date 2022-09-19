using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit_Control : MonoBehaviour
{
    public string Name;
    public int MaxHP;
    public int CorrentHP;

    public bool SelectedUnit = false;
    public bool Hover = false;
    public bool ally = false;

    Map_Manager map_manager;


    Mouse_Controler mouse;

    void Awake() {
        CorrentHP = MaxHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        
        if(this.transform.parent.name == "Ally Units") {
            this.ally = true;
        }
        if(this.transform.parent.name == "Enemy Units") {
            this.ally = false;
        }
        
        if(Hover) { // deixar brilhante ou mostrar informções basicas aqui

        }

        if(SelectedUnit) { // abrir a possibilidade de movimento ou ataque para unidades aliadas e mostrar informações de unidades inimigas
            if(Input.GetMouseButtonDown(0)) { // click com uma unidade selecionada, logo ela se move pra onde vc clicar.
                Vector2 newposition = unitmoviment();

                Vector3 check = newposition;

                Vector3Int check2 = Vector3Int.FloorToInt(check);

                map_manager = GameObject.FindGameObjectWithTag("map_manager").GetComponent<Map_Manager>();
                
                Tile_Data a = map_manager.GetTileData(check2);
                if (a != null) {
                    if (a.Hasunit == true) {
                        Debug.Log("has a unit");
                    }
                    else {
                        
                    }
                }
                else 
                {
                    transform.position = newposition;
                }
            }
        }

        if(Input.GetMouseButtonDown(0)) { //seleciona a unidade q ta em cima do mouse
            if(Hover) { 
                SelectedUnit = true;
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

    private Vector2 unitmoviment() {
        mouse = GameObject.Find("Cursor").GetComponent<Mouse_Controler>();
        Vector2 newpos = new Vector2(mouse.transform.position.x,mouse.transform.position.y);
        return newpos; 
    }
}
