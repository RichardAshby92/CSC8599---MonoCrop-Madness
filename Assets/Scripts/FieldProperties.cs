using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldProperties : MonoBehaviour
{
    public int soilQuality; //1 - 100 Percentage
    public int size;
    public float fieldHealth;
    public int leaching;

    private int lackOfwater = 0;

    public GameObject gameManager;
    public CropPreset crop;

    public void CalculateFieldHealth()
    {
        int localPollinators = gameManager.GetComponent<GameManager>().numPollinators;
        int localPests = gameManager.GetComponent<GameManager>().pests;
        
        if(gameManager.GetComponent<GameManager>().waterLevel < 0)
        {
            lackOfwater = 20;
        }
        else
        {
            lackOfwater = 0;
        }

        //only do Calc if field isnt barren
        fieldHealth -= (localPests + lackOfwater);
        print(fieldHealth);
        fieldHealth *= localPollinators / 100;
        fieldHealth *= (float)(1.1 / Mathf.Exp(5 / soilQuality));
        print(fieldHealth);

        fieldHealth = Mathf.Clamp(fieldHealth, 0, 100);

        if(fieldHealth < 1) //Crop Dies
        {
            //Change Field Type to Barren
            crop = Resources.Load<CropPreset>("ScriptableObjects/CropPresets/Barren");
        }
    }

    public void CalculateSoilQuality()
    {
        soilQuality += (crop.soilChange - leaching);
        soilQuality = Mathf.Clamp(soilQuality, 1, 1000);
        //Change Soil Texture
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


