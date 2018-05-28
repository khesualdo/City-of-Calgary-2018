using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using City_of_Calgary_2018;


public class ShowLocation : MonoBehaviour {
	DataLayer dl;
    public Text m_Text;

    IEnumerator Start()
    {
    	dl = new DataLayer();
    	//Fetch the Toggle GameObject
        //Add listener for when the state of the Toggle changes, to take action

        //Initialise the Text to say the first state of the Toggle
        m_Text.text = "In Start method";
        if (!Input.location.isEnabledByUser)
            m_Text.text = "Not enabled";
        // // First, check if user has location service enabled
        print(Input.location.status);
     	//yield break;

        // Start service before querying location
        Input.location.Start();
        print(Input.location.status);
        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            print("Initializing");
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
        	//m_Text.text = "Timed out";
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped )
        {
        	//m_Text.text = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }

        yield break;
    }

    void Update()
    {
        m_Text.text = "DOWNTOWN COMMERCIAL CORE";
    }

}
