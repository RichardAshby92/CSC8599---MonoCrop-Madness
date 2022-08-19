using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FieldActions : MonoBehaviour
{
    [field:SerializeField]
    public GameObject GameManagerObject { get; set; }
    [field:SerializeField]
    public GameObject FieldHealthObject { get; set; }
    [field: SerializeField]
    public GameObject MarketGameObject { get; set; }

    private GameManager _gameManager;
    private EconomyManager _economyManager;
    private CommunityManager _communityManager;
    private UIManager _uiManager;
    private Market _market;
    private FieldHealth _fieldHealth;

    [SerializeField]
    private FieldProperties _crop;
    [SerializeField]
    private int _foodCropAffect;


    private void Awake()
    {
        Intialise();
    }

    public void Intialise()
    {
        if (GameManagerObject)
        {
            _gameManager = GameManagerObject.GetComponent<GameManager>();
            _uiManager = GameManagerObject.GetComponent<UIManager>();
            _economyManager = GameManagerObject.GetComponent<EconomyManager>();          
            _communityManager = GameManagerObject.GetComponent<CommunityManager>();
            _crop = GetComponent<FieldProperties>();
        }

        if (FieldHealthObject)
        {
            _fieldHealth = FieldHealthObject.GetComponent<FieldHealth>();
        }

        if(MarketGameObject)
        {
            _market = MarketGameObject.GetComponent<Market>();
        }
    }

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        _uiManager.DisableMenus();
        _uiManager.ActionMenu.SetActive(true);

        _uiManager.ActionButtons[0].onClick.AddListener(InspectCrop);
        _uiManager.ActionButtons[1].onClick.AddListener(PlantField);
        _uiManager.ActionButtons[2].onClick.AddListener(AddFertiliser);
        _uiManager.ActionButtons[3].onClick.AddListener(HarvestField);
        

        if (!_crop.IsCropRipe)
        {
            _uiManager.ActionButtons[3].interactable = false;
        }
        else
        {
            _uiManager.ActionButtons[3].interactable = true;
        }

        if(_market.MarketInventory.Fertiliser <= 0)
        {
            _uiManager.ActionButtons[2].interactable = false;
        }
        else
        {
            _uiManager.ActionButtons[2].interactable = true;
        }

        //Check Planting Field Actions
    }

    public void PlantField()
    {
        //Enable Menu of Crops
        for (int i = 0; i < _uiManager.CropMenuButtons.Length; i++)
        {
            int tempNum = i + 1; //Needed for C#
            _uiManager.CropMenuButtons[i].onClick.AddListener(delegate { PlantCrop(tempNum); });
            if (!ImprovementNodeActioners.CheckUnlocked(i))
            {
                _uiManager.CropMenuButtons[i].interactable = false;
            }
        }
        _uiManager.CropMenu.SetActive(true);
    }

    public void PlantCrop(int x)
    {

        switch (x)
        {
            case 1:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/Carnations");
                break;
            case 2:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/Cassava");
                break;
            case 3:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/Cotton");
                break;
            case 4:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/Maize");
                break;
            case 5:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/RedVelvetBean");
                break;
            case 6:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/Wheat");
                break;
            case 7:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/Sorghum");
                break;
            case 8:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/SugarCane");
                break;
            case 9:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/Tobacco");
                break;
            case 10:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/Tomatoes");
                break;
            default:
                _crop.crop = Resources.Load<CropPreset>("CropPresets/Barren");
                break;
        }

        int actionsNeeded = 2;

        if (_market.MarketInventory.Tools[_crop.crop.IdNum-1])
        {
            actionsNeeded = 1;
        }

        if(_gameManager.ActionRemaining(actionsNeeded))
        {
            _gameManager.Cash -= _crop.crop.Cost;
            _crop.CropAge = 0;
            _crop.TimesPlanted[_crop.crop.IdNum]++;
            Destroy(transform.GetChild(0).gameObject);
            Instantiate(_crop.crop.UnripePrefab, this.transform);
            _crop.CalculateMaterial();
            _uiManager.UpdateUIText();
        }
        else
        {
            //Code if not enough actions remain
            print("Not Enough Actions");
        }
    }

    public void AddFertiliser()
    {
        if (_gameManager.ActionRemaining(1) && _market.MarketInventory.Fertiliser > 0)
        {
            _market.MarketInventory.Fertiliser--;
            _crop._soilQuality += 50;
            _crop.CalculateSoilQuality();

            if (_market.MarketInventory.Fertiliser >= 0)
            {
                _uiManager.ActionButtons[1].interactable = false;
            }

            _communityManager.CommunityHealth--;
            _uiManager.UpdateUIText();
        }
        else
        {
            //Code if no actions remain
            print("Not Enough Actions");
        }
    }

    public void HarvestField()
    {
        if (GetComponent<FieldProperties>().IsCropRipe) //Probably not needed, checked twice
        {
            if(_gameManager.ActionRemaining(1))
            {
                float AmountHarvested =  _crop.FieldHealth;
                AmountHarvested *= _economyManager.CurrentCropPrices[_crop.crop.IdNum];
                _gameManager.Cash += ((int)AmountHarvested);

                _crop.crop = Resources.Load<CropPreset>("CropPresets/Barren");
                _uiManager.UpdateUIText();
                _crop.IsCropRipe = false;
                if(_crop.crop.FoodCrop)
                {
                    _communityManager.CommunityHealth += _foodCropAffect;
                }

                Destroy(transform.GetChild(0).gameObject);
                Instantiate(_crop.crop.UnripePrefab, this.transform);
                _crop.CalculateMaterial();
            }
            else
            {
                //Code if no actions remain
                //How To Telegraph
                print("Not Enough Actions");
            }
        }
    }

    public void InspectCrop()
    {
        if(_gameManager.ActionRemaining(1))
        {
            _uiManager.FieldHealthMenu.SetActive(true);
           _fieldHealth.Intialise(ref _crop);
        }      
    }
}
    