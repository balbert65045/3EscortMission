using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainControll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ObjectReady(GameObject Obj)
    {
        BroadcastMessage("ActiveObject", Obj);
    }
}
