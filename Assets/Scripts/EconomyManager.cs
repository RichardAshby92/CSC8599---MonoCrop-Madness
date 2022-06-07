using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager inst;
    private GameManager gameManager;
    private Market market;

    public int[] baseCropPrices;
    public float[] currentCropPrices;
    public int[] marketShocks;
    private int currentShock = 0;

    private void Awake()
    {
        inst = this;
        gameManager = GetComponent<GameManager>();
        market = GetComponent<Market>();
        marketShocks = new int[6];

        for (int i = 0; i < baseCropPrices.Length; i++)
        {
            currentCropPrices[i] = baseCropPrices[i];
        }

        for(int i =1; i <= 6; i++)
        {
            marketShocks[i-1] = Random.Range((20*(i-1) +1), 20 *i);
        }

        //Load Market Shocks from .csv File? 
    }

    public void SimulateEnconomy()
    {
        if(marketShocks[currentShock] == gameManager.turnNum)
        {
            print("Market Shock");
            EconomicShock(currentShock);
            //Needs Messages takes from loadTexts Array
            currentShock++;
        }

        for (int i = 0; i < currentCropPrices.Length; i++)
        {
            float changeFactor = Random.Range(75, 150);;
            currentCropPrices[i] *= 100/changeFactor;            
        }
    }

    private void EconomicShock(int i)
    {
        switch (i)
        {
            case 0:
                //Demand for Sorghum
                //Increase Sorghum Price
                currentCropPrices[7] *= 2;
                //Load Text on Information
                break;
            case 1:
                //Sugar Price Crash
                //Decrease Sugar Price
                currentCropPrices[8] *= 0.75f;
                //Load Text on Information
                break;
            case 2:
                //Fertiliser Shortage
                market.fertiliserPrice *= 2;
                //Load Text on Information
                break;
            case 3:
                //Pesticide Shortage
                market.pesticidePrice *= 2;
                //Load Text on Information

                break;
            case 4:
                //Second Sugar Price Crash
                //Decrease Sugar Price
                currentCropPrices[8] *= 0.5f;
                //Load Text on Information
                break;
            case 5:
                //Price Rise in Wheat
                //Increase Wheat Price
                currentCropPrices[6] *= 1.5f;
                //Load Text on Information
                break;
            default:
                break;
        }
    }
}
