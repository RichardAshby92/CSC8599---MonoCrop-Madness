using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;

    private EconomyManager _economyManager;
    private GameManager _gameManager;
    private UIManager _uiManager;

    [SerializeField]
    private TextMeshProUGUI _healthTextObject;
    [SerializeField]
    private TextMeshProUGUI _valueText;
    [SerializeField]
    private TextMeshProUGUI _cropTypeText;
    [SerializeField]
    private TextMeshProUGUI _cropAgeText;

    private float _price;
    private float _health;
    private int _cropAge;

    [SerializeField]
    private string _goodHealthText;
    [SerializeField]
    private string _mediumHealthText;
    [SerializeField]
    private string _lowHealthText;

    private void Awake()
    {
        if(_gameManagerObject)
        {
            _economyManager = _gameManagerObject.GetComponent<EconomyManager>();
            _uiManager = _gameManagerObject.GetComponent<UIManager>();
        }
    }

    public void Intialise(ref FieldProperties currentField)
    {
        _price = _economyManager.CurrentCropPrices[currentField.crop.IdNum];
        _health = currentField.FieldHealth;
        _cropAge = currentField.CropAge;
        
        _valueText.text = "Current Price: " + _price.ToString("0.00");
        _cropTypeText.text = "Crop Type: " + currentField.crop.DisplayName;
        _cropAgeText.text = "Crop Age: " + _cropAge;
        
        if(_health >= 100)
        {
            _healthTextObject.text = _goodHealthText;
        }
        else
        {
            _healthTextObject.text = _mediumHealthText;
        }

        _uiManager.UpdateUIText();
    }
}
