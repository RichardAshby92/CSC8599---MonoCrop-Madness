using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager inst;

    public int[] baseCropPrices;
    public float[] currentCropPrices;
    public int[] marketShocks;

    private void Awake()
    {
        inst = this;

        for (int i = 0; i < baseCropPrices.Length; i++)
        {
            currentCropPrices[i] = baseCropPrices[i];
        }
    }

    public void SimulateEnconomy()
    {
        int shockChance = Random.Range(0, 100);
        if(shockChance < 5)
        {
            //Market Shock
            //Needs Messages takes from loadTexts Array
            //if(true) then pick event form list
        }

        for (int i = 0; i < currentCropPrices.Length; i++)
        {
            float changeFactor = Random.Range(75, 150);;
            currentCropPrices[i] *= 100/changeFactor;            
        }
    }
}
