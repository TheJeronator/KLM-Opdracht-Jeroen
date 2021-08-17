using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class planeMovement : MonoBehaviour
{
    public bool planeIsParked = false;
    public bool parkingCheck = true;
    public NavMeshAgent planeAI;
    public Vector3 parkingSpot;

    // Pakt de NavMeshAgent van het object.

    private void Start()
    {
        planeAI = GetComponent<NavMeshAgent>();
    }

    /*
     * Hier wordt een willekeurige locatie gegenereerd in een bepaalde radius om het vliegtuig heen.
     * eerst zal een willekeurige vector3 worden gekozen binnen de radius en daarna wordt met de 
     * navmeshhit gecheckt en omgerekend naar een bruikbare value voor de navmeshagent, deze value
     * wordt dan als bestemming gezet.
     */

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    /*
     * Hier wordt een check uitgevoerd om te kijken of het vliegtuig op de juiste positie staat geparkeerd.
     * Ook wordt er de bestemming van de NavMeshAgent gezet naar de locatie is is verkregen in ManagerScript.
     * Verder wordt de randomnavmeshlocation functie hier gecalled met daarin de radius en er wordt gekeken naar
     * of het vliegtuig al is geparkeerd.
     */

    private void Update()
    {
        if (transform.position.x == parkingSpot.x && transform.position.z == parkingSpot.z)
        {
            planeIsParked = true;
        }
        if (managerScript.Instance.isParking)
        {
            planeAI.SetDestination(parkingSpot);
        }
        else if (!planeAI.hasPath || planeAI.isStopped)
        {
            planeAI.SetDestination(RandomNavmeshLocation(8f));
        }
        if (parkingCheck && planeIsParked)
        {
            managerScript.Instance.planesInHangar++;
            parkingCheck = false;
            managerScript.Instance.checkIfParked();
        }
    }

    // Wireframe om de radius van willekeurige bestemmingen zichtbaar te maken.

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 8f);
    }
}
