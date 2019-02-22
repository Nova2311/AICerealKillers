using System;
using UnityEngine;

public class UnitSquadSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    private void Start()
    {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
            currentPrefabIndex = i;
        }
    }

    private void Update()
    {
        HandleNewObject();
        ChechForDeselect();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
        }
    }

    private void ChechForDeselect()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //end the selection for this unit type
            Destroy(currentPlaceableObject);
            currentPlaceableObject = null;
            return;
        }
    }

    private void HandleNewObject()
    {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if (Input.GetMouseButtonDown(0))
            {  
                    currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
                    currentPrefabIndex = i;             
            }
        }
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }
}