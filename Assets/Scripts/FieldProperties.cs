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
    public int[] timesPlanted;

    private int lackOfwater = 0;

    public GameObject gameManagerObject;
    private GameManager gameManager;
    public CropPreset crop;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        timesPlanted = new int[11];
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
        print(fieldHealth);
        fieldHealth *= gameManager.numPollinators / 100;
        fieldHealth *= (float)(1.1 / Mathf.Exp(5 / soilQuality));
        print(fieldHealth);

        fieldHealth = Mathf.Clamp(fieldHealth, 0, 100);

        if(fieldHealth < 1) //Crop Dies
        {
            //Change Field Type to Barren
            crop = Resources.Load<CropPreset>("CropPresets/Barren");
            Destroy(transform.GetChild(0).gameObject);
            Instantiate(crop.ripePrefab, this.transform);
        }
    }

    public void CalculateSoilQuality()
    {
        int naturalRecovery = 0;
        if(crop.idNum == 0)
        {
            naturalRecovery = 10;
        }

        soilQuality += (naturalRecovery + crop.soilChange - leaching); //Scale leaching with rain
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
}


