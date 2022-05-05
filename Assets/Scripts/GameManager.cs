using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public int turnNum;
    public int remainingActions;

    public GameObject[] fields;

    public bool DrySeason;
    public int rain;
    public int cash; //Amount
    public int waterLevel; //1 - 100 Percentage
    public int numPollinators; //1 - 100 Percentage
    public int pests; //1 - 100 Percentage

    public TMPro.TextMeshProUGUI statsText;
        
    private void Awake()
    {
        inst = this;
    }

    public void EndTurn()
    {
        turnNum++; //Add Listener for Game Elasped
        //Coroutine while waiting?
        //Disable User during Turn Ending

        //Loan Repayment
        cash -= 50; //Add Listener for Game Elasped

        CalculateSeason();
        CalculateRainfall();
        CalculateNewWaterLevel();
        CalculateSoilQuality();
        CalculateFieldHealth();
        remainingActions = 5;
        //Update Stats Text
        //Invoke GC

        //Reenable user
    }

    void CalculateSeason()
    {
        int season = turnNum % 12;

        if(season < 4 || season > 10) //Check Realworld Data
        {
            DrySeason = true;
        }
        else
        {
            //enable rain Effect
            DrySeason = false;
        }
    }

    void CalculateRainfall()
    {
        if(DrySeason)
        {
            rain = Random.Range(1, 4);
        }
        else
        {
            rain = Random.Range(5, 10);
        }
    }

    void CalculateNewWaterLevel()
    {
        int totalWaterUsed = 0;
        foreach (GameObject field in fields)
        {
            totalWaterUsed += field.GetComponent<FieldProperties>().crop.waterUsedPerTurn;
        }

        waterLevel += (rain - totalWaterUsed);
        waterLevel = Mathf.Clamp(waterLevel, 1, 100);
        //Set Lake Height Transform
    }

    void CalculateSoilQuality()
    {
        foreach(GameObject field in fields)
        {
            field.GetComponent<FieldProperties>().CalculateSoilQuality();
        }
    }

    void CalculateFieldHealth()
    {
        foreach(GameObject field in fields)
        {
            field.GetComponent<FieldProperties>().CalculateFieldHealth();
        }      
    }
}
