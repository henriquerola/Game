using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum battlestate {START, PLAYERTURN, ENDTURN, ENEMYTURN, WON, LOST}

public class Battle_System : MonoBehaviour
{
    public battlestate State;
    public GameObject Unitprefab; // isso vai virar uma lista conforme for adicionando mais unidades (possivelmente duas listas/ uma pros enimigos e outra para os aliados)
    public GameObject EnemyUnits;
    public GameObject AllyUnits;
    public Map_Manager map_manager;

    public Tilemap ground; // conectado na grid, logo in children vai pegar ground como var

    private Unit_Behaiviours unitbehaiviours;

    // Start is called before the first frame update
    void Start()
    {
        State = battlestate.START;
        setupbattle();
    }

    void setupbattle() {
        
        BoundsInt bounds = ground.cellBounds; // pega o limite do mapa

        int max_enemies = 100; //numero maximo de enimigos (bom quando quiser mudar a dificuldade do jogo)
        Unit_Control unit;

        // Esse loop é o q vai spawnar objetos,pontos de interesse, inimigos iniciais etc etc
        for (int y = bounds.min.y; y < bounds.max.y; y++) { // loop pelo ground (da pra fazer ele passar por outros grid como elevation)
            for (int x = bounds.min.x; x < bounds.max.x; x++) {
                // loc do tile
                var tilelocation = new Vector3Int(x,y,0);
                // ve se tem um tile na localizacao (da pra fazer buracos com isso) 
                if(ground.HasTile(tilelocation)) {
                    int randnum = Random.Range(1,max_enemies); // por enquanto  é random pra testar as unidades

                    if(randnum <= 2) {
                        var cellpos = ground.GetCellCenterWorld(tilelocation); //localizacao como cell e nao pos
                        Unitprefab.transform.position = new Vector3(cellpos.x,cellpos.y,cellpos.z+1); 
                        int rand = Random.Range(0,2);
                        if(rand == 1) {
                            unit = Instantiate(Unitprefab, EnemyUnits.transform).GetComponent<Unit_Control>();
                        } else {
                            unit = Instantiate(Unitprefab, AllyUnits.transform).GetComponent<Unit_Control>();

                        }
                    }
                }
            }
        }
        State = battlestate.PLAYERTURN;
    }

    // Update is called once per frame
    void Update()
    {
        if(State == battlestate.ENDTURN)
        {
            EndTurn();
            State = battlestate.ENEMYTURN;
        }
        if(State == battlestate.ENEMYTURN)
        {
            Unit_Control[] units = EnemyUnits.GetComponentsInChildren<Unit_Control>();
            foreach(var unit in units) // execute ai behaivour for each unit
            {
                EnemyAction(unit);
            }
            var cursor = GameObject.Find("Cursor").GetComponent<Mouse_Controler>();
            if(!cursor.ismoving)
            {
                cursor.selectedunit = null;
                cursor.enabled = false;
            }
            State = battlestate.PLAYERTURN;
            cursor.enabled = true;
        }
    }

    public Unit_Control GetSelectedUnit(GameObject AllyUnits, GameObject EnemyUnits)
    {
        Unit_Control[] units = AllyUnits.GetComponentsInChildren<Unit_Control>();

        foreach(var unit in units) 
        {
            if(unit.SelectedUnit == true)
            {
                return unit;
            }
        }

        units = EnemyUnits.GetComponentsInChildren<Unit_Control>();

        foreach(var unit in units) 
        {
            if(unit.SelectedUnit == true)
            {
                return unit;
            }
        }
        return null;
    }

    private void EndTurn()
    {
        Unit_Control[] units = AllyUnits.GetComponentsInChildren<Unit_Control>();

        foreach(var unit in units)
        {
            unit.Moviment = unit.MaxMoviment;
        }
    }

    private void EnemyAction(Unit_Control unit) // TODO: menemy moviment and action.
    {
        unitbehaiviours = new Unit_Behaiviours();
        unitbehaiviours.EnemyMoviment(unit);
    }
}
