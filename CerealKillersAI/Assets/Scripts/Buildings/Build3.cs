using System.Collections.Generic;

public class Build3 : Building
{
	public Build3()
	{
		units_ = new List<UnitName>
		{
			UnitName.LightCav,
			UnitName.HeavyCav,
			UnitName.Mage1
		};
		name_ = BuildingName.Building3;
	}
}
