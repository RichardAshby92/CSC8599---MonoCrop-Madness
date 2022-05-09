using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    public GameObject CommunityMenu;
    public GameObject MarketMenu;
    public GameObject NewspaperMenu;
    public GameObject ActionMenu;
    public GameObject CropMenu;

    public Button[] actionButtons;
    public Button[] cropMenuButtons;

     public void Awake()
     {
        inst = this;

        DisableMenus();
        actionButtons = ActionMenu.GetComponentsInChildren<Button>();
        cropMenuButtons = CropMenu.GetComponentsInChildren<Button>();
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

    void DisableMenus()
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
    }

    public void ReenableActionButtons()
    {
        foreach (Button child in actionButtons)
        {
            child.interactable = true;
        }
    }

}
