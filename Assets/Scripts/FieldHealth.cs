using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManagerObject;

    private EconomyManager economyManager;
    private GameManager gameManager;
    private UIManager uIManager;

    [SerializeField]
    private TextMeshProUGUI HealthTextObject;

    [SerializeField]
    private TextMeshProUGUI valueText;
    [SerializeField]
    private TextMeshProUGUI cropTypeText;
    [SerializeField]
    private TextMeshProUGUI cropAgeText;

    private float price;
    private float health;
    private int cropAge;

    [SerializeField]
    private string GoodHealthText;
    [SerializeField]
    private string MediumHealthText;
    [SerializeField]
    private string LowHealthText;

    //possibly Image

    private void Awake()
    {
        if(gameManagerObject)
        {
            economyManager = gameManagerObject.GetComponent<EconomyManager>();
            gameManager = gameManagerObject.GetComponent<GameManager>();
            uIManager = gameManagerObject.GetComponent<UIManager>();
        }
    }

    public void Intialise(ref FieldProperties currentField)
    {
        price = economyManager.currentCropPrices[currentField.crop.idNum];
        health = currentField.fieldHealth;

        gameManager.ActionRemaining(1);
        
        valueText.text += price.ToString();
        cropTypeText.text += currentField.crop.displayName;
        
        if(health >= 100)
        {
            HealthTextObject.text = GoodHealthText;
        }
        else
        {
            HealthTextObject.text = MediumHealthText;
        }

        uIManager.UpdateUIText();
    }
}
