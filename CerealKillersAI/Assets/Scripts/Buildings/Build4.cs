using System.Collections.Generic;

public class Build4 : Building
{
	public Build4()
	{
		units_ = new List<UnitName>
		{
			UnitName.Mage2,
			UnitName.Priest,
			UnitName.Catapult
		};
		name_ = BuildingName.Building4;
	}
}
