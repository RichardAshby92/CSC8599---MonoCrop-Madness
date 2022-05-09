using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager inst;

    public int[] baseCropPrices;
    public int[] currentCropPrices;

    private void Awake()
    {
        inst = this;

        for(int i = 0; i < baseCropPrices.Length; i++)
        {
            currentCropPrices[i] = baseCropPrices[i];
        }
    }

    public void SimulateEnconomy()
    {
        for(int i = 0; i < currentCropPrices.Length; i++)
        {
            
        }
    }
}
