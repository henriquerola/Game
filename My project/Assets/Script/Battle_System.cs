using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum battlestate {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class Battle_System : MonoBehaviour
{
    public battlestate State;
    public GameObject Unitprefab; // isso vai virar uma lista conforme for adicionando mais unidades (possivelmente duas listas/ uma pros enimigos e outra para os aliados)
    public GameObject EnemyUnits;
    public GameObject AllyUnits;

    public Tilemap ground; // conectado na grid, logo in children vai pegar ground como var

    // Start is called before the first frame update
    void Start()
    {
        State = battlestate.START;
        setupbattle();
    }

    void setupbattle() {
        
        BoundsInt bounds = ground.cellBounds; // pega o limite do mapa

        int max_enemies = 100; //numero maximo de enimigos (bom quando quiser mudar a dificuldade do jogo)

        // Esse loop é o q vai spawnar objetos,pontos de interesse, inimigos iniciais etc etc
        for (int y = bounds.min.y; y < bounds.max.y; y++) { // loop pelo ground (da pra fazer ele passar por outros grid como elevation)
            for (int x = bounds.min.x; x < bounds.max.x; x++) {
                // loc do tile
                var tilelocation = new Vector3Int(x,y,0);
                // ve se tem um tile na localizacao (da pra fazer buracos com isso) 
                if(ground.HasTile(tilelocation)) {
                    int randnum = Random.Range(1,max_enemies); // por enquanto  é random pra testar as unidades

                    if(randnum <= 5) {
                        var cellpos = ground.GetCellCenterWorld(tilelocation); //localizacao como cell e nao pos
                        Unitprefab.transform.position = new Vector3(cellpos.x,cellpos.y,cellpos.z+1); 
                        int rand = Random.Range(0,2);
                        if(rand == 1) {
                            Instantiate(Unitprefab, EnemyUnits.transform);
                        } else {
                            Instantiate(Unitprefab, AllyUnits.transform);
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
        
    }
}