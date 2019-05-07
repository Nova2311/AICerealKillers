using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subordinate_Controller : MonoBehaviour
{
    public float maxLeaderDistance = 10f;

    public Transform leader;
    public A_Star_Pathfinding pathFinding;

    public bool startMoving = false;

    public Vector3 newPosition;
    public bool startFlock = false;

    // Use this for initialization
    void Start()
    {
        pathFinding = GetComponent<A_Star_Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(leader.position, transform.position) < maxLeaderDistance) {
            //DO FLOCKING STUFF HERE
            startFlock = true;
        } else {
            //move towards the leader
            startFlock = false;
            pathFinding.UsePathFinding(leader.position);
        }
    }
    private void FixedUpdate()
    {

    }
}
