using System;
using UnityEngine;

public class UnitSquadSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;

    public GameObject PlaceableSquad1;
    public GameObject PlaceableSquad2;

    private int currentPrefabIndex = -1;
    private bool isReadyToPlaceSquadObject = false;
    private bool spawnPreviw = true;
    private GameObject currentSquadSelected;

    private void Start()
    {
       
    }

    private void Update()
    {
        HandleNewObject();
        ChechForDeselect();

        if (currentSquadSelected != null)
        {
            MoveCurrentObjectToMouse();
        }
    }

    private void ChechForDeselect()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isReadyToPlaceSquadObject = false;
            spawnPreviw = true;
            //end the selection for this unit type
            Destroy(currentSquadSelected);
            currentSquadSelected = null;
            return;
        }
    }

    private void HandleNewObject()
    {
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            for (int i = 0; i < placeableObjectPrefabs.Length; i++)
            {
                if (spawnPreviw == true)
                {
                    PlaceableSquad1 = Instantiate(placeableObjectPrefabs[i]);
                    currentPrefabIndex = i;
                    spawnPreviw = false;
                    currentSquadSelected = PlaceableSquad1;
                    isReadyToPlaceSquadObject = true;
                }
            }
  
        }

        if (isReadyToPlaceSquadObject == true)
        {
            for (int i = 0; i < placeableObjectPrefabs.Length; i++)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentSquadSelected = Instantiate(placeableObjectPrefabs[i]);
                    currentPrefabIndex = i;
                }
            }
        }

    }
    

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentSquadSelected.transform.position = hitInfo.point;
            
        }
    }
}