using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class managerScript : MonoBehaviour
{
    public static managerScript Instance { get; private set; }

    public GameObject[] hangars;
    public GameObject[] planes;
    public GameObject[] lights;
    public int planesInHangar;
    public int hangarNum = 1;
    public int planeNums = 2;
    public bool isParking;
    public bool LightsOn = false;
    public bool planesParked = false;
    public GameObject parkSuccess;

    // Zet het script om naar een static function zodat andere scripts hierheen kunnen callen.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        // Zorgt dat alle hangars, lichtjes en vliegtuigen in een array worden gezet.

        hangars = GameObject.FindGameObjectsWithTag("Hangars");
        planes = GameObject.FindGameObjectsWithTag("Planes");
        lights = GameObject.FindGameObjectsWithTag("Lights");

        // Zorgt dat elke hangar de bijhorende nummer op het dak krijgt weergegeven met gebruik van TextMeshPro.
        // Hangarnum is het weergegeven nummer, dit is waarom telkens als er een hangar bijkomt, er een verhoging van hangerNum is.

        foreach (GameObject hangar in hangars)
        {
            TextMeshPro TextMeshes = hangar.GetComponentInChildren<TextMeshPro>();
            TextMeshes.text = hangarNum.ToString();
            hangarNum++;
        }

        // Zelfde concept als bij de hangarnummers.

        foreach (GameObject plane in planes)
        {
            planeDescription planeDesc = plane.GetComponent<planeDescription>();
            planeDesc.planeNumber = planeNums;
            foreach (GameObject hangar in hangars)
            {

                /* 
                *  Hier kijkt het script of het vliegtuig een matchend nummer heeft met een hangaar. 
                *  Daarna pakt het script het tweede child-object van de hangaar GameObject, wat eigenlijk gewoon een positie is.
                *  Daarna zet pakt het de planeMovement component van het vliegtuig en geeft het hier de locatie van de parkeerlocatie aan door.
                */

                TextMeshPro TextMeshes = hangar.GetComponentInChildren<TextMeshPro>();
                string number = planeDesc.planeNumber.ToString();
                if (number == TextMeshes.text)
                {
                    GameObject parkLocation = hangar.gameObject.transform.GetChild(2).gameObject;
                    planeMovement planeMoveAI = plane.GetComponent<planeMovement>();
                    planeMoveAI.parkingSpot = parkLocation.transform.position;
                }
            }
            planeNums++;
        }

    }

    // Gebruiker heeft op parkeren gedrukt.

    public void parkPlanes()
    {
        isParking = true;
    }

    // Schakelt de lichten aan of uit

    public void lightsOnOff()
    {
        if (!LightsOn)
        {
            foreach (GameObject light in lights)
            {
                light.GetComponent<Light>().enabled = true;
            }
            LightsOn = true;
        }
        else
        {
            foreach (GameObject light in lights)
            {
                light.GetComponent<Light>().enabled = false;
            }
            LightsOn = false;
        }
    }

    // Checkt of alle vliegtuigen geparkeerd zijn en activeert de tekst.

    public void checkIfParked()
    {
        if (planesInHangar == planes.Length)
        {
            planesParked = true;
            parkSuccess.SetActive(true);
            StartCoroutine(disappearTime());
        }
    }

    // De tekst dat alle vliegtuigen zijn geparkeerd zal na 5 seconden weer verdwijnen.

    IEnumerator disappearTime()
    {
        yield return new WaitForSeconds(5f);
        parkSuccess.SetActive(false);
    }
}



