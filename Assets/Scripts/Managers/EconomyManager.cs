using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EconomyManager : MonoBehaviour
{
    public static EconomyManager S_inst;
    private GameManager _gameManager;
    private Market _market;

    [SerializeField]
    private GameObject _messageImage;
    [SerializeField]
    private TextMeshProUGUI _titleText;
    [SerializeField]
    private TextMeshProUGUI _bodyText;

    [SerializeField]
    private int[] _baseCropPrices;
    private int[] _marketShocks;
    private int _currentShock = 0;

    [field:SerializeField]
    public float[] CurrentCropPrices { get; set; }

    [SerializeField]
    private TextAsset _economicShockData;
    string[,] _economicShockText;

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

        _economicShockText = LoadData.LoadCSVToStringArray(_economicShockData);
    }

    public void SimulateEnconomy()
    {
        if(_marketShocks[_currentShock] == _gameManager.TurnNum)
        {
            EconomicShock(_currentShock);
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
        _messageImage.SetActive(true);

        switch (i)
        {
            case 0:
                //Demand for Sorghum
                //Increase Sorghum Price
                CurrentCropPrices[7] *= 2;
                _titleText.text = _economicShockText[1, 0];
                _bodyText.text = _economicShockText[1, 1];

                break;
            case 1:
                //Sugar Price Crash
                //Decrease Sugar Price
                CurrentCropPrices[8] *= 0.75f;
                _titleText.text = _economicShockText[2, 0];
                _bodyText.text = _economicShockText[2, 1];
                break;
            case 2:
                //Fertiliser Shortage
                _market.FertiliserPrice *= 2;
                _titleText.text = _economicShockText[3, 0];
                _bodyText.text = _economicShockText[3, 1];
                break;
            case 3:
                //Pesticide Shortage
                _market.PesticidePrice *= 2;
                _titleText.text = _economicShockText[4, 0];
                _bodyText.text = _economicShockText[4, 1];

                break;
            case 4:
                //Second Sugar Price Crash
                //Decrease Sugar Price
                CurrentCropPrices[8] *= 0.5f;
                _titleText.text = _economicShockText[5, 0];
                _bodyText.text = _economicShockText[5, 1];
                break;
            case 5:
                //Price Rise in Wheat
                //Increase Wheat Price
                CurrentCropPrices[6] *= 1.5f;
                _titleText.text = _economicShockText[6, 0];
                _bodyText.text = _economicShockText[6, 1];
                break;
            default:
                break;
        }
    }
}
