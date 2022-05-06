using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject CommunityMenu;
    public GameObject MarketMenu;
    public GameObject NewspaperMenu;
    public GameObject ActionMenu;

    public Button[] actionButtons;

    private void Awake()
    {
        var turnsElsasped = GetComponent<GameManager>().actionsElasped;
        turnsElsasped.AddListener(DisableActionButtons);
        DisableMenus();

        actionButtons = ActionMenu.GetComponentsInChildren<Button>();
    }

    public void Update()
    {
        if(Input.GetMouseButton(1))
        {
            DisableMenus();
        }
    }

    void DisableMenus()
    {
        CommunityMenu.SetActive(false);
        MarketMenu.SetActive(false);
        NewspaperMenu.SetActive(false);
        ActionMenu.SetActive(false);
    }

    void DisableActionButtons()
    {
        foreach(Button child in actionButtons)
        {
            child.interactable = false;
        }
    }

    void ReeableActionButtons()
    {
        foreach (Button child in actionButtons)
        {
            child.interactable = true;
        }
    }

}
