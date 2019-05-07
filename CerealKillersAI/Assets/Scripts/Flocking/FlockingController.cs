using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingController : MonoBehaviour {

    public static FlockingController instance;

    public List<GameObject> units = new List<GameObject>();

    public Vector3[] positions;
    public GameObject Leader;
    public int currentUnits = 0;

    public float wanderRadius;
    public float wanderTimer;

    [Range(0.1f, 10.0f)]
    public float neighborDist = 0.5f;

    public LayerMask searchLayer = 11;

    private void Start(){
        instance = this;
    }

    public void AddUnits(GameObject unit) {
        units.Add(unit.gameObject);
        currentUnits++;
    }

    public void RemoveUnit(GameObject unit) {
        for (int i = 0; i < units.Count; i++){
            if (units[i] == unit) {
                units.RemoveAt(i);
                break;
            }
        }
        currentUnits--;
    }

    public void RemoveAllUnits() {
        units.Clear();
        currentUnits = 0;
    }

}
