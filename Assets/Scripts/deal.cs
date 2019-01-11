using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// James Quinney | QUI16000158

public class deal : MonoBehaviour {
    public banker bankerScript; // We send our choice to the banker
    public bool isDeal; // Whether this is the deal or no deal button
    public GameObject dondPanel; // We use this to hide the panel later on

    // Start is called before the first frame update
    void Start(){
        GetComponent<Button>().onClick.AddListener(DoClick); // We tell the button to point to DoClick when it is pressed
    }

    void DoClick(){
        if(isDeal){
            bankerScript.DoDeal(); // We tell the banker we accept his deal
        }
        else{
            bankerScript.DoNoDeal(); // We tell the banker we didn't accept his deal
        }
    }
}
