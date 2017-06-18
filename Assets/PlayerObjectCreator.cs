using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectCreator : MonoBehaviour {

    private PlayerObjectHolder PlayerObjectHolder;
    public GameObject ParentforObjects;
    private Camera cam;
    private EscortObj EscortObj;

    
    public bool PlayerOnTile = false;
    public bool ObjectValid = true;

    public GameObject ObjHeld;
	// Use this for initialization
	void Start () {
        PlayerObjectHolder = FindObjectOfType<PlayerObjectHolder>();
        cam = FindObjectOfType<CameraControl>().GetComponent<Camera>();
        EscortObj = FindObjectOfType<EscortObj>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

   

    private void OnMouseDown()
    {
        //Debug.Log("MouseClicked");

        if (PlayerObjectHolder.ReadyObject != null && EscortObj.transform.position.z < (transform.position.z + 5))
        {
            if (ObjHeld == null && PlayerOnTile == false && ObjectValid)
            {
                Vector3 location = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z - 7);
                //   location = new Vector3(location.x, 0, location.y);
                Instantiate(PlayerObjectHolder.ReadyObject, location, Quaternion.identity, ParentforObjects.transform);
                ObjHeld = PlayerObjectHolder.ReadyObject;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EscortObj>())
        {
         
            PlayerOnTile = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<EscortObj>())
        {

            PlayerOnTile = false;
        }
    }

    public void TogglePlacement(bool value)
    {
        ObjectValid = value;
    }

}
