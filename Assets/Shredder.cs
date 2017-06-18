using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

    private CameraControl MainCamera;
    Vector3 InitalOfset;
	// Use this for initialization
	void Start () {
        MainCamera = FindObjectOfType<CameraControl>();
        InitalOfset = MainCamera.transform.position - transform.position;


    }
	
	// Update is called once per frame
	void Update () {
        transform.position = MainCamera.transform.position - InitalOfset;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggeredWith" + other.gameObject.name);
        if (other.gameObject.GetComponent<AddedObj>() != null)
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
