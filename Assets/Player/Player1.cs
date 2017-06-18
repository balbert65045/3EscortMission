using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Player1 : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider col)
        {
         //   if (col.gameObject.GetComponent<Arrow>())
            {
                SendMessage("Dead");
            }
        }
    }


