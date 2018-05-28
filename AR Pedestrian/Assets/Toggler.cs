using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggler : MonoBehaviour {

    public GameObject GlobalToggle;
    public GameObject LocalToggle;

	// Use this for initialization
	void Start () {
        if (!GlobalToggle.GetComponent<Toggle>().isOn) { GlobalToggle.GetComponent<Toggle>().isOn = !GlobalToggle.GetComponent<Toggle>().isOn; };
        if (LocalToggle.GetComponent<Toggle>().isOn) { LocalToggle.GetComponent<Toggle>().isOn = !LocalToggle.GetComponent<Toggle>().isOn; };
        
    }

    // Update is called once per frame
    void Update() {

    }
}
