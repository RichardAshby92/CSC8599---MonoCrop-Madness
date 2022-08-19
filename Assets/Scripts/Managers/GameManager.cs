using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static GameManager S_inst;

    [SerializeField]
    private GameObject _improvementsObject;
    [SerializeField]
    private GameObject _rainEmitter;

    private UIManager _uiManager;
    private EconomyManager _economyManager;
    private GameSceneManager _gameSceneManager;
    private CommunityManager _communityManager;
    private ImprovementsManager _improvements;
    private List<FieldProperties> _fieldProperties;
    private VisualEffect _rainEffect;

    [SerializeField]
    private GameObject _lake;
    [SerializeField]
    private GameObject _information; //Wrong PLace
    [SerializeField]
    private GameObject[] _fields;

    [field:SerializeField]
    public int TurnNum { set;  get; }
    [field:SerializeField]
    public int RemainingActions { set; get; }
    [field:SerializeField]
    public bool DrySeason { set; get; }
    [field:SerializeField]
    public int Rain { set;  get; }
    [field:SerializeField]
    public int Cash { get; set; }
    [field:SerializeField]
    public int WaterLevel { get; set; } //1 - 100 Percentage
    [field:SerializeField]
    public int NumPollinators { get; set; } //1 - 100 Percentage
    [field:SerializeField]
    public int[] NumPests { get; set; } //1 - 100 Percentage

    [SerializeField]
    private int _loanRepayment;
    [SerializeField]
    private int _minLakeHeight;
    [SerializeField]
    private int _maxLakeheight;

    const int _improvementID = 7;

    public static int MaximumTurns { get; set; }
    public static bool[] S_UnlockedCrops { get; set; }

    private void Awake()
    {
        S_inst = this;
        _uiManager = GetComponent<UIManager>();
        _economyManager = GetComponent<EconomyManager>();
        _gameSceneManager = GetComponent<GameSceneManager>();
        _communityManager = GetComponent<CommunityManager>();
        _improvements = _improvementsObject.GetComponent<ImprovementsManager>();
        _rainEffect = _rainEmitter.GetComponent<VisualEffect>();

        _fieldProperties = new List<FieldProperties>();

        for (int i = 0; i < _fields.Length; i++)
        {
            _fieldProperties.Add(_fields[i].GetComponent<FieldProperties>());
        }

        S_UnlockedCrops = new bool[10];
        S_UnlockedCrops[7] = true;

        MaximumTurns = 120;
    }
       

    public void EndTurn()
    {
        CountTurn();
        LoanRepayment();
        CalculateSeason();
        CalculateRainfall();
        CalculateNewWaterLevel();
        CalculateSoilQuality();
        CalculateFieldHealth();
        GrowCrops();
        CalculatePests();
        _economyManager.SimulateEnconomy();
        ResetActions();
        _uiManager.UpdateUIText();
        _uiManager.UpdateCropPriceDisplay();
        _communityManager.CheckHealth();
        _improvements.ResearchImprovement();

        System.GC.Collect();
    }

    void CountTurn()
    {
        TurnNum++;
        if (TurnNum > MaximumTurns)
        {
            _gameSceneManager.LoadEndGame();
        }
    }

    void LoanRepayment()
    {
        Cash -= _loanRepayment;

        if (Cash <= 0)
        {
            _gameSceneManager.LoadEndGame();
        }
    }

    void CalculateSeason()
    {
        int season = TurnNum % 12;

        if(season < 4 || season > 10)
        {
            _rainEffect.Stop();
            DrySeason = true;
        }
        else
        {
            _rainEffect.Play();
            DrySeason = false;
        }
    }

    void CalculateRainfall()
    {
        if(DrySeason)
        {
            Rain = Random.Range(1, 10);
        }
        else
        {
            Rain = Random.Range(8, 20);
        }
    }

    void CalculateNewWaterLevel()
    {
        float totalWaterUsed = 0;
        float ImprovementMultiplier = ImprovementNodeActioners.GetMultiplier(_improvementID);

        foreach(FieldProperties fieldProperty in _fieldProperties)
        {
            totalWaterUsed += fieldProperty.crop.WaterUsedPerTurn;
        }

        totalWaterUsed *= 1/ImprovementMultiplier;
        WaterLevel += (Rain - (int)totalWaterUsed);
        WaterLevel = Mathf.Clamp(WaterLevel, _minLakeHeight, _maxLakeheight);
        Vector3 waterHeight = _lake.transform.position;
        waterHeight.y = WaterLevel;

        _lake.transform.position = waterHeight;
    }

    void CalculateSoilQuality()
    {
        foreach(FieldProperties fieldProperty in _fieldProperties)
        {
            fieldProperty.CalculateSoilQuality();
        }
    }

    void CalculateFieldHealth()
    {
        foreach(FieldProperties fieldProperty in _fieldProperties)
        {
            fieldProperty.CalculateFieldHealth();
        }      
    }

    void GrowCrops()
    {
        foreach(FieldProperties fieldProperty in _fieldProperties)
        {
            fieldProperty.GrowCrops();
        }
    }

    void CalculatePests()
    {
        foreach(FieldProperties fieldProperty in _fieldProperties)
        {
            fieldProperty.CalculatePests();
        }
    }

    void ResetActions()
    {
        RemainingActions = 4;
        _uiManager.ReenableActionButtons();
    }

    public bool ActionRemaining(int actionsTaken)
    {
        if((RemainingActions - actionsTaken) < 0)
        {
            return false;
        }

        RemainingActions = RemainingActions - actionsTaken;

        if (RemainingActions <= 0)
        {
            _uiManager.DisableActionButtons();
        }

        return true;
    }

    public void AddField(GameObject newField)
    {
        _fieldProperties.Add(newField.GetComponent<FieldProperties>());
    }


}
