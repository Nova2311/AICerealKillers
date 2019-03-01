using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: MonoBehaviour {

    public int teamIDNumber = 1;
    //public int gameSpeedMultiplier = 1;

    public GameObject teamPlacementPanel;

    public void TroopSelected(GameObject selected) {
        Debug.Log(selected.name);

        UnitSquadSpawn.instance.currentSquadSelected = selected;

        /*
        show the placement green/red

        */
    }

    public void ChangeTeam(int TeamID) {
        teamIDNumber = TeamID;
        Debug.Log(TeamID.ToString());
    }

    public void StartGame() {
        Debug.Log("Starting Simulation");
        teamPlacementPanel.SetActive(false);
    }

    public void StopGame() {
        Debug.Log("Stopping Simulation");
        teamPlacementPanel.SetActive(true);
    }

    public void ChangeGameSpeed(int NewSpeed) {


    }
}
