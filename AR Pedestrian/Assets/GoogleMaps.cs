using UnityEngine;
using System.Collections;

public class GoogleMaps : MonoBehaviour
{

    string exampleUrl = "http://maps.googleapis.com/maps/api/staticmap?" +
                        "center=Calgary&zoom=13&size=1080x1920&maptype=roadmap";
    string key = "&key=AIzaSyBZdz6ErozDyUv4yK8iqrOagwS7WyeVC1M"; //put your own API key here.

    IEnumerator Start()
    {
        WWW www = new WWW(exampleUrl + key);
        yield return www;
        GetComponent<Renderer>().material.mainTexture = www.texture;
    }
}