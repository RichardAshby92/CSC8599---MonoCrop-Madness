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
        Instantiate(crop.crop.prefab, this.transform);
    }

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        uIManager.DisableMenus();
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
    }

    public void PlantField()
    {
        //Enable Menu of Crops
        for (int i = 0; i < uIManager.cropMenuButtons.Length; i++)
        {
            int tempNum = i + 1; //Needed for C#
            uIManager.cropMenuButtons[i].onClick.AddListener(delegate { PlantCrop(tempNum); });
        }
        uIManager.CropMenu.SetActive(true);
    }

    public void PlantCrop(int x)
    {
        gameManager.ActionRemaining();

        switch (x)
        {
            case 1:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Carnations");
                break;
            case 2:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Cassava");
                break;
            case 3:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Cotton");
                break;
            case 4:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Maize");
                break;
            case 5:
                crop.crop = Resources.Load<CropPreset>("CropPresets/RedVelvetBean");
                break;
            case 6:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Rice");
                break;
            case 7:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Sorghum");
                break;
            case 8:
                crop.crop = Resources.Load<CropPreset>("CropPresets/SugarCane");
                break;
            case 9:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Tobacco");
                break;
            case 10:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Tomatoes");
                break;
            default:
                crop.crop = Resources.Load<CropPreset>("CropPresets/Barren");
                break;
        }

        gameManager.cash -= crop.crop.cost;
        crop.cropAge = 0;
        crop.timesPlanted[crop.crop.idNum]++;
        Destroy(transform.GetChild(0).gameObject);
        Instantiate(crop.crop.prefab, this.transform);
        
        uIManager.UpdateUIText();
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
        uIManager.UpdateUIText();
    }

    public void HarvestField()
    {
        if (GetComponent<FieldProperties>().isCropRipe) //Probably not needed, checked twice
        {
            gameManager.ActionRemaining(); //Add Tool Check for action amount
            float AmountHarvested = crop.size * crop.fieldHealth;
            AmountHarvested *= economyManager.currentCropPrices[crop.crop.idNum];
            gameManager.cash += (int) AmountHarvested;

            crop.crop = Resources.Load<CropPreset>("CropPresets/Barren");
            uIManager.UpdateUIText();

            crop.isCropRipe = false;
            Destroy(transform.GetChild(0).gameObject);
            Instantiate(crop.crop.prefab, this.transform);
        }
    }   
}
    