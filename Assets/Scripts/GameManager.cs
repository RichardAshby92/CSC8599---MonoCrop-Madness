using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent onBankrupt;
    public UnityEvent onTurnsElasped;
    public UnityEvent actionsElasped;

        
    private void Awake()
    {
        inst = this;
    }

    public void EndTurn()
    {
        turnNum++;
        if(turnNum > 120)
        {
            onTurnsElasped.Invoke();
        }
        //Coroutine while waiting?
        //Disable User during Turn Ending

        //Loan Repayment
        cash -= 50;
        if(cash <= 0)
        {
            onBankrupt.Invoke();
        }

        CalculateSeason();
        CalculateRainfall();
        CalculateNewWaterLevel();
        CalculateSoilQuality();
        CalculateFieldHealth();
        //Grow Crops
        //ResetAction()
        remainingActions = 5;
        //Update Stats Text

        System.GC.Collect();
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

    void GrowCrops()
    {
        foreach(GameObject field in fields)
        {
            //GrowCropFunction
        }
    }

    public void ActionRemaining()
    {
        remainingActions--;

        if (remainingActions <= 0)
        {
            actionsElasped.Invoke();
        }
    }
}
