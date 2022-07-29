using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public GameObject gameManagerObject;

    private GameManager gameManager;
    private UIManager uIManager;

    [field: SerializeField]
    public int fertiliserPrice { get; set; }
    [field: SerializeField]
    public int pesticidePrice { get; set; }
    [SerializeField]
    private int toolCost;

    public MarketInventory marketInventory;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        uIManager = gameManagerObject.GetComponent<UIManager>();

        for (int i = 0; i < uIManager.toolsButtons.Length; i++)
        {
            int tempNum = i; //Needed for C#
            uIManager.toolsButtons[i].onClick.AddListener(delegate { BuyTools(tempNum); });
        }

        uIManager.DisableToolButton(7);
    }

    public void AccessMenu()
    {
        uIManager.DisableMenus();
        gameObject.SetActive(true);
    }

    public void BuyToolsMenu()
    {
        //enable tools list
        uIManager.toolsMenu.SetActive(true);

    }

    public void BuyTools(int idnum)
    {
        marketInventory.tools[idnum] = true;
        uIManager.DisableToolButton(idnum);
        gameManager.cash -= toolCost;
        uIManager.UpdateUIText();
    }

    public void BuyFertiliser()
    {
        marketInventory.Fertiliser++;
        gameManager.cash -= fertiliserPrice;
        uIManager.UpdateUIText();
    }

    public void BuyPesticides()
    {
        marketInventory.Pesticide++;
        gameManager.cash -= pesticidePrice;
        uIManager.UpdateUIText();
    }
}

[System.Serializable]
public struct MarketInventory
{
    [field: SerializeField]
    public int Pesticide { get; set; }
    [field: SerializeField]
    public int Fertiliser { get; set; }
    [field: SerializeField]
    public bool[] tools { get; set; }
    [field: SerializeField]
    public bool[] cultivars { get; set; }
}
