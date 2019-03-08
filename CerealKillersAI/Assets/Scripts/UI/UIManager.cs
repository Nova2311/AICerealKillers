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

	private Dictionary<string, Icon> icon_dict_;

	private void Start()
	{
		icon_dict_ = new Dictionary<string, Icon>();
		Image[] images = troop_icons.GetComponentsInChildren<Image>();
		Button[] buttons = troop_icons.GetComponentsInChildren<Button>();
		for(int i = 0; i < buttons.Length; i++)
		{
			Icon icon = new Icon(images[i + 1], buttons[i]);
			if (i > 2) { icon.Disable(); }
			icon_dict_.Add(buttons[i].name, icon);
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
        
		foreach(string unit in selected.GetComponent<Building>().GetUnits())
		{
			EnableUnit(unit);
		}
	}

	public void EnableUnit(string name)
	{
		icon_dict_[name].Enable();
	}

	public void DisableUnit(string name)
	{
		icon_dict_[name].Disable();
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
