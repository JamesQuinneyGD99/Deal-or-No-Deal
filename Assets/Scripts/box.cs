using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class box : MonoBehaviour {
    public Text boxText; // This is the text object on the front of the box
    public int boxNumber; // This is the box's number
    public box_container boxContainer; // This allows us to access the box container from the box
    public banker bankerScript; // This allows us to access the banker from the box
    public GameObject dondPanel; // This is the deal or no deal panel

    // This will reveal what is inside the box
    public void RevealBox(){
        boxText.text = "£"+boxContainer.boxValuesCache[boxNumber]; // This retrieves the box value and puts it on the front of the box
    }

    // Start is called before the first frame update
    void Start(){
        boxText.text = ""+boxNumber; // This will put the box number on the front of the box
        boxContainer = GameObject.Find("Box Container").GetComponent<box_container>(); // This saves our box container script
        bankerScript = GameObject.Find("Banker").GetComponent<banker>(); // This saves our banker script
        dondPanel = GameObject.Find("Deal or No Deal Panel"); // This allows us to access the deal or no deal panel quicker

        GetComponent<Button>().onClick.AddListener(DoClick); // This tells the button to call DoClick when we click it
    }

    // This is ran whenever the user clicks the box
    public void DoClick(){
        // We check to see if the player has chosen their box yet
        if(boxContainer.playerBox == null){
            boxContainer.playerBox = this.gameObject; // We give the player this box
            boxContainer.boxValues.Remove(boxContainer.boxValuesCache[boxNumber]); // We remove the player box from the game
            GetComponent<Button>().interactable = false; // We make it so the player can't click this box
            transform.position = new Vector2(-2.85f,-1.3f); // We move the box

            boxContainer.EndRound();

            // We tell the player how many boxes they can choose
            bankerScript.bankerText.text = "Please choose " + boxContainer.boxesToChoose + " boxes";
        }
        else if(boxContainer.boxesToChoose > 0 && boxContainer.playerBox != this.gameObject){
            RevealBox(); // We open the box
            boxContainer.boxesToChoose-=1; // We tell the player they can choose one fewer box
            boxContainer.boxValues.Remove(boxContainer.boxValuesCache[boxNumber]); // We remove the current box from the game
            GetComponent<Button>().interactable = false; // We make it so the player can't click this box anymore

            GameObject[] sidePanelValues = GameObject.FindGameObjectsWithTag("panel value"); // We find all side panel values
            // This checks through all side panel values
            for(int i = 0;i<sidePanelValues.Length;i+=1){
                // We check if the current side panel is equal to the box which was just removed
                if(sidePanelValues[i].name==""+boxContainer.boxValuesCache[boxNumber]){
                    Destroy(sidePanelValues[i]); // We remove the value from the side of the screen
                }
            }

            // We check if the player can choose no more boxes
            if(boxContainer.boxesToChoose == 0){
                bankerScript.MakeOffer(); // We tell the banker to make an offer
                dondPanel.transform.localScale = new Vector2(1.0f,1.0f); // Make the deal or no deal panel visible
            }
            else{
                // We tell the player how many boxes they can choose
                bankerScript.bankerText.text = "Please choose " + boxContainer.boxesToChoose + " boxes";
            }
        }
    }
}
