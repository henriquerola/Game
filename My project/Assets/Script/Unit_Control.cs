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
    public List<string> Habilities;
    public int MaxMoviment;
    public int Moviment;

    // information for internal use
    public bool SelectedUnit = false;
    public bool Hover = false;
    public bool ally = false;
    public bool Attack = false;
    public string Behaviour;

    public Selected_Tile activetile;
    public List<Sprite> Sprites;
    public int ID;

    // Start is called before the first frame update
    void Start()
    {
        if(this.transform.parent.name == "AllyUnits") { // if mind control exists put this in update
            this.ally = true;
            ID = 1;
        }
        if(this.transform.parent.name == "EnemyUnits") {
            this.ally = false;
            ID = 0;
        }

        AwakeUnit();
        CurrentHP = MaxHP;
        Moviment = MaxMoviment;
        Attack = false;
    }

    // Update is called once per frame
    void Update() {
        CheckCondition();
        
        if(Hover) { // deixar brilhante ou mostrar informções basicas aqui
            var UI = GameObject.Find("UI").GetComponent<UI_Managment>();
            UI.unitinfo.text = "HP: " + this.CurrentHP + "/" + this.MaxHP + "\nMV: " + this.Moviment + "/" + this.MaxMoviment + "\nTYPE: " + this.Type;

        }

        if(SelectedUnit) { // abrir a possibilidade de movimento ou ataque para unidades aliadas e mostrar informações de unidades inimigas
            
        }

        if(Input.GetMouseButtonDown(0)) { //seleciona a unidade q ta em cima do mouse
            var cursor = GameObject.Find("Cursor").GetComponent<Mouse_Controler>();
            var focusontilehit = cursor.focusontile();
            if(Hover) { 
                SelectedUnit = true;
                //GetUnitStats(this);
            } else if(focusontilehit.HasValue){
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
        for(int i = 0; i < 2; i++)
        {
            Habilities.Add(units.Habilities[ID,i]);
        }
        if(!ally)
        {
            Behaviour = units.Behaviour[ID];
        }
    }

    private void CheckCondition() // check conditions (death, poison, fire, selection)
    {
        var renderer = GetComponentInChildren<SpriteRenderer>();
        if(this.CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
        if(this.Hover || this.SelectedUnit)
        {
            renderer.material.SetFloat("_IsSelected", 1);
            if(this.Hover)
            {
                renderer.material.SetColor("_OutlineColor", Color.yellow);
            } else
            {
                renderer.material.SetColor("_OutlineColor", Color.green);
            }
        } else
        {
            renderer.material.SetFloat("_IsSelected", 0);
        }
    }
}
