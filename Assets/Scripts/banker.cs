using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class banker : MonoBehaviour {
    public box_container boxContainer; // This allows the banker to interact with the box container
    public float bankerOffer; // This is the offer the banker is offering to the player
    public float highestOffer; // This will keep track of the highest offer
    public Text bankerText; // This is the text the banker uses to talk to the player
    public GameObject dondPanel; // This is so we can show the panel for the box swap
    bool isBoxSwap; // This is whether we are in the final round

    // This runs before the first fram
    void Start(){
        bankerText = GetComponent<Text>(); // This allows other game objects to access the text
    }

    // This is when the player clicks the Deal button
    public void DoDeal(){
        // We check if the player has been offered a box swap
        if(isBoxSwap){
            GameObject[] remainingBoxes = GameObject.FindGameObjectsWithTag("box"); // This is an array with all boxes

            // We loop through every box, even the ones which have already been removed
            for(int i = 0;i<remainingBoxes.Length;i+=1){
                // We search for the last box
                if(remainingBoxes[i].GetComponent<Button>().interactable && remainingBoxes[i] != boxContainer.playerBox){
                    // We open both boxes
                    remainingBoxes[i].GetComponent<box>().RevealBox();
                    boxContainer.playerBox.GetComponent<box>().RevealBox();

                    // We tell the player what they have won
                    bankerText.text = "Congratulations!\n You win: £"
                        + boxContainer.boxValuesCache[remainingBoxes[i].GetComponent<box>().boxNumber];
                    dondPanel.transform.localScale = new Vector2(0.0f,0.0f); // We hide the deal or no deal panel
                }
            }
        }
        else{
            dondPanel.transform.localScale = new Vector2(0.0f,0.0f); // We hide the deal or no deal panel
            bankerText.text = "Congratulations!\n You win: £" + bankerOffer; // We tell the player they won the offer
        }
    }

    // This is when the player clicks the No Deal button
    public void DoNoDeal(){
        // We check if the player has been offered a box swap
        if(isBoxSwap){
            GameObject[] remainingBoxes = GameObject.FindGameObjectsWithTag("box"); // This is an array with all boxes

            // We loop through every box, even the ones which have already been removed
            for(int i = 0;i<remainingBoxes.Length;i+=1){
                // We search for the last box
                if(remainingBoxes[i].GetComponent<Button>().interactable && remainingBoxes[i] != boxContainer.playerBox){
                    // We open both boxes
                    remainingBoxes[i].GetComponent<box>().RevealBox();
                    boxContainer.playerBox.GetComponent<box>().RevealBox();

                    // We tell the player what they have won
                    bankerText.text = "Congratulations!\n You win: £"
                        + boxContainer.boxValuesCache[boxContainer.playerBox.GetComponent<box>().boxNumber];
                    dondPanel.transform.localScale = new Vector2(0.0f,0.0f); // We hide the deal or no deal panel
                }
            }
        }
        else{
            // We check if we are on the last round
            if(boxContainer.currentRound+1==boxContainer.rounds.Length){
                bankerText.text = "I would like to offer you a box swap\nDeal or No Deal?"; // Offer the player a box swap
                isBoxSwap = true;
            }
            else{
                dondPanel.transform.localScale = new Vector2(0.0f,0.0f); // We hide the deal or no deal panel
                boxContainer.EndRound(); // We end the round if it isn't the last
                // We tell the player how many boxes they can choose
                bankerText.text = "Please choose " + boxContainer.boxesToChoose + " boxes";
            }
        }
    }

    // This is used to set the banker's offer
    public void MakeOffer(){
        float currentOffer = 0.0f; // We set the current offer at 0 to begin

        // We check if it is the last round
        if(boxContainer.currentRound+1 == boxContainer.rounds.Length){
            // The last round is always the average of the player box and the last box

            float lastBoxValue = boxContainer.boxValues[0]; // This is the value in the last box
            int playerBoxNumber = boxContainer.playerBox.GetComponent<box>().boxNumber; // This is the player's box's number
            float playerBoxValue = boxContainer.boxValuesCache[playerBoxNumber]; // This is the value in the player's box
            
            currentOffer = (lastBoxValue + playerBoxValue) / 2.0f; // We set the offer to the average between the player's box and the last box
        }
        else{
            List<float> redBoxes = new List<float>(); // These are boxes with a value equal to or above 1000
            List<float> blueBoxes = new List<float>(); // These are boxes with a value lessar than 1000

            // This will look through each box and sort them into red and blue values
            for(int i = 0;i<boxContainer.boxValues.Count;i+=1){
                // We check if the box has a value above 1000
                if(boxContainer.boxValues[i] >= 1000.0f){
                    // Add the box to the red list
                    redBoxes.Add(boxContainer.boxValues[i]);
                }
                else{
                    // Add the box to the blue list
                    blueBoxes.Add(boxContainer.boxValues[i]);
                }
            }

            // We check if we have any red boxes left
            if(redBoxes.Count > 0){
                // We look through each red box
                for(int i = 0;i<redBoxes.Count;i+=1){
                    currentOffer += redBoxes[i]; // We increase our offer by the current box's value
                }
                // Divide the offer by the total red boxes
                currentOffer /= redBoxes.Count;
            }
            else{
                // We look through each blue box instead
                for(int i = 0;i<blueBoxes.Count;i+=1){
                    currentOffer += blueBoxes[i]; // We increase our offer by the current box's value
                }
                // Divide the offer by the total blue boxes
                currentOffer /= blueBoxes.Count;
            }

            currentOffer /= boxContainer.boxValues.Count; // We divide the value by the total number of boxes

            // We divide our offer by the total rounds minus the current round, we then clamp the value between 1 and the total rounds
            currentOffer /= Mathf.Clamp(boxContainer.rounds.Length-boxContainer.currentRound, 1, boxContainer.rounds.Length);
        }

        bankerOffer = Mathf.Floor(currentOffer); // We save our offer
        // If the current offer is above the previous highest offer, we change the highest offer
        if(bankerOffer>highestOffer){
            highestOffer=bankerOffer;
        }
        bankerText.text = "I would like to offer you £"+bankerOffer+"\nHighest Offer: £"+highestOffer; // We set the banker text
    }
}
