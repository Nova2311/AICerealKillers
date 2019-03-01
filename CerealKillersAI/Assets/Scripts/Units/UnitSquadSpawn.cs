using UnityEngine;

public class UnitSquadSpawn : MonoBehaviour{
    public static UnitSquadSpawn instance;

    [SerializeField]
    private GameObject[] placeableObjectPrefabs;

    public GameObject PlaceableSquad1;
    public GameObject PlaceableSquad2;

    private bool isReadyToPlaceSquadObject = false;
    private bool spawnPreviw = true;
    public GameObject currentSquadSelected;

    private void Start()
    {
        instance = this;
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

        if (currentSquadSelected == null)
            return;

            if (spawnPreviw == true)
            {
                PlaceableSquad1 = Instantiate(currentSquadSelected);
                spawnPreviw = false;
                isReadyToPlaceSquadObject = true;
            }
        
            //check if were not playing
                


        if (isReadyToPlaceSquadObject == true)
        {
            
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(currentSquadSelected);
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