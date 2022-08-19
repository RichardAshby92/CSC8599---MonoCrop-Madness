using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager S_inst;
    private GameManager _gameManager;
    private Market _market;

    [SerializeField]
    private int[] _baseCropPrices;
    private int[] _marketShocks;
    private int _currentShock = 0;

    [field:SerializeField]
    public float[] CurrentCropPrices { get; set; }

    const int _improvementID = 2;

    private void Awake()
    {
        S_inst = this;
        _gameManager = GetComponent<GameManager>();
        _market = GetComponent<Market>();
        _marketShocks = new int[6];

        for (int i = 0; i < _baseCropPrices.Length; i++)
        {
            CurrentCropPrices[i] = _baseCropPrices[i];
        }

        for(int i =1; i <= 6; i++)
        {
            _marketShocks[i-1] = Random.Range((20*(i-1) +1), 20 *i);
        }

        //Load Market Shocks from .csv File? 
    }

    public void SimulateEnconomy()
    {
        if(_marketShocks[_currentShock] == _gameManager.TurnNum)
        {
            print("Market Shock");
            EconomicShock(_currentShock);
            //Needs Messages takes from loadTexts Array
            _currentShock++;
        }

        float ImprovementMultplier = ImprovementNodeActioners.GetMultiplier(_improvementID);

        for (int i = 0; i < CurrentCropPrices.Length; i++)
        {
           
            float changeFactor = Random.Range(75, 150);;
            CurrentCropPrices[i] *= 100/changeFactor;
            CurrentCropPrices[i] *= ImprovementMultplier;
        }
    }

    private void EconomicShock(int i)
    {
        switch (i)
        {
            case 0:
                //Demand for Sorghum
                //Increase Sorghum Price
                CurrentCropPrices[7] *= 2;
                //Load Text on Information
                break;
            case 1:
                //Sugar Price Crash
                //Decrease Sugar Price
                CurrentCropPrices[8] *= 0.75f;
                //Load Text on Information
                break;
            case 2:
                //Fertiliser Shortage
                _market.FertiliserPrice *= 2;
                //Load Text on Information
                break;
            case 3:
                //Pesticide Shortage
                _market.PesticidePrice *= 2;
                //Load Text on Information

                break;
            case 4:
                //Second Sugar Price Crash
                //Decrease Sugar Price
                CurrentCropPrices[8] *= 0.5f;
                //Load Text on Information
                break;
            case 5:
                //Price Rise in Wheat
                //Increase Wheat Price
                CurrentCropPrices[6] *= 1.5f;
                //Load Text on Information
                break;
            default:
                break;
        }
    }
}
