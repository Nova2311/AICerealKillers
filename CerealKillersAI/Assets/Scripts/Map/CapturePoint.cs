using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour {

    public float CaptureScore = 0;
    private float coolDown = 3;
    private bool tied = false;

    private bool nutrual = true;
    private bool red = false;
    private bool blue = false;


    public GameObject flag;
    public int CaptureVariable = 10;

    void Start()
    {

    }

    void Update()
    {
        if (CaptureScore > 100)
            CaptureScore = 100;

        if (CaptureScore < 0)
            CaptureScore = 0;
    }

    void OnTriggerEnter(Collider col)
    {
      /* if (col.gameObject.tag == "Blue" && col.gameObject.tag == "Red")
        {
            tied = true;
        }
        else tied = false;
        */

        if (tied == false) // to check if both the team is in the circle if so the captureing stops until one team is eliminated
         {
            Debug.Log("wut");
            if (col.gameObject.tag == "Blue")
            {
                Debug.Log("wutt");
                if (nutrual == true)
                {
                    CaptureScore = CaptureScore + Time.deltaTime * CaptureVariable;
                    Debug.Log("wuttt");
                }
                else if (red == true)
                { CaptureScore = CaptureScore - Time.deltaTime * CaptureVariable; }
                else if (blue == true)
                { CaptureScore = CaptureScore + Time.deltaTime * CaptureVariable; }
            }

            if (col.gameObject.tag == "Red")
            {
                if (nutrual == true)
                { CaptureScore = CaptureScore + Time.deltaTime * CaptureVariable; }
                else if (red == true)
                { CaptureScore = CaptureScore + Time.deltaTime * CaptureVariable; }
                else if (blue == true)
                { CaptureScore = CaptureScore - Time.deltaTime * CaptureVariable; }
            }
        }

        if (col.gameObject.tag == "Blue" && CaptureScore == 100 && nutrual == true)
        {
            nutrual = false;
            blue = true;
            Debug.Log("blue has captured a point");
        }
        if (col.gameObject.tag == "Red" && CaptureScore == 100 && nutrual == true)
        {
            nutrual = false;
            red = true;
            Debug.Log("red has captured a point");
        }
        if (col.gameObject.tag == "Blue" && CaptureScore == 0 && nutrual == true)
        {
            nutrual = false;
            red = false;
        }
        if (col.gameObject.tag == "Red" && CaptureScore == 0 && nutrual == true)
        {
            nutrual = true;
            blue = false;
        }
    }
}
