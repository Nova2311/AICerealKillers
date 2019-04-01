using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subordinate_Controller : MonoBehaviour
{
    public float maxLeaderDistance = 3f;
    public Transform leader;
    private A_Star_Pathfinding pathFinding;
    // Use this for initialization
    void Start()
    {
        pathFinding = GetComponent<A_Star_Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(leader.position, transform.position) < maxLeaderDistance)
        {
            // DO FLOCKING STUFF HERE
        }
        else
        {
            pathFinding.UsePathFinding(leader.position);

        }
    }
    private void FixedUpdate()
    {

    }
}
