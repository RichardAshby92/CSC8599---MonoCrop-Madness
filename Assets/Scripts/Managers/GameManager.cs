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
    private CommunityManager communityManager;
    private ImprovementsManager Improvements;
    private List<FieldProperties> fieldProperties;

    [SerializeField]
    private GameObject Lake;
    [SerializeField]
    private GameObject Information; //Wrong PLace
    [SerializeField]
    private GameObject[] fields;

    [field:SerializeField]
    public int turnNum { set;  get; }
    [field:SerializeField]
    public int remainingActions { set; get; }
    [field:SerializeField]
    public bool DrySeason { set; get; }
    [field:SerializeField]
    public int rain { set;  get; }
    [field:SerializeField]
    public int cash { get; set; }
    [field:SerializeField]
    public int waterLevel { get; set; } //1 - 100 Percentage
    [field:SerializeField]
    public int numPollinators { get; set; } //1 - 100 Percentage
    [field:SerializeField]
    public int[] numPests { get; set; } //1 - 100 Percentage

    [SerializeField]
    private int loanRepayment;

    private void Awake()
    {
        inst = this;
        uIManager = GetComponent<UIManager>();
        economyManager = GetComponent<EconomyManager>();
        gameSceneManager = GetComponent<GameSceneManager>();
        communityManager = GetComponent<CommunityManager>();
        Improvements = GetComponent<ImprovementsManager>();

        fieldProperties = new List<FieldProperties>();

        for (int i = 0; i < fields.Length; i++)
        {
            fieldProperties.Add(fields[i].GetComponent<FieldProperties>());
        }
    }
       

    public void EndTurn()
    {
        turnNum++;
        if(turnNum > 120)
        {
            gameSceneManager.LoadEndGame();
            return;
        }

        cash -= loanRepayment;

        if(cash <= 0)
        {
            gameSceneManager.LoadEndGame();
            return;
        }

        CalculateSeason();
        CalculateRainfall();
        CalculateNewWaterLevel();
        CalculateSoilQuality();
        CalculateFieldHealth();
        GrowCrops();
        CalculatePests();
        economyManager.SimulateEnconomy();
        ResetActions();
        uIManager.UpdateUIText();
        uIManager.UpdateCropPriceDisplay();
        Information.SetActive(true);
        communityManager.CheckHealth();
        Improvements.ResearchImprovement();
        //Update Stats Text

        System.GC.Collect();
    }

    void CalculateSeason()
    {
        int season = turnNum % 12;

        if(season < 4 || season > 10)
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
            rain = Random.Range(1, 10);
        }
        else
        {
            rain = Random.Range(8, 20);
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
        Vector3 waterHeight = Lake.transform.position;
        waterHeight.y = waterLevel;

        Lake.transform.position = waterHeight;
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

    void CalculatePests()
    {
        foreach(FieldProperties fieldProperty in fieldProperties)
        {
            fieldProperty.CalculatePests();
        }
    }

    void ResetActions()
    {
        remainingActions = 4;
        uIManager.ReenableActionButtons();
    }

    public bool ActionRemaining(int actionsTaken)
    {
        if((remainingActions - actionsTaken) < 0)
        {
            return false;
        }

        remainingActions = remainingActions - actionsTaken;

        if (remainingActions <= 0)
        {
            uIManager.DisableActionButtons();
        }

        return true;
    }

    public void AddField(GameObject newField)
    {
        fieldProperties.Add(newField.GetComponent<FieldProperties>());
    }


}
