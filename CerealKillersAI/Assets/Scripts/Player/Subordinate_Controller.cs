using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subordinate_Controller : MonoBehaviour
{
    public float maxLeaderDistance = 3f;

    public Transform leader;
    private A_Star_Pathfinding pathFinding;

    public bool startMoving = false;

    public Vector3 newPosition;
    // Use this for initialization
    void Start()
    {
        pathFinding = GetComponent<A_Star_Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
            //FlockingController.instance.BuildGridPattern();
        //pathFinding.UsePathFinding(FlockingController.instance.UnitPosition(leader.position, this.gameObject));

        //if (Vector3.Distance(leader.position, transform.position) < maxLeaderDistance)
        //{
        // DO FLOCKING STUFF HERE
        //}
        //else
        //{
        //}
    }
    private void FixedUpdate()
    {

    }
}
