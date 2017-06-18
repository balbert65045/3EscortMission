using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectHolder : MonoBehaviour {

    public CreatorButton[] PlayerObjects;
    public GameObject ReadyObject;
    private TerrainControll TerrainControll;

    // Use this for initialization
    void Start()
    {
        PlayerObjects = GetComponentsInChildren<CreatorButton>();
        TerrainControll = FindObjectOfType<TerrainControll>();
    }
	
	// Update is called once per frame
	void Update () {
    
       
    }

    public void DeactivateotherButton(int type)
    {
        foreach (CreatorButton CB in PlayerObjects){
            if (CB.type != type)
            {
                CB.Deactivate();
            }
        }
    }

    public void ActiveObject(GameObject Obj)
    {
        TerrainControll.ObjectReady(Obj);
        ReadyObject = Obj;
    }
}
