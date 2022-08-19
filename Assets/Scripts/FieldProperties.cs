using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldProperties : MonoBehaviour
{
    public GameObject GameManagerObject;
    private GameManager _gameManager;

    public CropPreset crop;

    [field:SerializeField]
    public float FieldHealth { get; set; }
    [field:SerializeField]
    public bool IsCropRipe { get; set; }
    [field: SerializeField]
    public int CropAge { get; set; }
    [field: SerializeField]
    public int[] TimesPlanted { get; set; }
    [field:SerializeField]
    public int _soilQuality { get; set; }

    [SerializeField]
    private int _leaching;
    [SerializeField]
    private int _maximumSoilQuality;
    [SerializeField]
    private Material _goodSoilMaterial;
    [SerializeField]
    private Material _averageSoilMaterial;
    [SerializeField]
    private Material _badSoilMaterial;

    private int _lackOfwater = 0;
    private const int _improvementID = 8;
    private const int _improvementID2 = 10;

    private void Awake()
    {
        Intialise();
        TimesPlanted = new int[11];
    }

    public void Intialise()
    {
        if (GameManagerObject)
        {
            _gameManager = GameManagerObject.GetComponent<GameManager>();
        }
    }

    public void CalculateFieldHealth()
    {
        if(crop.IdNum == 0)
        {
            return;
        }
        if(_gameManager.WaterLevel <= 1)
        {
            _lackOfwater = 20;
        }
        else
        {
            _lackOfwater = 0;
        }
        
        FieldHealth -= (_gameManager.NumPests[crop.IdNum] + _lackOfwater);
        FieldHealth *= _gameManager.NumPollinators / 100;
        FieldHealth *= (float)(1.1 / Mathf.Exp(5 / _soilQuality));
        print(FieldHealth);

        float ImprovementMultiplier = ImprovementNodeActioners.GetMultiplier(_improvementID);
        FieldHealth *= ImprovementMultiplier;

        FieldHealth = Mathf.Clamp(FieldHealth, 0, 100);

        if(FieldHealth < 1) //Crop Dies
        {
            //Change Field Type to Barren
            crop = Resources.Load<CropPreset>("CropPresets/Barren");
            Destroy(transform.GetChild(0).gameObject);
            Instantiate(crop.UnripePrefab, this.transform);
            CalculateMaterial();
        }
    }

    public void CalculateSoilQuality()
    {
        float naturalRecovery = 0;
        float ImprovementMultiplier = ImprovementNodeActioners.GetMultiplier(_improvementID2);
        if(crop.IdNum == 0)
        {
            naturalRecovery = 10;
            naturalRecovery *= ImprovementMultiplier;
        }

        _soilQuality += ((int)naturalRecovery + crop.SoilChange - _leaching);
        _soilQuality = Mathf.Clamp(_soilQuality, 1, _maximumSoilQuality);
        CalculateMaterial();
    }

    public void GrowCrops()
    {
        if(crop.DisplayName == "Barren")
        {
            return;
        }
        else
        {
            CropAge++;
            if(CropAge == crop.TurnsToGrow)
            {
                IsCropRipe = true;
                Destroy(transform.GetChild(0).gameObject);
                Instantiate(crop.RipePrefab, this.transform);
                CalculateMaterial();
            }
        }
    }

    public void CalculatePests()
    {
        for(int i = 0; i < TimesPlanted.Length; i++)
        {
            if((float)TimesPlanted[i] / ((float)_gameManager.TurnNum + 2) > 3)
            {
                _gameManager.NumPests[i] += 5;
            }
        }
    }

    public void CalculateMaterial()
    {
        if (_soilQuality >= 200)
        {
            GetComponentInChildren<MeshRenderer>().sharedMaterial = _goodSoilMaterial;
        }
        else if (_soilQuality < 200 && _soilQuality > 100)
        {
            GetComponentInChildren<MeshRenderer>().sharedMaterial = _averageSoilMaterial;
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().sharedMaterial = _badSoilMaterial;
        }
    }
}


