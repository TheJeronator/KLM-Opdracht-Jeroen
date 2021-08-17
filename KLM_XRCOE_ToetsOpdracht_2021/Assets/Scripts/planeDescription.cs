using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class planeDescription : MonoBehaviour
{
    public Plane plane;
    public int planeNumber;
    public string planeText;
    
    // Hier wordt het merk en het nummer van het vliegtuig boven het vliegtuig zelf gedisplayed met 
    // behulp van TextMeshPro.

    public void Start()
    {
        TextMeshPro[] planeUI = GetComponentsInChildren<TextMeshPro>();
        planeText = plane.planeBrand.ToString() + " " + plane.planeType.ToString();
        planeUI[0].text = planeText;
        planeUI[1].text = "Number " + planeNumber.ToString();
    }
}
