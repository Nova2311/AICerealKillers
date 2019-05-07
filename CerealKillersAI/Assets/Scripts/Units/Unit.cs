using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, ISelectHandler, IPointerClickHandler, IDeselectHandler {

    public static HashSet<Unit> allMySelectabkes = new HashSet<Unit>();
    public static HashSet<Unit> currentlySelected = new HashSet<Unit>();

    public GameObject selected;

    public static bool hasLeader = false;
    public bool isLeader = false;


    private void Awake() {
        allMySelectabkes.Add(this);
        selected.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)) {
            DeselectAll(eventData);
        }
        OnSelect(eventData);      
    }

    public void OnSelect(BaseEventData eventData) {
        currentlySelected.Add(this);
        selected.SetActive(true);
        this.gameObject.AddComponent<Subordinate_Controller>();
        FlockingController.instance.AddUnits(this.gameObject);
    }

    public void OnDeselect(BaseEventData eventData) {
        selected.SetActive(false);
        FlockingController.instance.RemoveUnit(this.gameObject);
    }

    public static void DeselectAll(BaseEventData eventData) {
        foreach (Unit selectable in currentlySelected) {
            selectable.OnDeselect(eventData);
            selectable.isLeader = false;

            Destroy(selectable.GetComponent<Leader_Controller>());
            Destroy(selectable.GetComponent<Subordinate_Controller>());
            
        }
        hasLeader = false;
        FlockingController.instance.RemoveAllUnits();
        Debug.Log("Leaders removed");
        currentlySelected.Clear();
    }

    public static void GenerateLeader() {
        GameObject leader = null;
        foreach (Unit unit in currentlySelected) {
            if (!hasLeader) {
                hasLeader = true;
                unit.isLeader = true;
                leader = unit.gameObject;
                Destroy(unit.GetComponent<Subordinate_Controller>());
                unit.gameObject.AddComponent<Leader_Controller>();
            }

            if (!unit.isLeader) {
                unit.GetComponent<Subordinate_Controller>().leader = leader.transform;
                unit.gameObject.AddComponent<FlockBehaviour>();
                unit.GetComponent<FlockBehaviour>().controller = GameObject.Find("Managers").GetComponent<FlockingController>();
            }

        }
    }
}
