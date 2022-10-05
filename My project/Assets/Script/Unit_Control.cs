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
    public int Range;
    public string Type;
    public string Habilities;
    public int MaxMoviment;
    public int Moviment;

    // information for internal use
    public bool SelectedUnit = false;
    public bool Hover = false;
    public bool ally = false;
    public bool Attack = false;

    public Selected_Tile activetile;
    public List<Sprite> Sprites;
    public int ID = 1;

    // Start is called before the first frame update
    void Start()
    {
        AwakeUnit();
        CurrentHP = MaxHP;
        Moviment = MaxMoviment;
        Attack = false;
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
            
        }

        if(Input.GetMouseButtonDown(0)) { //seleciona a unidade q ta em cima do mouse
            if(Hover) { 
                SelectedUnit = true;
                //GetUnitStats(this);
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
        Mouse_Controler mouse = GameObject.Find("Cursor").GetComponent<Mouse_Controler>();
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
    // give the right stats/skills and images for the unit
    private void AwakeUnit() 
    {
        Units_Info units = new Units_Info();
        SpriteRenderer image = GetComponent<SpriteRenderer>();
        image.sprite = Sprites[ID];
        Name = units.Name[ID];
        MaxHP = units.MaxHP[ID];
        Damage = units.Damage[ID];
        Range = units.Range[ID];
        Type = units.Type[ID];
        MaxMoviment = units.MaxMoviment[ID];
        Habilities = units.Habilities[ID];
    }
}
