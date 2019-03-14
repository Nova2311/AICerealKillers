using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldGathering : MonoBehaviour {

    public int numberOfBlueFlags = 0;
    public int numberOfRedFlags = 0;
    private int bluesGoldLevel = 0;
    private int redsGoldLevel = 0;
    private int rateOfGoldIncreaseBase = 2;
    public Text redsGoldDisplayText;
    public Text bluesGoldDisplayText;

    public int blueGoldIncreaseTotal;
    public int redGoldIncreaseTotal;


    // Use this for initialization
    void Start () {
        blueGoldIncreaseTotal = rateOfGoldIncreaseBase * numberOfBlueFlags;
        redGoldIncreaseTotal = rateOfGoldIncreaseBase * numberOfRedFlags;
    }
	
	// Update is called once per frame
	void Update () {

        blueGoldIncreaseTotal = rateOfGoldIncreaseBase * numberOfBlueFlags;
        redGoldIncreaseTotal = rateOfGoldIncreaseBase * numberOfRedFlags;

        StartCoroutine(goldIncrease());

    }

    IEnumerator goldIncrease(){
        bluesGoldLevel = bluesGoldLevel + blueGoldIncreaseTotal;
        redsGoldLevel = redsGoldLevel + redGoldIncreaseTotal;

        redsGoldDisplayText.text = "Red's Gold:" + redsGoldLevel.ToString();
        bluesGoldDisplayText.text = "Blue's Gold:" + bluesGoldLevel.ToString();
        yield return new WaitForSeconds(2);
    }
    
}

