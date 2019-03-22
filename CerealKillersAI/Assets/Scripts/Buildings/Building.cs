using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {

	protected List<UnitName> units_;
	protected BuildingName name_;

	public void EnableUnits(ref UIManager manager)
	{
		foreach(UnitName unit in units_)
		{
			manager.SetIconActive(unit, true);
		}
	}

	public BuildingName GetName()
	{
		return name_;
	}
}
