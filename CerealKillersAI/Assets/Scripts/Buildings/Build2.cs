using System.Collections.Generic;

public class Build2 : Building {

	public Build2()
	{
		units_ = new List<UnitName>
		{
			UnitName.Archer2,
			UnitName.Spear1,
			UnitName.Spear2
		};
		name_ = BuildingName.Building2;
	}
}
