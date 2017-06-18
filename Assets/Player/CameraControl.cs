using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    // Use this for initialization
    Vector3 PlayerPosition;
    float ZDeltaPos;

    void Start () {
        PlayerPosition = FindObjectOfType<EscortObj>().transform.position;
        ZDeltaPos = transform.position.z - PlayerPosition.z;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        PlayerPosition = FindObjectOfType<EscortObj>().transform.position;
      //  Debug.Log(PlayerPosition);
       


        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(PlayerPosition.x, transform.position.y, PlayerPosition.z + ZDeltaPos), Time.deltaTime*20);
	}
}
