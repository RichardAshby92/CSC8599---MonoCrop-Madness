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
        if(gameManagerObject)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            uIManager = gameManagerObject.GetComponent<UIManager>();
            economyManager = gameManagerObject.GetComponent<EconomyManager>();
            inventory = gameManagerObject.GetComponent<Inventory>();
            crop = GetComponent<FieldProperties>();
        }
    }

    public void Intialise()
    {
        if (gameManagerObject)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            uIManager = gameManagerObject.GetComponent<UIManager>();
            economyManager = gameManagerObject.GetComponent<EconomyManager>();
            inventory = gameManagerObject.GetComponent<Inventory>();
            crop = GetComponent<FieldProperties>();
        }
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

        //Check Planting Field Actions
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
                crop.crop = Resources.Load<CropPreset>("CropPresets/Wheat");
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

        int actionsNeeded = 2;

        if (inventory.tools[crop.crop.idNum])
        {
            actionsNeeded = 1;
        }

        if(gameManager.ActionRemaining(actionsNeeded))
        {
            gameManager.cash -= crop.crop.cost;
            crop.cropAge = 0;
            crop.timesPlanted[crop.crop.idNum]++;
            Destroy(transform.GetChild(0).gameObject);
            Instantiate(crop.crop.unripePrefab, this.transform);
            crop.CalculateMaterial();
            uIManager.UpdateUIText();
        }
        else
        {
            //Code if not enough actions remain
            print("Not Enough Actions");
        }
    }

    public void AddFertiliser()
    {
        if (gameManager.ActionRemaining(1))  //Should it take an action?
        {
            inventory.SubtractFromInventory(0);
            crop.soilQuality += 50;
            crop.CalculateSoilQuality();

            if (!inventory.isThereFertilizer)
            {
                uIManager.actionButtons[1].interactable = false;
            }
            //RainForest Damage Function
            uIManager.UpdateUIText();
        }
        else
        {
            //Code if no actions remain
            print("Not Enough Actions");
        }
    }

    public void HarvestField()
    {
        if (GetComponent<FieldProperties>().isCropRipe) //Probably not needed, checked twice
        {
            if(gameManager.ActionRemaining(1))
            {
                float AmountHarvested = crop.size * crop.fieldHealth;
                AmountHarvested *= economyManager.currentCropPrices[crop.crop.idNum];
                gameManager.cash -= ((int)AmountHarvested);

                crop.crop = Resources.Load<CropPreset>("CropPresets/Barren");
                uIManager.UpdateUIText();

                crop.isCropRipe = false;
                Destroy(transform.GetChild(0).gameObject);
                Instantiate(crop.crop.unripePrefab, this.transform);
                crop.CalculateMaterial();
            }
            else
            {
                //Code if no actions remain
                print("Not Enough Actions");
            }
        }
    }   
}
    