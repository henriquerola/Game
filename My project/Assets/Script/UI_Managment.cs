using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Managment : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; //just so i can chosse wich camera

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 cursorposition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //x y z coord of the mouse
            Vector2 cursorposition2D = new Vector2(cursorposition.x,cursorposition.y); // xy coord
            RaycastHit2D[] hits = Physics2D.RaycastAll(cursorposition2D, Vector2.zero); // a list of all the thing the mouse hit 
            if(hits.Length > 0) {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject.name == "UIEndTurnButtom")
                    {
                        var battlesystem = GameObject.Find("BattleSystem").GetComponent<Battle_System>();
                        battlesystem.State = battlestate.ENDTURN;
                    }
                }
            }
        }
        
    }


}
