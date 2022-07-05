using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManagerObject;

    EconomyManager economyManager;

    [SerializeField]
    private GameObject GoodHealthTextObject;
    [SerializeField]
    private GameObject BadHealthTextObject;

    [SerializeField]
    private TextMeshProUGUI valueText;
    [SerializeField]
    private TextMeshProUGUI cropTypeText;
    [SerializeField]
    private TextMeshProUGUI cropAgeText;

    private float price;
    private float health;
    private int cropAge;

    //possibly Image

    private void Awake()
    {
        if(gameManagerObject)
        {
            economyManager = gameManagerObject.GetComponent<EconomyManager>();
        }
    }

    public void Intialise(ref FieldProperties currentField)
    {
        print(currentField.crop.idNum);

        price = economyManager.currentCropPrices[currentField.crop.idNum];
        health = currentField.fieldHealth;

        valueText.text += price.ToString();

        cropTypeText.text += currentField.crop.displayName;
        
        if(health >= 100)
        {
            GoodHealthTextObject.SetActive(true);
            BadHealthTextObject.SetActive(false);
        }
        else
        {
            GoodHealthTextObject.SetActive(false);
            BadHealthTextObject.SetActive(true);
        }
    }
}
