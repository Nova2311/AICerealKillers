﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_Star_Pathfinding : MonoBehaviour
{
    private Camera cam;
    private Transform seeker;

    private Vector3 target;
    private Grid grid;
    private Transform terrain;

    public float speed = 3;

    void Awake()
    {
        //grid = GetComponent<Grid> ();
        seeker = transform;
        target = transform.position;
        cam = Camera.main;
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
        terrain = GameObject.FindWithTag("Terrain").transform;


    }

    void Update()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         target = hit.point;

        //     }
        // }
        FindPath(target);

    }
    public void UsePathFinding(Vector3 destination)
    {
        target = destination;
    }
    private void FindPath(Vector3 targetPos)
    {

        Vector3 startPos = seeker.position;
        target = targetPos;
        Node startNode = grid.NodeFromWorldPoint(startPos - terrain.position);
        Node targetNode = grid.NodeFromWorldPoint(targetPos - terrain.position);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
        if (path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[0].worldPosition, Time.deltaTime * speed);

        }

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
