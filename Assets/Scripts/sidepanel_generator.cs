using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sidepanel_generator : MonoBehaviour {
    public GameObject valuePrefab; // This is the value object which appears on the side panel
    public GameObject bluePanel; // This is the panel on the left of the screen
    public GameObject redPanel; // This is the panel on the right of the screen
    Vector2 redPos; // This is where the next red box will appear
    Vector2 bluePos; // This is where the next blue box will appear
    public List<float> boxValuesUnsorted; // These are the box values in order from low to high

    // Start is called before the first frame update
    void Start(){
        bluePos = new Vector2(-5.6f,4.53f); // The starting position for blue values
        redPos = new Vector2(5.6f,4.53f); // The starting position for red values

        // We loop through every box value
        for(int i = 0;i<boxValuesUnsorted.Count;i+=1){
            // We check if the value of the box is above or equal to 1000
            if(boxValuesUnsorted[i]>=1000.0f){
                GameObject newValue = Instantiate(valuePrefab,redPanel.transform,false); // We create a new panel within the red panel
                newValue.transform.position = redPos; // We set the position of the value panel
                newValue.transform.GetChild(0).gameObject.GetComponent<Text>().text = "£"+boxValuesUnsorted[i]; // We set the text of the value panel
                newValue.name = ""+boxValuesUnsorted[i]; // We set the name of the value panel

                redPos -= new Vector2(0.0f,0.75f); // We set the position for the next value panel
            }
            else{
                GameObject newValue = Instantiate(valuePrefab,bluePanel.transform,false); // We create a new panel within the red panel
                newValue.transform.position = bluePos; // We set the position of the value panel
                newValue.transform.GetChild(0).gameObject.GetComponent<Text>().text = "£"+boxValuesUnsorted[i]; // We set the text of the value panel
                newValue.name = ""+boxValuesUnsorted[i]; // We set the name of the value panel

                bluePos -= new Vector2(0.0f,0.75f); // We set the position for the next value panel
            }
        }
    }
}
