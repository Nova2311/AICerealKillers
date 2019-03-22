using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldGathering : MonoBehaviour {

    public int numberOfBlueFlags = 0;
    public int numberOfRedFlags = 0;
    private int bluesGoaldLevel = 0;
    private int redsGoaldLevel = 0;
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
    IEnumerator goldIncrease()
    {
        bluesGoaldLevel = bluesGoaldLevel + blueGoldIncreaseTotal * Time.deltaTime;
        redsGoaldLevel = redsGoaldLevel + redGoldIncreaseTotal * Time.deltaTime;

        redsGoldDisplayText.text = "Red's Gold:" + redsGoaldLevel.ToString();
        blueGoldIncreaseTotal.text = "Blue's Gold:" + bluesGoaldLevel.ToString();
    }
    

}

