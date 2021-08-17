using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Plane", menuName = "Plane")]
public class Plane : ScriptableObject {

    // type vliegtuig en het merk van het vliegtuig, deze worden ook weergegeven boven de vliegtuigen zelf.

    public string planeType;
    public string planeBrand;
 
}
