using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RandomTaunt : MonoBehaviour
{
    [Header("Genetic Algorithm")]
    [SerializeField] string targetString = "Ryan is the best";
    [SerializeField] string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
    [SerializeField] int populationSize = 200;
    [SerializeField] float mutationRate = 0.01f;
    [SerializeField] int elitism = 5;

    [Header("Other")]
    [SerializeField] int numCharsPerText = 15000;
    [SerializeField] Text targetText;
    [SerializeField] Text bestText;
    [SerializeField] Text bestFitnessText;
    [SerializeField] Text numGenerationsText;
    [SerializeField] Transform populationTextParent;
    [SerializeField] Text textPrefab;
    [SerializeField] GameObject scrolView;

    //public String RandTaunt1 = "";
    //public String RandTaunt2 = "";
    //public String RandTaunt3 = "";
    //public String RandTaunt4 = "";
    //public String RandTaunt5 = "";

    [SerializeField] string tauntString = "";
    string[] taunts;

    float tauntTimer = 10f;
    bool startagain = false;

    string genholder;

    int Duration = 5;
    int TauntDuration = 3;
    int TauntSelect;



    private GeneticAlgorithm<char> ga;
    private System.Random random;

    void Start()
    {
        ResetPopUpText();
        taunts = tauntString.Split('-');
        

        targetText.text = targetString;

        //if (string.IsNullOrEmpty(targetString))
        //{
        //    Debug.LogError("Target string is null or empty");
        //}

        random = new System.Random();
        ga = new GeneticAlgorithm<char>(populationSize, targetString.Length, random, GetRandomCharacter, FitnessFunction, elitism, mutationRate);

        //starts the randon taunt system
       // ResetPopUpText();
       // StartCoroutine(RandomNumberGenerator());
    }

    void Update()
    {
        tauntTimer -= Time.deltaTime;
       // this.enabled = true;

        if (tauntTimer <= 0) {
            int rand = UnityEngine.Random.Range(0, taunts.Length);
            tauntTimer -= Time.deltaTime;
            Debug.Log(rand);
            switch (rand)
            {
                case 0:
                    targetString = taunts[0];

                    startPopUpText();
                    ga = new GeneticAlgorithm<char>(populationSize, targetString.Length, random, GetRandomCharacter, FitnessFunction, elitism, mutationRate);
                    break;
                case 1:
                    targetString = taunts[1];

                    startPopUpText();
                    ga = new GeneticAlgorithm<char>(populationSize, targetString.Length, random, GetRandomCharacter, FitnessFunction, elitism, mutationRate);

                    break;
                case 2:
                    targetString = taunts[2];

                    startPopUpText();
                    ga = new GeneticAlgorithm<char>(populationSize, targetString.Length, random, GetRandomCharacter, FitnessFunction, elitism, mutationRate);

                    break;

            }
            tauntTimer = 100f;

        }

        ga.NewGeneration();

        UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation, ga.Population.Count, (j) => ga.Population[j].Genes);



        if (bestText.text == targetString)
        {
            startagain = false;
            StartCoroutine(CountDownTillTaskDisapears());
           
        }
        
    }

    private char GetRandomCharacter()
    {
        int i = random.Next(validCharacters.Length);
        return validCharacters[i];
    }

    private float FitnessFunction(int index)
    {
        float score = 0;
        DNA<char> dna = ga.Population[index];

        for (int i = 0; i < dna.Genes.Length; i++)
        {
            if (dna.Genes[i] == targetString[i])
            {
                score += 1;
            }
        }

        score /= targetString.Length;

        score = (Mathf.Pow(2, score) - 1) / (2 - 1);

        return score;
    }



    private int numCharsPerTextObj;
    private List<Text> textList = new List<Text>();

    void Awake()
    {
        numCharsPerTextObj = numCharsPerText / validCharacters.Length;
        if (numCharsPerTextObj > populationSize) numCharsPerTextObj = populationSize;

        int numTextObjects = Mathf.CeilToInt((float)populationSize / numCharsPerTextObj);

        for (int i = 0; i < numTextObjects; i++)
        {

                textList.Add(Instantiate(textPrefab, populationTextParent));
            
        }
    }

    private void UpdateText(char[] bestGenes, float bestFitness, int generation, int populationSize, Func<int, char[]> getGenes)
    {
        bestText.text = CharArrayToString(bestGenes);
        bestFitnessText.text = bestFitness.ToString();

        if (startagain)

        {
            numGenerationsText.text = "Number of generations: :" + generation.ToString();
        }

        for (int i = 0; i < textList.Count; i++)
        {
            var sb = new StringBuilder();
            int endIndex = i == textList.Count - 1 ? populationSize : (i + 1) * numCharsPerTextObj;
            for (int j = i * numCharsPerTextObj; j < endIndex; j++)
            {
                foreach (var c in getGenes(j))
                {
                    sb.Append(c);
                }
                if (j < endIndex - 1) sb.AppendLine();
            }
            if (startagain)
            {
                textList[i].text = sb.ToString();
            }
        }
    }

    private string CharArrayToString(char[] charArray)
    {
        var sb = new StringBuilder();
        foreach (var c in charArray)
        {
            sb.Append(c);
        }

        return sb.ToString();
    }

    private void ResetPopUpText()
    {
        bestText.gameObject.SetActive(false);
        scrolView.gameObject.SetActive(false);
        numGenerationsText.gameObject.SetActive(false);
        targetText.gameObject.SetActive(false);
        targetString = "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee" +
            "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";
        tauntTimer = 10f;

    }

    private void startPopUpText()
    {
        bestText.gameObject.SetActive(true);
        scrolView.gameObject.SetActive(true);
        numGenerationsText.gameObject.SetActive(true);
        targetText.gameObject.SetActive(false);
        startagain = true;
        tauntTimer = 100f;
    }

   System.Collections.IEnumerator CountDownTillTaskDisapears()
    {
        yield return new WaitForSeconds(3);
        ResetPopUpText();
    }
}



