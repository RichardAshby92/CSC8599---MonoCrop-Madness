using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FieldActions : MonoBehaviour
{

    public GameObject gameManagerObject;
    private GameManager gameManager;
    private EconomyManager economyManager;
    private UIManager uIManager;
    private Inventory inventory;
    private FieldProperties crop;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        uIManager = gameManagerObject.GetComponent<UIManager>();
        economyManager = gameManagerObject.GetComponent<EconomyManager>();
        inventory = gameManagerObject.GetComponent<Inventory>();
        crop = GetComponent<FieldProperties>();
    }

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        uIManager.ActionMenu.SetActive(true);

        uIManager.actionButtons[0].onClick.AddListener(PlantField);
        uIManager.actionButtons[1].onClick.AddListener(AddFertiliser);
        uIManager.actionButtons[2].onClick.AddListener(HarvestField);

        if (!crop.isCropRipe)
        {
            uIManager.actionButtons[2].interactable = false;
        }
        else
        {
            uIManager.actionButtons[2].interactable = true;
        }

        if(!inventory.isThereFertilizer)
        {
            uIManager.actionButtons[1].interactable = false;
        }
        else
        {
            uIManager.actionButtons[1].interactable = true;
        }

        for (int i =0; i < uIManager.cropMenuButtons.Length; i++)
        {
            uIManager.cropMenuButtons[i].onClick.AddListener(delegate { PlantCrop(i); });
        }

    }

    public void PlantField()
    {
        //Enable Menu of Crops
        uIManager.CropMenu.SetActive(true);
    }

    public void PlantCrop(int x)
    {
        gameManager.ActionRemaining();
        switch (x)
        {
            case 1:
                print("Printed Sugar Cane");
                crop.crop = Resources.Load<CropPreset>("CropPresets/SugarCane");
                break;
            default:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Barren");
                break;
        }
    }

    public void AddFertiliser()
    {
        gameManager.ActionRemaining(); //Should it take an action?
        inventory.SubtractFromInventory(0);
        crop.soilQuality += 50;

        if (!inventory.isThereFertilizer)
        {
            uIManager.actionButtons[1].interactable = false;
        }
        //RainForest Damage Function
    }

    public void HarvestField()
    {
        if(GetComponent<FieldProperties>().isCropRipe) //Probably not needed, checked twice
        {
            gameManager.ActionRemaining(); //Add Tool Check for action amount
            int AmountHarvested = crop.size * (int)crop.fieldHealth;
            AmountHarvested *= economyManager.currentCropPrices[crop.crop.idNum];
            gameManager.cash += AmountHarvested;

            crop.crop = Resources.Load<CropPreset>("CropPresets/Barren");
        }
    }   
}
