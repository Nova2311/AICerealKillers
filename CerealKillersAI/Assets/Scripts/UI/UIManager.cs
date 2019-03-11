using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour {

	private struct Icon
	{
		public Icon(Image image, Button button)
		{
			img = image;
			btn = button;
		}
		public void Enable()
		{
			img.color = Color.white;
			btn.interactable = true;
		}

		public void Disable()
		{
			img.color = Color.grey;
			btn.interactable = false;
		}
		private Image img;
		private Button btn;
	}

	public int teamIDNumber = 1;
    //public int gameSpeedMultiplier = 1;
    public GameObject teamPlacementPanel;
	public GameObject troop_icons;

	private Team team_selected_;
	private Dictionary<UnitName, Icon> teamred_icon_dict_, teamblue_icon_dict_;
	private BuildingManager build_manager_;

	private void Start()
	{
		team_selected_ = Team.Red;
		build_manager_ = GetComponent<BuildingManager>();
		teamred_icon_dict_ = new Dictionary<UnitName, Icon>();
		teamblue_icon_dict_ = new Dictionary<UnitName, Icon>();
		Image[] images = troop_icons.GetComponentsInChildren<Image>();
		Button[] buttons = troop_icons.GetComponentsInChildren<Button>();
		var units = Enum.GetValues(typeof(UnitName));

		uint index = 0;
		foreach(UnitName a in units)
		{
			Icon icon = new Icon(images[index + 1], buttons[index]);
			if (index > 2) { icon.Disable(); }
			teamred_icon_dict_.Add(a, icon);
			index++;
		}

		index = 0;
		foreach (UnitName a in units)
		{
			Icon icon = new Icon(images[index + 1], buttons[index]);
			if (index > 2) { icon.Disable(); }
			teamblue_icon_dict_.Add(a, icon);
			index++;
		}
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
		build_manager_.AddBuilding(team_selected_, selected.GetComponent<Building>().GetName());
	}

	public void EnableUnit(UnitName name)
	{
		switch (team_selected_)
		{
			case Team.Red:
				teamred_icon_dict_[name].Enable();
				break;
			case Team.Blue:
				teamblue_icon_dict_[name].Enable();
				break;
		}
	}

	public void DisableUnit(UnitName name)
	{
		switch (team_selected_)
		{
			case Team.Red:
				teamred_icon_dict_[name].Disable();
				break;
			case Team.Blue:
				teamblue_icon_dict_[name].Disable();
				break;
		}
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

	private void UpdateUI()
	{

	}
}
