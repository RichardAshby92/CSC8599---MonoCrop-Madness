using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldProperties : MonoBehaviour
{
    [SerializeField]
    public int soilQuality; //1 - 100 Percentage
    [SerializeField]
    public int size { get; set; }
    public float fieldHealth;
    [SerializeField]
    private int leaching;
    public int cropAge;
    public bool isCropRipe;
    public int[] timesPlanted;

    private int lackOfwater = 0;
    [SerializeField]
    private int _maximumSoilQuality;

    public GameObject gameManagerObject;
    private GameManager gameManager;

    [SerializeField]
    private Material goodSoilMaterial;
    [SerializeField]
    private Material averageSoilMaterial;
    [SerializeField]
    private Material badSoilMaterial;

    public CropPreset crop;

    const int ImprovementID = 8;
    const int ImprovementID2 = 10;

    private void Awake()
    {
        Intialise();
        timesPlanted = new int[11];
    }

    public void Intialise()
    {
        if (gameManagerObject)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
    }

    public void CalculateFieldHealth()
    {
        //Skip Calc if field is barren. 
        if(crop.idNum == 0)
        {
            return;
        }
        
        //Check if Crops get water
        if(gameManager.waterLevel <= 1)
        {
            lackOfwater = 20;
        }
        else
        {
            lackOfwater = 0;
        }
        
        fieldHealth -= (gameManager.numPests[crop.idNum] + lackOfwater);
        fieldHealth *= gameManager.numPollinators / 100;
        fieldHealth *= (float)(1.1 / Mathf.Exp(5 / soilQuality));
        print(fieldHealth);

        float ImprovementMultiplier = ImprovementNodeActioners.GetMultiplier(ImprovementID);
        fieldHealth *= ImprovementMultiplier;

        fieldHealth = Mathf.Clamp(fieldHealth, 0, 100);

        if(fieldHealth < 1) //Crop Dies
        {
            //Change Field Type to Barren
            crop = Resources.Load<CropPreset>("CropPresets/Barren");
            Destroy(transform.GetChild(0).gameObject);
            Instantiate(crop.unripePrefab, this.transform);
            CalculateMaterial();
        }
    }

    public void CalculateSoilQuality()
    {
        float naturalRecovery = 0;
        float ImprovementMultiplier = ImprovementNodeActioners.GetMultiplier(ImprovementID2);
        if(crop.idNum == 0)
        {
            naturalRecovery = 10;
            naturalRecovery *= ImprovementMultiplier;
        }

        soilQuality += ((int)naturalRecovery + crop.soilChange - leaching);
        soilQuality = Mathf.Clamp(soilQuality, 1, _maximumSoilQuality);
        CalculateMaterial();
    }

    public void GrowCrops()
    {
        if(crop.displayName == "Barren")
        {
            return;
        }
        else
        {
            cropAge++;
            if(cropAge == crop.turnsToGrow)
            {
                isCropRipe = true;
                Destroy(transform.GetChild(0).gameObject);
                Instantiate(crop.ripePrefab, this.transform);
                CalculateMaterial();
            }
        }
    }

    public void CalculatePests()
    {
        for(int i = 0; i < timesPlanted.Length; i++)
        {
            if((float)timesPlanted[i] / ((float)gameManager.turnNum + 2) > 3)
            {
                gameManager.numPests[i] += 5;
            }
        }
    }

    public void CalculateMaterial()
    {
        if (soilQuality >= 200)
        {
            GetComponentInChildren<MeshRenderer>().sharedMaterial = goodSoilMaterial;
        }
        else if (soilQuality < 200 && soilQuality > 100)
        {
            GetComponentInChildren<MeshRenderer>().sharedMaterial = averageSoilMaterial;
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().sharedMaterial = badSoilMaterial;
        }
    }
}


