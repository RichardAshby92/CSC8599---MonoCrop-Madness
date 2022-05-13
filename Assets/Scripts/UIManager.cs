using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;
    private GameManager gameManager;
    private Inventory inventory;

    public GameObject CommunityMenu;
    public GameObject MarketMenu;
    public GameObject NewspaperMenu;
    public GameObject ActionMenu;
    public GameObject CropMenu;

    public Button[] actionButtons;
    public Button[] cropMenuButtons;

    public GameObject cashUIObject;
    public GameObject turnValueUIObject;
    public GameObject seasonValueUIObject;
    public GameObject fertilserUIObject;
    public GameObject pesticideUIObject;
    public GameObject actionRemainingUIObject;

    private TextMeshProUGUI cashValueText;
    private TextMeshProUGUI turnValueText;
    private TextMeshProUGUI seasonValueText;
    private TextMeshProUGUI fertiliserValueText;
    private TextMeshProUGUI pesticideValueText;
    private TextMeshProUGUI actionRemainingValueText;


     public void Awake()
     {
        inst = this;

        gameManager = GetComponent<GameManager>();
        inventory = GetComponent<Inventory>();

        //DisableMenus();
        actionButtons = ActionMenu.GetComponentsInChildren<Button>();
        cropMenuButtons = CropMenu.GetComponentsInChildren<Button>();

        cashValueText = cashUIObject.GetComponent<TextMeshProUGUI>();
        turnValueText = turnValueUIObject.GetComponent<TextMeshProUGUI>();
        seasonValueText = seasonValueUIObject.GetComponent<TextMeshProUGUI>();
        fertiliserValueText = fertilserUIObject.GetComponent<TextMeshProUGUI>();
        pesticideValueText = pesticideUIObject.GetComponent<TextMeshProUGUI>();
        actionRemainingValueText = actionRemainingUIObject.GetComponent<TextMeshProUGUI>();

        UpdateUIText();
     }

    public void Start()
    {
        DisableMenus();
    }

    public void Update()
    {
        if(Input.GetMouseButton(1))
        {
            DisableMenus();
            for(int i = 0; i < actionButtons.Length; i++)
            {
                actionButtons[i].onClick.RemoveAllListeners();
            }

            for (int i = 0; i < cropMenuButtons.Length; i++)
            {
                cropMenuButtons[i].onClick.RemoveAllListeners();
            }
        }
    }

    public void DisableMenus()
    {
        CommunityMenu.SetActive(false);
        MarketMenu.SetActive(false);
        NewspaperMenu.SetActive(false);
        ActionMenu.SetActive(false);
        CropMenu.SetActive(false);
    }

    public void DisableActionButtons()
    {
        foreach(Button child in actionButtons)
        {
            child.interactable = false;
        }

        foreach(Button child in cropMenuButtons)
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
        cashValueText.text = "Cash: $" + gameManager.cash.ToString();
        turnValueText.text = "Turn Number: " + gameManager.turnNum.ToString();
        seasonValueText.text = "Season: ";
        fertiliserValueText.text = "Fertiliser Left: " + inventory.fertilizer.ToString();
        pesticideValueText.text = "Pesticides Left: " + inventory.pesticide.ToString();
        actionRemainingValueText.text = "Actions Remaining: " + gameManager.remainingActions.ToString();
}

}
