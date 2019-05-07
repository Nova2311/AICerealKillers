using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class FlockBehaviour : MonoBehaviour
{
    // Reference to the controller.
    public FlockingController controller;
    private float timer;

    void Start()
    {
        timer = controller.wanderTimer;

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= controller.wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, controller.wanderRadius, -1);
            gameObject.GetComponent<Subordinate_Controller>().pathFinding.UsePathFinding(newPos);
            timer = 0;
        }

        var nearbyUnits = Physics.OverlapSphere(this.gameObject.transform.position, controller.neighborDist, controller.searchLayer);
        foreach (var unit in nearbyUnits) {
            if (unit.gameObject != gameObject) continue;
            
            Vector3 newPos = RandomNavSphere(transform.position, controller.wanderRadius, -1);
            gameObject.GetComponent<Subordinate_Controller>().pathFinding.UsePathFinding(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        //Finds a position to walk to
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
