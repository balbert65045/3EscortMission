using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour {


    PlayerObjectCreator[] PlayerObjectCreators;
    public PlayerObjectCreator[] OrderdPlayerObjectCreators;

    private GameObject ReadyObject;
    int length;
    // Use this for initialization
    void Start () {
        PlayerObjectCreators = GetComponentsInChildren<PlayerObjectCreator>();
        OrderdPlayerObjectCreators = GetComponentsInChildren<PlayerObjectCreator>();
        length = PlayerObjectCreators.Length - 1;

        for (int i =0; i < PlayerObjectCreators.Length; i++)
        {
            int index = 0;
            for (int n = 0; n < PlayerObjectCreators.Length; n++)
            {
                if (i == n)
                {

                }
                else if (PlayerObjectCreators[i].transform.position.x > PlayerObjectCreators[n].transform.position.x)
                {
                    index++;
                }
            }
            OrderdPlayerObjectCreators[index] = PlayerObjectCreators[i];
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActiveObject(GameObject Obj)
    {
        if (Obj.GetComponent<BoxArrow>() != null)
        {
            //Toggle All on
            for (int i = 0; i < PlayerObjectCreators.Length; i++)
            {
                 OrderdPlayerObjectCreators[i].TogglePlacement(true);
            }

            //if farthest left tile disable LeftBox
            if (Obj.GetComponent<BoxArrow>().BoxType == 1)
            {
                OrderdPlayerObjectCreators[0].TogglePlacement(false);

                //if Tile to the left is a right box, disable LeftBox
                for (int i = 0; i < PlayerObjectCreators.Length; i++)
                {
                    if (i == 0)
                    {

                    }
                    
                    else if (OrderdPlayerObjectCreators[i].ObjHeld != null && OrderdPlayerObjectCreators[i].ObjHeld.GetComponent<BoxArrow>() != null)
                    {

                        if (OrderdPlayerObjectCreators[i].ObjHeld.GetComponent<BoxArrow>().BoxType == 2)
                        {
                            OrderdPlayerObjectCreators[i + 1].TogglePlacement(false);
                        }
                    }
                }
              }
            //if farthest Right tile disable RightBox
            else if (Obj.GetComponent<BoxArrow>().BoxType == 2)
            {
                OrderdPlayerObjectCreators[length].TogglePlacement(false);
                //if Tile to the right tile dis a left box, disabel rightbox
                for (int i = 0; i < PlayerObjectCreators.Length; i++)
                {
                    if (i == length)
                    {

                    }
                    else if (OrderdPlayerObjectCreators[i].ObjHeld != null && OrderdPlayerObjectCreators[i].ObjHeld.GetComponent<BoxArrow>() != null)
                    {
                        if (OrderdPlayerObjectCreators[i ].ObjHeld.GetComponent<BoxArrow>().BoxType == 1)
                        {
                            OrderdPlayerObjectCreators[i - 1].TogglePlacement(false);
                        }
                    }
                }
            }

        }
    }

}
