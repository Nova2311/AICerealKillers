using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public GameObject
        build1_prefab, build2_prefab,
        build3_prefab, build4_prefab;

    private Dictionary<BuildingName, Dictionary<string, GameObject>>
        team1_buildings_, team2_buildings_;
    private UIManager ui_manager_;

    private void Start()
    {
        team1_buildings_ = new Dictionary<BuildingName, Dictionary<string, GameObject>>
        {
            { BuildingName.Building1, new Dictionary<string, GameObject>() },
            { BuildingName.Building2, new Dictionary<string, GameObject>() },
            { BuildingName.Building3, new Dictionary<string, GameObject>() },
            { BuildingName.Building4, new Dictionary<string, GameObject>() }
        };
        team2_buildings_ = new Dictionary<BuildingName, Dictionary<string, GameObject>>
        {
            { BuildingName.Building1, new Dictionary<string, GameObject>() },
            { BuildingName.Building2, new Dictionary<string, GameObject>() },
            { BuildingName.Building3, new Dictionary<string, GameObject>() },
            { BuildingName.Building4, new Dictionary<string, GameObject>() }
        };
        ui_manager_ = GetComponent<UIManager>();
    }

    public void AddBuilding(Team team, BuildingName name)
    {
		switch (team)
		{
			case Team.Red:
				InstantiateBuilding(ref team1_buildings_, name);
				break;
			case Team.Blue:
				InstantiateBuilding(ref team2_buildings_, name);
				break;
		}
    }

	private void InstantiateBuilding(
		ref Dictionary<BuildingName, Dictionary<string, GameObject>> team,
		BuildingName name)
	{
		GameObject instance;
		switch (name)
		{
			case BuildingName.Building1:
				instance = Instantiate(build1_prefab);
				break;
			case BuildingName.Building2:
				instance = Instantiate(build2_prefab);
				break;
			case BuildingName.Building3:
				instance = Instantiate(build3_prefab);
				break;
			case BuildingName.Building4:
				instance = Instantiate(build4_prefab);
				break;
			default:
				Debug.Log("Big problem happened, BuildingManager.InstantiateBuilding");
				instance = null;
				break;
		}
		team[name].Add(instance.name, instance);
		instance.GetComponent<Building>().EnableUnits(ref ui_manager_);
		ui_manager_.SetIconActive(name, false);
	}
}
