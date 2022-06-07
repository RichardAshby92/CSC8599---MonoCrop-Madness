using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public GameObject gameManagerObject;

    private GameManager gameManager;
    private UIManager uIManager;
    private Inventory inventory;

    public int fertiliserPrice;
    public int pesticidePrice;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        uIManager = gameManagerObject.GetComponent<UIManager>();
        inventory = gameManagerObject.GetComponent<Inventory>();
    }

    public void AccessMenu()
    {
        uIManager.DisableMenus();
        gameObject.SetActive(true);
    }

    public void BuyTools()
    {
        //enable tools list
    }

    public void BuyFertiliser()
    {
        inventory.AddToInvetory(0);
        gameManager.cash -= 50;
        uIManager.UpdateUIText();
    }

    public void BuyPesticides()
    {
        inventory.AddToInvetory(1);
        gameManager.cash -= 100;
        uIManager.UpdateUIText();
    }
}
