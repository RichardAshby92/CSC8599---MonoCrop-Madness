using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldProperties : MonoBehaviour
{
    public int soilQuality; //1 - 100 Percentage
    public int size;
    public float fieldHealth;
    public int waterUsed;

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

        fieldHealth -= (localPests + lackOfwater);
        fieldHealth *= localPollinators / 100;
        fieldHealth *= (float)(1.25 / Mathf.Exp(10 / soilQuality));

        Mathf.Clamp(fieldHealth, 0, 100);
    }

    public int HarvestField()
    {
        int amount = size * (int)fieldHealth;
        return amount;
    }

}


