using System.Collections.Generic;

public class Build1 : Building {

	public Build1()
	{
		units_ = new List<UnitName>
		{
			UnitName.HeavySword,
			UnitName.HeavyAxe,
			UnitName.Archer1
		};
		name_ = BuildingName.Building1;
	}
}
