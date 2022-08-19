using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public GameObject _gameManagerObject;

    private GameManager _gameManager;
    private UIManager _uiManager;

    [field: SerializeField]
    public int FertiliserPrice { get; set; }
    [field: SerializeField]
    public int PesticidePrice { get; set; }
    [SerializeField]
    private int _toolCost;

    public MarketInventory MarketInventory;

    private void Awake()
    {
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
        _uiManager = _gameManagerObject.GetComponent<UIManager>();

        for (int i = 0; i < _uiManager.ToolsButtons.Length; i++)
        {
            int tempNum = i; //Needed for C#
            _uiManager.ToolsButtons[i].onClick.AddListener(delegate { BuyTools(tempNum); });
        }

        _uiManager.DisableToolButton(7);
    }

    public void AccessMenu()
    {
        _uiManager.DisableMenus();
        gameObject.SetActive(true);
    }

    public void BuyToolsMenu()
    {
        //enable tools list
        _uiManager.ToolsMenu.SetActive(true);

    }

    public void BuyTools(int idnum)
    {
        MarketInventory.Tools[idnum] = true;
        _uiManager.DisableToolButton(idnum);
        _gameManager.Cash -= _toolCost;
        _uiManager.UpdateUIText();
    }

    public void BuyFertiliser()
    {
        MarketInventory.Fertiliser++;
        _gameManager.Cash -= FertiliserPrice;
        _uiManager.UpdateUIText();
    }

    public void BuyPesticides()
    {
        MarketInventory.Pesticide++;
        _gameManager.Cash -= PesticidePrice;
        _uiManager.UpdateUIText();
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
    public bool[] Tools { get; set; }
}
