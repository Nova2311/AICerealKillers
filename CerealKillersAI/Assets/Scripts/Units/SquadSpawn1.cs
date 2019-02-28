using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSpawn1 : MonoBehaviour {
    // [SerializeField]
    // private GameObject[] placeableObjectPrefabs = PlaceableSquad;

    //Squad_Prefabs/Squad1

    private GameObject SelectedSquad = GameObject.Find("Infantry squad");            // Resources.Load("Squad_Prefabs/Squad1", GameObject) as GameObject;


    //  private GameObject variableForPrefab = (GameObject) Resources.Load("prefabs/prefab1", typeof(GameObject));
    private bool isReadyToPlaceSquadObject = false;
    private bool spawnPreviw = true;
    private GameObject currentSquadSelected;

    private void Start()
    {
        Debug.Log("it worked");
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
            if (spawnPreviw == true)
            {
                spawnPreviw = false;
                currentSquadSelected = SelectedSquad;
                isReadyToPlaceSquadObject = true;
            }
        



        if (isReadyToPlaceSquadObject == true)
        {
           
                if (Input.GetMouseButtonDown(0))
                {
                    currentSquadSelected = SelectedSquad;
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