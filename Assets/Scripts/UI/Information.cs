using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    public GameObject gameManagerObject;

    private GameManager gameManager;
    private UIManager uIManager;

    List<InformationNode> informationNodes;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        uIManager = gameManagerObject.GetComponent<UIManager>();

        informationNodes = new List<InformationNode>(18);
        //Populate Structs
        //Load tips from CSV
    }

    public void AccessMenu()
    {
        uIManager.DisableMenus();
        gameObject.SetActive(true);
    }

    private void BuyTip()
    {
        //Child Bool is Unlockable
        //String equal to load text array
        //Change Button Colour
        //Button is no longer interactable
    }
}

