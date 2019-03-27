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
    }

    public void OnDeselect(BaseEventData eventData) {
        selected.SetActive(false);
    }

    public static void DeselectAll(BaseEventData eventData) {
        foreach (Unit selectable in currentlySelected) {
            selectable.OnDeselect(eventData);
            selectable.isLeader = false;
        }
        hasLeader = false;

        Debug.Log("Leaders removed");
        currentlySelected.Clear();
    }

    public static void GenerateLeader() {
        foreach (Unit unit in currentlySelected) {
            if (!hasLeader) {
                hasLeader = true;
                unit.isLeader = true;
                Debug.Log(unit.gameObject.name);
                return;
            }
        }
    }
}
