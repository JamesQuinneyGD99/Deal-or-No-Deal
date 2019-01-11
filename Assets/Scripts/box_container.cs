using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney | QUI16000158

public class box_container : MonoBehaviour {
    public GameObject boxPrefab; // These are the money boxes which are opened by the player
    public List<float> boxValues; // This is the values in each of the boxes
    public List<float> boxValuesCache; // This is used to retrieve data on each box even after they are removed;
    public int[] rounds; // This is how many boxes the player has to choose each round
    public int currentRound = 0; // This is the round the player is currently on
    public int boxesToChoose; // This is how many boxes the player currently has to choose
    public GameObject playerBox; // This is the player's box, they choose it before the first round

    // This will create a box and tell it what number it is
    void CreateBox(Vector2 pos,int boxNumber){
        GameObject newBox = Instantiate(boxPrefab,transform,false); // We create a new box within the box container
        newBox.transform.position = pos; // We move our box to the specified position
        newBox.GetComponent<box>().boxNumber = boxNumber; // We tell the box what its number is
    }

    // This will end the current round and start the next
    public void EndRound(){
        currentRound+=1; // We move to the next round
        boxesToChoose = rounds[currentRound]; // We let the player choose the boxes for the next round
    }

    // Start is called before the first frame update
    void Start(){
        Vector2 boxPos = new Vector2(-4.0f,4.0f); // This is the position the next box will spawn at
        
        // This will loop through each box and swap the value with another box at random
        for(int i = 0;i<boxValues.Count;i+=1){
            int randomBox = Random.Range(0,boxValues.Count); // This will select a box at random
            float savedValue = boxValues[randomBox]; // We save our random box
            // The next two lines swap our random box, and the box at the current index
            boxValues[randomBox] = boxValues[i];
            boxValues[i] = savedValue;
        }

        /*
            This will save our box values into a cached list,
            this means that we have a copy of our list for
            individual boxes to reference since box indexes
            change throughout the game

            We will also use this loop to create our boxes
         */
        for(int i = 0;i<boxValues.Count;i+=1){
            boxValuesCache.Add(boxValues[i]); // We backup the current index in box values

            CreateBox(boxPos,i); // We create our box at the current position

            // The following code will set the position for the next box
            boxPos += new Vector2(1.0f,0.0f); // We move our next box position to the right
            // We check if the box has moved too far to the right
            if(boxPos.x>=5.0f){
                boxPos = new Vector2(-4.0f,boxPos.y + -1.0f); // We move our box to the start of the next line
            }
        }
    }
}
