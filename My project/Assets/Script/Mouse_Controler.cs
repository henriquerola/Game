using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mouse_Controler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; //just so i can chosse wich camera

    public float Speed;

    private Path_Finder pathfinder;

    public Unit_Control selectedunit;

    private void Start() 
    {
        pathfinder = new Path_Finder();
    }

    // Start is called before the first frame update
    private void LateUpdate() {
        var focusontilehit = focusontile(); 

        if(focusontilehit.HasValue) { // verifica se esta em cima de um tile 

            Selected_Tile selectedtile = focusontilehit.Value.collider.gameObject.GetComponent<Selected_Tile>(); //pega aonde esta esse tile
            transform.position = selectedtile.transform.position; // coloca o mous no lugar certo

            if(Input.GetMouseButtonDown(0)) {
                selectedtile.showtile(); //tile branco pra mostrar q vc selecionou ele (chama o select_tiles.cs)

                if(selectedunit != null) 
                {
                    if(selectedtile.Hasunit)
                    {

                    } else
                    {
                        Debug.Log("pathfinding executed");
                        var path = pathfinder.FindPath(selectedunit.activetile ,selectedtile);
                    }
                }
            }
        }
        Battle_System system = GameObject.Find("BattleSystem").GetComponent<Battle_System>();
        selectedunit = system.GetSelectedUnit(system.EnemyUnits, system.AllyUnits); // get current selected unit
    }

    public RaycastHit2D? focusontile() {
        Vector3 cursorposition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //x y z coord of the mouse
        Vector2 cursorposition2D = new Vector2(cursorposition.x,cursorposition.y); // xy coord

        RaycastHit2D[] hits = Physics2D.RaycastAll(cursorposition2D, Vector2.zero); // a list of all the thing the mouse hit 

        if(hits.Length > 0) {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.name == "Selected Tile(Clone)")
                {
                    return hits[i];
                }
            }
        }
        return null;
    }
}
