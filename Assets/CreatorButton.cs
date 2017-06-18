using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorButton : MonoBehaviour {

    //public int type;
    public GameObject Object;
    private PlayerObjectHolder OBJHolder;
    public int type; 
    public bool active;
	// Use this for initialization
	void Start () {
        OBJHolder = GetComponentInParent<PlayerObjectHolder>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void GrabObject()
    {
        active = true;
        OBJHolder.ActiveObject(Object);
        OBJHolder.DeactivateotherButton(type);
    }

    public void Deactivate()
    {
        active = false;
    }

}
