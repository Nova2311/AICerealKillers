using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour {

	public int teamIDNumber = 1;
	//public int gameSpeedMultiplier = 1;
	public GameObject teamPlacementPanel;
	public GameObject troop_icons;
	public GameObject build_icons;

	private Team team_selected_;

	private BuildingManager build_manager_;
	private IconStatus icon_status_;

	private void Start()
	{
		team_selected_ = Team.Red;
		build_manager_ = GetComponent<BuildingManager>();
		icon_status_ = new IconStatus(troop_icons, build_icons);
	}

	public void TroopSelected(GameObject selected) {
        Debug.Log(selected.name);

        UnitSquadSpawn.instance.currentSquadSelected = selected;

        /*
        show the placement green/red

        */
    }

	public void BuildingSelected(GameObject selected)
	{
		Debug.Log(selected.name);
		build_manager_.AddBuilding(
			team_selected_,
			selected.GetComponent<Building>().GetName());
	}

	public void EnableUnit(UnitName name)
	{
		icon_status_.SetActive(team_selected_, name, true);
		icon_status_.UpdateUI(team_selected_);
	}

	public void DisableUnit(UnitName name)
	{
		icon_status_.SetActive(team_selected_, name, false);
	}

	public void ChangeTeam(int team) {
		switch (team)
		{
			case 0:
				team_selected_ = Team.Red;
				break;
			case 1:
				team_selected_ = Team.Blue;
				break;
			default:
				Debug.Log("Undefined team in UIManager.ChangeTeam");
				break;
		}
		icon_status_.UpdateUI(team_selected_);
        Debug.Log(team_selected_.ToString());
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
