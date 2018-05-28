using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour {

    public GameObject TextPanel;
    bool isActive = false;

	// Use this for initialization
	void Start () {
        TextPanel.SetActive(isActive);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    isActive = !isActive;
                    TextPanel.SetActive(isActive);
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    
                    break;
            }
        }

        if ( Input.GetMouseButtonDown(0))
        {
            isActive = !isActive;
            TextPanel.SetActive(isActive);
        }
    }

}

