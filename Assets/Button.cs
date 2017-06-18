using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button : MonoBehaviour {

    // Use this for initialization
    private Button[] buttonArray;
   // public GameObject DefenderPrefab; 
   // public static GameObject DefenderSelected;
    public Text CostText;

	void Start () {
     //   buttonArray = GameObject.FindObjectsOfType<Button>();
       // CostText.text = DefenderPrefab.GetComponent<Defender>().StarCost.ToString();
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        foreach (Button thisButton in buttonArray){
            //thisButton.GetComponent<SpriteRenderer>().color = Color.black;
        }
 //       GetComponent<SpriteRenderer>().color = Color.white;
        // Debug.Log(name + "pressed");
 //       DefenderSelected = DefenderPrefab;
    }

    

 //   void OnMouseUp()
 //   {
 //       GetComponent<SpriteRenderer>().color = Color.black;
 //   }
}
