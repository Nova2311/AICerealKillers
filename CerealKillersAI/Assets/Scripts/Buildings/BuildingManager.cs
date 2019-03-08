using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public enum Team
    {
        One,
        Two
    }

    public enum BuildingName
    {
        Building1,
        Building2,
        Building3,
        Building4
    }

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
        Dictionary<BuildingName, Dictionary<string, GameObject>> selected_team;
        GameObject instance;
        switch (team)
        {
            case Team.One:
                selected_team = team1_buildings_;
                switch (name)
                {
                    case BuildingName.Building1:
                        instance = Instantiate(build1_prefab);
                        selected_team[name].Add(instance.name, instance);
                        break;
                    case BuildingName.Building2:
                        instance = Instantiate(build2_prefab);
                        selected_team[name].Add(instance.name, instance);
                        break;
                    case BuildingName.Building3:
                        instance = Instantiate(build3_prefab);
                        selected_team[name].Add(instance.name, instance);
                        break;
                    case BuildingName.Building4:
                        instance = Instantiate(build4_prefab);
                        selected_team[name].Add(instance.name, instance);
                        break;
                }
                break;
            case Team.Two:
                selected_team = team2_buildings_;
                switch (name)
                {
                    case BuildingName.Building1:
                        instance = Instantiate(build1_prefab);
                        selected_team[name].Add(instance.name, instance);
                        break;
                    case BuildingName.Building2:
                        instance = Instantiate(build2_prefab);
                        selected_team[name].Add(instance.name, instance);
                        break;
                    case BuildingName.Building3:
                        instance = Instantiate(build3_prefab);
                        selected_team[name].Add(instance.name, instance);
                        break;
                    case BuildingName.Building4:
                        instance = Instantiate(build4_prefab);
                        selected_team[name].Add(instance.name, instance);
                        break;
                }
                break;
        }
    }
}
