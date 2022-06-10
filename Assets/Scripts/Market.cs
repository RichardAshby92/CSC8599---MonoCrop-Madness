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

        //Does it disable
    }

    public void BuyTools(int idnum)
    {
        inventory.tools[idnum] = true;
        uIManager.DisableToolButton(idnum);
        gameManager.cash -= 200;
        uIManager.UpdateUIText();
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
