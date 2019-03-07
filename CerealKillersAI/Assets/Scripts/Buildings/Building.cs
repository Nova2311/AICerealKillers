using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {

	protected List<string> units_;

	public List<string> GetUnits()
	{
		return units_;
	}
}
