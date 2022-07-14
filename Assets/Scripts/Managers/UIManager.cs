using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;
    private GameManager gameManager;
    private EconomyManager economy;
    private Market market;

    public GameObject CommunityMenu;
    public GameObject MarketMenu;
    public GameObject NewspaperMenu;
    public GameObject ActionMenu;
    public GameObject CropMenu;
    public GameObject toolsMenu;
    public GameObject newFieldMenu;
    public GameObject FieldHealthMenu;
    public GameObject ImprovementsMenu;  

    public Button[] actionButtons;
    public Button[] cropMenuButtons;
    public Button[] toolsButtons;
    public Button newFieldButton;
    public Button usePesticideButton;

    [SerializeField]
    private GameObject cashUIObject;
    [SerializeField]
    private GameObject turnValueUIObject;
    [SerializeField]
    private GameObject seasonValueUIObject;
    [SerializeField]
    private GameObject fertilserUIObject;
    [SerializeField]
    private GameObject pesticideUIObject;
    [SerializeField]
    private GameObject actionRemainingUIObject;

    private TextMeshProUGUI cashValueText;
    private TextMeshProUGUI turnValueText;
    private TextMeshProUGUI seasonValueText;
    private TextMeshProUGUI fertiliserValueText;
    private TextMeshProUGUI pesticideValueText;
    private TextMeshProUGUI actionRemainingValueText;
    [SerializeField]
    private TextMeshProUGUI[] PriceText;
    private string[] DefaultPriceText;

    public void Awake()
    {
        inst = this;

        gameManager = GetComponent<GameManager>();
        market = MarketMenu.GetComponent<Market>();
        economy = GetComponent<EconomyManager>();

        actionButtons = ActionMenu.GetComponentsInChildren<Button>();
        cropMenuButtons = CropMenu.GetComponentsInChildren<Button>();
        toolsButtons = toolsMenu.GetComponentsInChildren<Button>();

        cashValueText = cashUIObject.GetComponent<TextMeshProUGUI>();
        turnValueText = turnValueUIObject.GetComponent<TextMeshProUGUI>();
        seasonValueText = seasonValueUIObject.GetComponent<TextMeshProUGUI>();
        fertiliserValueText = fertilserUIObject.GetComponent<TextMeshProUGUI>();
        pesticideValueText = pesticideUIObject.GetComponent<TextMeshProUGUI>();
        actionRemainingValueText = actionRemainingUIObject.GetComponent<TextMeshProUGUI>();

        UpdateUIText();

        DefaultPriceText = new string[10];
    }

    public void Start()
    {
        DisableMenus();

        for (int i = 0; i < 10; i++)
        {
            DefaultPriceText[i] = PriceText[i].text;
        }

        UpdateCropPriceDisplay();
    }

    public void Update()
    {
        if (Input.GetMouseButton(1))
        {
            DisableMenus();
            //if statement to reduce work
            for (int i = 0; i < actionButtons.Length; i++)
            {
                actionButtons[i].onClick.RemoveAllListeners();
            }

            for (int i = 0; i < cropMenuButtons.Length; i++)
            {
                cropMenuButtons[i].onClick.RemoveAllListeners();
            }

            newFieldButton.onClick.RemoveAllListeners();
        }
    }

    public void DisableMenus()
    {
        CommunityMenu.SetActive(false);
        MarketMenu.SetActive(false);
        NewspaperMenu.SetActive(false);
        ActionMenu.SetActive(false);
        CropMenu.SetActive(false);
        toolsMenu.SetActive(false);
        newFieldMenu.SetActive(false);
        FieldHealthMenu.SetActive(false);
        ImprovementsMenu.SetActive(false);
    }

    public void DisableActionButtons()
    {
        foreach (Button child in actionButtons)
        {
            child.interactable = false;
        }

        foreach (Button child in cropMenuButtons)
        {
            child.interactable = false;
        }
    }

    public void ReenableActionButtons()
    {
        foreach (Button child in actionButtons)
        {
            child.interactable = true;
        }

        foreach (Button child in cropMenuButtons)
        {
            child.interactable = true;
        }
    }

    public void UpdateUIText()
    {
        if(gameManager.cash > 1000)
        {
            cashValueText.color = Color.green;
        }
        if(gameManager.cash < 200)
        {
            cashValueText.color = Color.yellow;
        }

        cashValueText.text = "Cash: $" + gameManager.cash.ToString();
        turnValueText.text = "Turn Number: " + gameManager.turnNum.ToString();
        fertiliserValueText.text = "Fertiliser Left: " + market.marketInventory.Fertiliser.ToString();
        pesticideValueText.text = "Pesticides Left: " + market.marketInventory.Pesticide.ToString();
        actionRemainingValueText.text = "Actions Remaining: " + gameManager.remainingActions.ToString();

        if (gameManager.DrySeason)
        {
            seasonValueText.text = "Season: Dry";
        }
        else
        {
            seasonValueText.text = "Season: Rainy";
        }
    }

    public void DisableToolButton(int i)
    {
        toolsButtons[i].interactable = false;
    }

    public void UpdateCropPriceDisplay()
    {
        for(int i = 0; i < 10; i++)
        {
            //Price Colour Change
            PriceText[i].text = DefaultPriceText[i] +  economy.currentCropPrices[i].ToString("0.00");
        }
    }

}
