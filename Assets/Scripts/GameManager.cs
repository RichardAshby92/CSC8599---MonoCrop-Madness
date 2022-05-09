using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    private UIManager uIManager;
    private EconomyManager economyManager;
    private GameSceneManager gameSceneManager;

    public GameObject[] fields;
    public FieldProperties[] fieldProperties;

    public int turnNum;
    public int remainingActions;

    public bool DrySeason;
    public int rain;
    public int cash; //Amount
    public int waterLevel; //1 - 100 Percentage
    public int numPollinators; //1 - 100 Percentage
    public int numPests; //1 - 100 Percentage

    public TMPro.TextMeshProUGUI statsText;
        
    private void Awake()
    {
        inst = this;
        uIManager = GetComponent<UIManager>();
        economyManager = GetComponent<EconomyManager>();
        gameSceneManager = GetComponent<GameSceneManager>();

        for(int i = 0; i < fields.Length; i++)
        {
            fieldProperties[i] = fields[i].GetComponent<FieldProperties>();
        }

    }

    public void EndTurn()
    {
        turnNum++;
        if(turnNum > 120)
        {
            gameSceneManager.LoadEndGame();
        }
        //Coroutine while waiting?
        //Disable User during Turn Ending

        //Loan Repayment
        cash -= 50;
        if(cash <= 0)
        {
            gameSceneManager.LoadEndGame();
        }

        CalculateSeason();
        CalculateRainfall();
        CalculateNewWaterLevel();
        CalculateSoilQuality();
        CalculateFieldHealth();
        GrowCrops();
        SimulateEconomy();
        ResetActions();
        //Update Stats Text

        System.GC.Collect();
        //Reenable user
    }

    void CalculateSeason()
    {
        int season = turnNum % 12;

        if(season < 4 || season > 10) //Check Realworld Data
        {
            //disable rain effect
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

        foreach(FieldProperties fieldProperty in fieldProperties)
        {
            totalWaterUsed += fieldProperty.crop.waterUsedPerTurn;
        }

        waterLevel += (rain - totalWaterUsed);
        waterLevel = Mathf.Clamp(waterLevel, 1, 100);
        //Set Lake Height Transform
    }

    void CalculateSoilQuality()
    {
        foreach(FieldProperties fieldProperty in fieldProperties)
        {
            fieldProperty.CalculateSoilQuality();
        }
    }

    void CalculateFieldHealth()
    {
        foreach(FieldProperties fieldProperty in fieldProperties)
        {
            fieldProperty.CalculateFieldHealth();
        }      
    }

    void GrowCrops()
    {
        foreach(FieldProperties fieldProperty in fieldProperties)
        {
            fieldProperty.GrowCrops();
        }
    }

    void SimulateEconomy()
    {
        economyManager.SimulateEnconomy();
    }

    void ResetActions()
    {
        remainingActions = 5;
        uIManager.ReenableActionButtons();
    }

    public void ActionRemaining()
    {
        remainingActions--;

        if (remainingActions <= 0)
        {
            uIManager.DisableActionButtons();
        }
    }
}
