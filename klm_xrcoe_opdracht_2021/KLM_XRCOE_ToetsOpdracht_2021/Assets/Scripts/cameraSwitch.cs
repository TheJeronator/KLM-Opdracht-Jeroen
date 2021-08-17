using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] planes;
    public Camera[] cams;
    public List<Camera> planeCams = new List<Camera>(); 
    public int currentCamera = 0;
    public int maxCameras;


    /* 
     * Pakt alle cameras uit alle vliegtuig objecten en voegt ze toe aan de planeCams lijst.
     * Daarna zet het de maxCameras int naar het aantal cameras in de planeCams lijst.
     */

    public void Awake()
    {
        planes = GameObject.FindGameObjectsWithTag("Planes");
        foreach (GameObject thisplane in planes)
        {
            Camera cam = thisplane.GetComponentInChildren<Camera>();
            planeCams.Add(cam);
        }
        maxCameras = planeCams.Count;
    }

    // Start zorgt er hier voor dat alle cameras behalve de overzichtelijke (Van bovenaf kijkende) camera. 

    public void Start()
    {
        foreach (Camera cam in planeCams)
        {
            cam.enabled = false;
        }
    }

    /*
     * Dit script zorgt er voor dat er tussen de cameras geswitched kan worden
     * Eerst checkt het of de currentCamera int minder is dan het totale aantal cameras 
     * Als dit zo is, dan schakelt het alle overige cameras uit en schakelt het de camera in
     * die op dit moment hetzelfde nummer heeft in de lijst van cameras als de currentCamera integer.
     * Ook schakelt het de overzichtelijke cammera uit en voegt het 1 toe aan de currentCamera integer.
     */

    public void switchCams()
    {
         if (currentCamera < planeCams.Count)
        {
            foreach (Camera cam in planeCams)
            {
                cam.enabled = false;
            }
            planeCams[currentCamera].enabled = true;
            mainCamera.enabled = false;
            currentCamera++;
        }
         /*
          * Als de integer currentCamera groter is dan het totale aantal cameras zal het systeem de lijst
          * opnieuw beginnen door eerst terug te switchen naar de overzichtelijke camera en daarna de integer
          * currentCamera terugzetten naar 0.
          */ 

        else if (currentCamera >= planeCams.Count)
        {
            mainCamera.enabled = true;
            foreach (Camera cam in planeCams)
            {
                cam.enabled = false;
            }
            currentCamera = 0;
        }

         // Hier zorgt het dat de lijst ook inderdaad opnieuw afgelopen wordt door te beginnen met 0.

         else if (currentCamera == 0)
        {
            foreach (Camera cam in planeCams)
            {
                cam.enabled = false;
            }
            planeCams[0].enabled = true;
            mainCamera.enabled = false;
            currentCamera++;
        }
    }
}