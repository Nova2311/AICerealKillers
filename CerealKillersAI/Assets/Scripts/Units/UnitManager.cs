using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    public static UnitManager instance;


    private void Awake() {
        instance = this;
    }
 
}
