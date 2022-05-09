using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldProperties : MonoBehaviour
{
    public int soilQuality; //1 - 100 Percentage
    public int size;
    public float fieldHealth;
    public int leaching;
    public int cropAge;
    public bool isCropRipe;

    private int lackOfwater = 0;

    public GameObject gameManagerObject;
    private GameManager gameManager;
    public CropPreset crop;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
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

        
        fieldHealth -= (gameManager.numPests + lackOfwater);
        print(fieldHealth);
        fieldHealth *= gameManager.numPollinators / 100;
        fieldHealth *= (float)(1.1 / Mathf.Exp(5 / soilQuality));
        print(fieldHealth);

        fieldHealth = Mathf.Clamp(fieldHealth, 0, 100);

        if(fieldHealth < 1) //Crop Dies
        {
            //Change Field Type to Barren
            crop = Resources.Load<CropPreset>("CropPresets/Barren");
        }
    }

    public void CalculateSoilQuality()
    {
        soilQuality += (crop.soilChange - leaching);
        soilQuality = Mathf.Clamp(soilQuality, 1, 1000);
        //Change Soil Texture
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
            }
        }
    }

    public void PlantCrop(CropPreset newCrop)
    {
        crop = Resources.Load<CropPreset>(newCrop.displayName);
    }

    public int HarvestField()
    {
        int amount = size * (int)fieldHealth;
        return amount;
    }

}


