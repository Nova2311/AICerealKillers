using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingController : MonoBehaviour {

    public static FlockingController instance;

    public int xSize = 5;
    public int zSize;

    public int offset = 3;

    public List<GameObject> units = new List<GameObject>();

    public Vector3[] positions;
    public GameObject Leader;
    public int currentUnits = 0;
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

    public void BuildGridPattern() {
        zSize = (currentUnits / xSize) +1;//How many rows do we need
        
        for (int i = 0; i < units.Count; i++){
            if (units[i].GetComponent<Unit>().isLeader) {
                Leader = units[i];

                return;
            }
        }

        for (int i = 0; i < units.Count; i++){
            if (!units[i].GetComponent<Unit>().isLeader){
                Vector3 leaderPos = Leader.transform.position;
                //units[i].transform.position = new Vector3(leaderPos.x -= offset, leaderPos.y, leaderPos.z);
            }
        }
        Debug.Log(zSize);
    }

    public Vector3 UnitPosition(Vector3 LeaderPos, GameObject unit) {
        for (int i = 1; i < units.Count; i++){
            if (units[i] == unit) {
                if (i <= 2) {
                    units[i].transform.position = new Vector3(LeaderPos.x -= (i*offset), LeaderPos.y, LeaderPos.z);
                }
                if (unit.GetComponent<Unit>().isLeader) {
                    return LeaderPos;
                }
                if (i > 2 && i <= 4) {
                    units[i].transform.position = new Vector3(LeaderPos.x += offset, LeaderPos.y, LeaderPos.z);
                }
            }
        }

        return unit.transform.position;
    }

}
