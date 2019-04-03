using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Leader_Controller : MonoBehaviour
{
    private Camera cam;
    private A_Star_Pathfinding pathFinding;
    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        pathFinding = GetComponent<A_Star_Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                pathFinding.UsePathFinding(hit.point);
            }
        }
    }
}
