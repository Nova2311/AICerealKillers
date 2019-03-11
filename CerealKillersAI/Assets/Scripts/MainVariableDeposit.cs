using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this script is for all the main variables that we all or most of us will have to access and to keep them all orginised
 all in one script instaid of includeing multiple at once we will only need one, and it will make it easyier to modify these
 variables in a rush as they are all together.*/

public enum Team
{
	Red,
	Blue
};

public enum BuildingName
{
	Building1,
	Building2,
	Building3,
	Building4
}

public enum UnitName
{
	Worker,
	LightSword,
	LightAxe,
	HeavySword,
	HeavyAxe,
	Archer1,
	Archer2,
	Spear1,
	Spear2,
	LightCav,
	HeavyCav,
	Mage1,
	Mage2,
	Priest,
	Catapult
}

public class MainVariableDeposit : MonoBehaviour
{
	private int GameSpeed = 1;
	private float GameCountDownTimer = 0;
	private int PlayerGoald = 0;
	private int AIGoald = 0;
}
