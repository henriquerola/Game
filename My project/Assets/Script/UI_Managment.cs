using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Managment : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; //just so i can chosse wich camera
    [SerializeField] private TextMeshProUGUI unitname;
    [SerializeField] public TextMeshProUGUI unitinfo;

    // Start is called before the first frame update
    void Start()
    {
        unitname.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        var battlesystem = GameObject.Find("BattleSystem").GetComponent<Battle_System>();
        var cursor = GameObject.Find("Cursor").GetComponent<Mouse_Controler>();
        var hab1 = transform.Find("UIHab1");
        var hab2 = transform.Find("UIHab2");
        var portrait = transform.Find("UIPortrait");

        unitinfo.text = ""; // reset unitinfo

        if(cursor.selectedunit != null) // show selected unit GUI
        {
            unitname.text = cursor.selectedunit.Name;

            portrait.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            if(cursor.selectedunit.Attack) // atk GUI
            {
                if(cursor.HabID == 0)
                {
                    hab1.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
                    hab2.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
                }
                else if(cursor.HabID == 1)
                {
                    hab2.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
                    hab1.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
                }
            } else if(cursor.selectedunit.ally) // ally GUI
            {
                hab1.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
                hab2.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            } else  // enemy GUI
            {
                hab1.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
                hab2.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            }
        } else // hide unit GUI
        {
            unitname.text = "";
            portrait.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            hab1.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            hab2.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        }
        if(Input.GetMouseButtonDown(0) && battlesystem.State != battlestate.START && !cursor.ismoving) // check selected GUI
        {
            Vector3 cursorposition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //x y z coord of the mouse
            Vector2 cursorposition2D = new Vector2(cursorposition.x,cursorposition.y); // xy coord
            RaycastHit2D[] hits = Physics2D.RaycastAll(cursorposition2D, Vector2.zero); // a list of all the thing the mouse hit 
            if(hits.Length > 0) {
                for (int i = 0; i < hits.Length; i++) // manage Clicked UI
                {
                    if (hits[i].collider.gameObject.name == "UIEndTurnButtom")
                    {
                        battlesystem.State = battlestate.ENDTURN;
                    }

                    if (hits[i].collider.gameObject.name == "UIHab1")
                    {
                        if (cursor.selectedunit != null && cursor.selectedunit.ally)
                        {
                            cursor.selectedunit.Attack = !cursor.selectedunit.Attack;
                            cursor.HabID = 0;
                        }
                    }

                    if (hits[i].collider.gameObject.name == "UIHab2")
                    {
                        if (cursor.selectedunit != null && cursor.selectedunit.ally)
                        {
                            cursor.selectedunit.Attack = !cursor.selectedunit.Attack;
                            cursor.HabID = 1;
                            // hits[i].collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
                        }
                    }
                }
            }
        }
    }
}
