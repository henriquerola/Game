using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using static ArrowTranslator;

public class Mouse_Controler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; //just so i can chosse wich camera

    public float Speed = 1;
    private List<Selected_Tile> path = new List<Selected_Tile>();
    private List<Selected_Tile> inrangetiles = new List<Selected_Tile>();
    private List<Selected_Tile> inattackrange = new List<Selected_Tile>();

    private Path_Finder pathfinder;
    private Range_Finder rangefinder;
    private Attack_Finder attackfinder;
    private ArrowTranslator arrowtranslator;

    public Unit_Control selectedunit;

    private bool ismoving = false;
    private int HabID = 0;

    private void Start() 
    {
        pathfinder = new Path_Finder();
        rangefinder = new Range_Finder();
        arrowtranslator = new ArrowTranslator();
        attackfinder = new Attack_Finder();
    }

    // Start is called before the first frame update
    private void LateUpdate() {
        var focusontilehit = focusontile();

        if(selectedunit != null)
        {
            GetInRangeTiles(selectedunit);
            if(selectedunit.ally)  // unit basic attack 
            {
                if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) // attack mode
                {
                    selectedunit.Attack = !selectedunit.Attack;
                    HabID = 0;
                }
                if(Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) // attack mode
                {
                    selectedunit.Attack = !selectedunit.Attack;
                    HabID = 1;
                }
            }
            if(selectedunit.Attack) // display attack
            {
                inattackrange = attackfinder.BasicAttack(selectedunit, 1, HabID);
                GetAttackRange(selectedunit);
            }
        }
        
        if(focusontilehit.HasValue) { // verifica se esta em cima de um tile 

            Selected_Tile selectedtile = focusontilehit.Value.collider.gameObject.GetComponent<Selected_Tile>(); //pega aonde esta esse tile
            transform.position = selectedtile.transform.position; // coloca o mouse no lugar certo

            if(inrangetiles.Contains(selectedtile) && !ismoving)
            {
                if(selectedunit != null && selectedunit.ally && !selectedunit.Attack)
                {
                    path = pathfinder.FindPath(selectedunit.activetile ,selectedtile, inrangetiles);

                    foreach (var tile in inrangetiles) // hide arrows
                    {
                        tile.SetArrowSprite(ArrowDirection.None);
                    }
                    for (int i = 0; i < path.Count; i++) // show arrows
                    {
                        var previoustile = i > 0 ? path[i - 1] : selectedunit.activetile;
                        var futuretile = i < path.Count - 1? path[i + 1] : null;
                        var arrowdir = arrowtranslator.TranslateDirection(previoustile, path[i], futuretile);
                        path[i].SetArrowSprite(arrowdir);
                    }
                }
            }

            if(Input.GetMouseButtonDown(0)) {

                if(selectedunit != null)  // se uma unidade esta selecionada
                {
                    if(selectedunit.ally && inrangetiles.Contains(selectedtile) && !selectedtile.Hasunit && !selectedunit.Attack) // se é um movimento valido
                    {
                        ismoving = true;
                        selectedunit.Moviment -= path.Count;
                    }
                    if(selectedunit.ally && inattackrange.Contains(selectedtile) && selectedunit.Attack) // se é um ataque válido
                    {
                        Debug.Log("Valid Attack");
                        attackfinder.AttackAction(selectedunit, selectedtile);
                    }
                }
            }
        }

        if(path.Count > 0 && ismoving)
        {
            MoveAlongPath(selectedunit);
            if(path.Count == 0)
            {
                selectedunit.SelectedUnit = true;
            }

        } else
        {
            Battle_System system = GameObject.Find("BattleSystem").GetComponent<Battle_System>();
            selectedunit = system.GetSelectedUnit(system.EnemyUnits, system.AllyUnits); // get current selected unit
        }
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

    private void MoveAlongPath(Unit_Control unit)
    {
        var step = Speed * Time.deltaTime;

        var zindex = path[0].transform.position.z; // preserve z position since we will be using vector2 to change the unit position z= 0
        unit.transform.position = Vector2.MoveTowards(unit.transform.position, path[0].transform.position, step);
        unit.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y, zindex);

        if(Vector2.Distance(unit.transform.position, path[0].transform.position) < 0.0001) // see if unit pos is equal to tile pos
        {
            PositionUnitOnTile(unit, path[0]); 
            path.RemoveAt(0);
        }

        if( path.Count == 0 )
        {
            GetInRangeTiles(unit);
            ismoving = false;
        }
        
    }
    // put unit pos = to tile pos and unit.activetile
    public void PositionUnitOnTile(Unit_Control unit, Selected_Tile tile)
    {
        unit.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
        unit.activetile = tile;
    }

    public void GetInRangeTiles(Unit_Control unit)
    {
        foreach (var tile in inrangetiles)
        {
            tile.hidetile();
        }

        inrangetiles = rangefinder.GetTilesInRange(unit.activetile, unit.Moviment);

        foreach (var tile in inrangetiles)
        {
            tile.showtile();
        }
    }

    public void GetAttackRange(Unit_Control unit)
    {
        foreach (var tile in inattackrange)
        {
            tile.hidetile();
        } 

        inattackrange = attackfinder.BasicAttack(unit, 1, HabID);
        
        foreach (var tile in inattackrange)
        {
            tile.showattacktile();
        }
    }
}
