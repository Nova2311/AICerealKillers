using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour {

    public float blueCaptureScore = 0;
    public float redCaptureScore = 0;
    private float coolDown = 3;

    private bool blueOnPoint = false;
    private bool redOnPoint = false;
    public bool contested = false;

    public bool neutral = true;
    public bool redControlled = false;
    public bool blueControlled = false;


    public GameObject flag;
    public int CaptureVariable = 10;
    public Color redTeam;
    public Color blueTeam;
    public Color neutralColour;

    private void Start() {
        flag.GetComponent<Renderer>().material.color = neutralColour;
    }

    void Update(){

        if (blueOnPoint && redOnPoint) {
            contested = true;
        } else {
            contested = false;
        }

        if (blueCaptureScore > 100) {
            blueCaptureScore = 100;
        }
        if (redCaptureScore > 100) {
            redCaptureScore = 100;
        }

        if (blueCaptureScore < 0) {
            blueCaptureScore = 0;
            blueControlled = false;
            neutral = true;
            flag.GetComponent<Renderer>().material.color = neutralColour;
        }
        if (redCaptureScore < 0) {
            redCaptureScore = 0;
            redControlled = false;
            neutral = true;
            flag.GetComponent<Renderer>().material.color = neutralColour;
        }

        if (BlueCapturedPoint()) {
            blueControlled = true;
            neutral = false;
            flag.GetComponent<Renderer>().material.color = blueTeam;
        }
        if (RedCapturedPoint()) {
            redControlled = true;
            neutral = false;
            flag.GetComponent<Renderer>().material.color = redTeam;
        }
    }

    void OnTriggerStay(Collider col){
        if (col.gameObject.tag == "Blue") {
            blueOnPoint = true;
        }
        if (col.gameObject.tag == "Red") {
            redOnPoint = true;
        }
          
        if (contested == true) // to check if both the team is in the circle if so the captureing stops until one team is eliminated
            return;
            if (col.gameObject.tag == "Blue"){
                if (neutral == true && redCaptureScore == 0f) {
                    blueCaptureScore = blueCaptureScore + CaptureVariable * Time.deltaTime;
                } else if (redControlled == true || redCaptureScore != 0f) {
                    redCaptureScore = redCaptureScore - Time.deltaTime * CaptureVariable;
                   
                }
            }

            if (col.gameObject.tag == "Red"){
                if (neutral == true && blueCaptureScore == 0f) {
                    redCaptureScore = redCaptureScore +  CaptureVariable * Time.deltaTime;
                } else if (blueControlled == true || blueCaptureScore != 0f) {
                    blueCaptureScore = blueCaptureScore -= CaptureVariable * Time.deltaTime;
                    
                }
            }
    }

    private void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Blue") {
            blueOnPoint = false;
        }
        if (col.gameObject.tag == "Red") {
            redOnPoint = false;
        }
    }

    bool BlueCapturedPoint() {
        if (blueCaptureScore == 100 && !contested) {
            return true;
        } else {
            return false;
        }
    }

    bool RedCapturedPoint() {
        if (redCaptureScore == 100 && !contested) {
            return true;
        } else {
            return false;
        }
    }

}
