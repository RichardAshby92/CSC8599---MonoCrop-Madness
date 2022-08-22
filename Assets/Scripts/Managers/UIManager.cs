using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager S_inst;
    private GameManager _gameManager;
    private EconomyManager _economy;
    private Market _market;

    [field:SerializeField]
    public GameObject CommunityMenu { get; set; }
    [field:SerializeField]
    public GameObject MarketMenu { get; set; }
    [field:SerializeField]
    public GameObject TipsMenu { get; set; }
    [field:SerializeField]
    public GameObject ActionMenu { get; set; }
    [field:SerializeField]
    public GameObject CropMenu { get; set; }
    [field:SerializeField]
    public GameObject ToolsMenu { get; set; }
    [field:SerializeField]
    public GameObject NewFieldMenu { get; set; }
    [field:SerializeField]
    public GameObject FieldHealthMenu { get; set; }
    [field:SerializeField]
    public GameObject ImprovementsMenu { get; set; }

    [SerializeField]
    private GameObject _tipImageObject;

    public Button[] ActionButtons { get; set; }
    public Button[] CropMenuButtons { get; set; }
    public Button[] ToolsButtons { get; set; }
    [field:SerializeField]
    public Button NewFieldButton { get; set; }
    public Button UsePesticideButton { get; set; }

    [SerializeField]
    private GameObject _cashUIObject;
    [SerializeField]
    private GameObject _turnValueUIObject;
    [SerializeField]
    private GameObject _seasonValueUIObject;
    [SerializeField]
    private GameObject _fertilserUIObject;
    [SerializeField]
    private GameObject _pesticideUIObject;
    [SerializeField]
    private GameObject _actionRemainingUIObject;

    private TextMeshProUGUI _cashValueText;
    private TextMeshProUGUI _turnValueText;
    private TextMeshProUGUI _seasonValueText;
    private TextMeshProUGUI _fertiliserValueText;
    private TextMeshProUGUI _pesticideValueText;
    private TextMeshProUGUI _actionRemainingValueText;
    [SerializeField]
    private TextMeshProUGUI[] _priceText;
    private string[] _defaultPriceText;

    public bool _level2Menu { get; set; }

    public void Awake()
    {
        S_inst = this;

        _gameManager = GetComponent<GameManager>();
        _market = MarketMenu.GetComponent<Market>();
        _economy = GetComponent<EconomyManager>();

        ActionButtons = ActionMenu.GetComponentsInChildren<Button>();
        CropMenuButtons = CropMenu.GetComponentsInChildren<Button>();
        ToolsButtons = ToolsMenu.GetComponentsInChildren<Button>();

        _cashValueText = _cashUIObject.GetComponent<TextMeshProUGUI>();
        _turnValueText = _turnValueUIObject.GetComponent<TextMeshProUGUI>();
        _seasonValueText = _seasonValueUIObject.GetComponent<TextMeshProUGUI>();
        _fertiliserValueText = _fertilserUIObject.GetComponent<TextMeshProUGUI>();
        _pesticideValueText = _pesticideUIObject.GetComponent<TextMeshProUGUI>();
        _actionRemainingValueText = _actionRemainingUIObject.GetComponent<TextMeshProUGUI>();

        UpdateUIText();

        _defaultPriceText = new string[10];

        _level2Menu = false;
    }

    public void Start()
    {
        DisableMenus();

        for (int i = 0; i < 10; i++)
        {
            _defaultPriceText[i] = _priceText[i].text;
        }

        UpdateCropPriceDisplay();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(_level2Menu)
            {
                _tipImageObject.SetActive(false);
                _level2Menu = false;
            }
            else
            {
                DisableMenus();
            }           

            //if statement to reduce work
            for (int i = 0; i < ActionButtons.Length; i++)
            {
                ActionButtons[i].onClick.RemoveAllListeners();
            }

            for (int i = 0; i < CropMenuButtons.Length; i++)
            {
                CropMenuButtons[i].onClick.RemoveAllListeners();
            }

            NewFieldButton.onClick.RemoveAllListeners();
        }
    }

    public void DisableMenus()
    {
        CommunityMenu.SetActive(false);
        MarketMenu.SetActive(false);
        TipsMenu.SetActive(false);
        ActionMenu.SetActive(false);
        CropMenu.SetActive(false);
        ToolsMenu.SetActive(false);
        NewFieldMenu.SetActive(false);
        FieldHealthMenu.SetActive(false);
        ImprovementsMenu.SetActive(false);
    }

    public void DisableActionButtons()
    {
        foreach (Button child in ActionButtons)
        {
            child.interactable = false;
        }

        foreach (Button child in CropMenuButtons)
        {
            child.interactable = false;
        }
    }

    public void ReenableActionButtons()
    {
        foreach (Button child in ActionButtons)
        {
            child.interactable = true;
        }

        foreach (Button child in CropMenuButtons)
        {
            child.interactable = true;
        }
    }

    public void UpdateUIText()
    {
        if(_gameManager.Cash > 1000)
        {
            _cashValueText.color = Color.green;
        }
        if(_gameManager.Cash < 200)
        {
            _cashValueText.color = Color.yellow;
        }

        _cashValueText.text = "Cash: $" + _gameManager.Cash.ToString();
        _turnValueText.text = "Turn Number: " + _gameManager.TurnNum.ToString();
        _fertiliserValueText.text = "Fertiliser Left: " + _market.MarketInventory.Fertiliser.ToString();
        _pesticideValueText.text = "Pesticides Left: " + _market.MarketInventory.Pesticide.ToString();
        _actionRemainingValueText.text = "Actions Remaining: " + _gameManager.RemainingActions.ToString();

        if (_gameManager.DrySeason)
        {
            _seasonValueText.text = "Season: Dry";
        }
        else
        {
            _seasonValueText.text = "Season: Rainy";
        }
    }

    public void DisableToolButton(int i)
    {
        ToolsButtons[i].interactable = false;
    }

    public void UpdateCropPriceDisplay()
    {
        for(int i = 0; i < 10; i++)
        {
            //Price Colour Change
            _priceText[i].text = _defaultPriceText[i] +  _economy.CurrentCropPrices[i].ToString("0.00");
        }
    }

}
