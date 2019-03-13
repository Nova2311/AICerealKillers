using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconStatus {

	private struct Icon
	{
		public Icon(Image image, Button button)
		{
			img = image;
			btn = button;
		}

		public void SetActive(bool status)
		{
			if (status) { Enable(); }
			else { Disable(); }
		}

		private void Enable()
		{
			img.color = Color.white;
			btn.interactable = true;
		}

		private void Disable()
		{
			img.color = Color.grey;
			btn.interactable = false;
		}
		private Image img;
		private Button btn;
	}

	private struct TeamIconStatus
	{
		public TeamIconStatus(Array units, Array buildings)
		{
			unit_icon_status_ = new Dictionary<UnitName, bool>();
			foreach (UnitName name in units)
			{
				unit_icon_status_.Add(name, false);
			}
			unit_icon_status_[UnitName.Worker] = true;
			unit_icon_status_[UnitName.LightSword] = true;
			unit_icon_status_[UnitName.LightAxe] = true;

			build_icon_status_ = new Dictionary<BuildingName, bool>();
			foreach (BuildingName name in buildings)
			{
				build_icon_status_.Add(name, true);
			}
			units_ = units;
			buildings_ = buildings;
		}

		public void UpdateUI(
			ref Dictionary<UnitName, Icon> unit_icons,
			ref Dictionary<BuildingName, Icon> build_icons)
		{
			foreach (UnitName name in units_)
			{
				unit_icons[name].SetActive(unit_icon_status_[name]);
			}

			foreach (BuildingName name in buildings_)
			{
				build_icons[name].SetActive(build_icon_status_[name]);
			}
		}

		public void SetActive(UnitName name, bool status)
		{
			unit_icon_status_[name] = status;
		}

		public void SetActive(BuildingName name, bool status)
		{
			build_icon_status_[name] = status;
		}
		private Dictionary<UnitName, bool> unit_icon_status_;
		private Dictionary<BuildingName, bool> build_icon_status_;
		private Array units_, buildings_;
	}

	//UI icon references
	private Dictionary<UnitName, Icon> unit_icon_dict_;
	private Dictionary<BuildingName, Icon> build_icon_dict_;

	//Team enabled/disabled icon tracker
	private TeamIconStatus red_status_, blue_status_;

	public IconStatus(GameObject unit_icons, GameObject building_icons)
	{
		//Fill unit icons
		unit_icon_dict_ = new Dictionary<UnitName, Icon>();
		Image[] unit_images = unit_icons.GetComponentsInChildren<Image>();
		Button[] unit_buttons = unit_icons.GetComponentsInChildren<Button>();
		var units = Enum.GetValues(typeof(UnitName));

		uint index = 0;
		foreach (UnitName name in units)
		{
			Icon icon = new Icon(unit_images[index + 1], unit_buttons[index]);
			unit_icon_dict_.Add(name, icon);
			index++;
		}

		//Fill building icons
		build_icon_dict_ = new Dictionary<BuildingName, Icon>();
		Image[] build_images = building_icons.GetComponentsInChildren<Image>();
		Button[] build_buttons = building_icons.GetComponentsInChildren<Button>();
		var buildings = Enum.GetValues(typeof(BuildingName));

		index = 0;
		foreach (BuildingName name in buildings)
		{
			Icon icon = new Icon(build_images[index + 1], build_buttons[index]);
			build_icon_dict_.Add(name, icon);
			index++;
		}

		//Build team icon reference
		red_status_ = new TeamIconStatus(units, buildings);
		red_status_.SetActive(BuildingName.Building1, false);
		blue_status_ = new TeamIconStatus(units, buildings);
		UpdateUI(Team.Red);
	}

	public void UpdateUI(Team team)
	{
		switch (team)
		{
			case Team.Red:
				red_status_.UpdateUI(ref unit_icon_dict_, ref build_icon_dict_);
				break;
			case Team.Blue:
				blue_status_.UpdateUI(ref unit_icon_dict_, ref build_icon_dict_);
				break;
			default:
				Debug.Log("Undefined team in IconStatus.UpdateUI");
				break;
		}
	}

	public void SetActive(Team team, UnitName name, bool status)
	{
		switch (team)
		{
			case Team.Red:
				red_status_.SetActive(name, status);
				break;
			case Team.Blue:
				blue_status_.SetActive(name, status);
				break;
			default:
				Debug.Log("Undefined team in IconStatus.SetActive");
				break;
		}
	}

	public void SetActive(Team team, BuildingName name, bool status)
	{
		switch (team)
		{
			case Team.Red:
				red_status_.SetActive(name, status);
				break;
			case Team.Blue:
				blue_status_.SetActive(name, status);
				break;
			default:
				Debug.Log("Undefined team in IconStatus.SetActive");
				break;
		}
	}
}
