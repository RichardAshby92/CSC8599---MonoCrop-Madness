using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Information : MonoBehaviour
{
    public GameObject gameManagerObject;

    private GameManager gameManager;
    private UIManager uIManager;

    [SerializeField]
    private TextAsset _tipFile;
    private string[,] _tipString;

    private InformationNode[] _informationNodes;
    private Button[] _tipButtons;

    [SerializeField]
    private GameObject _tipImage;
    [SerializeField]
    private TextMeshProUGUI _TitleText;
    [SerializeField]
    private TextMeshProUGUI _BodyText;
    [SerializeField]
    private Material _unlockedColour;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        uIManager = gameManagerObject.GetComponent<UIManager>();

        _tipString = LoadData.LoadCSVToStringArray(_tipFile);
        _tipButtons = GetComponentsInChildren<Button>();

        _informationNodes = GetComponentsInChildren<InformationNode>();

        int buttonNumber = 0;
        foreach(Button button in _tipButtons)
        {
            button.interactable = false;
            button.onClick.AddListener(delegate { BuyTip(buttonNumber); });
        }
        _tipButtons[0].interactable = true;

        int counter = 1;
        foreach(InformationNode node in _informationNodes)
        {
            node.IsUnlocked = false;
            node.Cost = int.Parse(_tipString[counter, 1]);
            node.TitleText = _tipString[counter, 2];
            node.BodyText = _tipString[counter, 3];
            counter++;
        }
    }

    public void AccessMenu()
    {
        uIManager.DisableMenus();
        gameObject.SetActive(true);
    }

    private void BuyTip(int iD)
    {
        if(!_informationNodes[iD].IsUnlocked)
        {
            gameManager.cash -= _informationNodes[iD].Cost;
            _informationNodes[iD].IsUnlocked = true;
            _tipButtons[iD].GetComponent<Image>().material = _unlockedColour;

            //Unlock Next Button
            _tipButtons[iD + 1].interactable = false;
            //What if its the last button
        }

        _tipImage.active = true;
        _TitleText.text = _tipString[iD + 1, 2];
        _BodyText.text = _tipString[iD + 2, 3];


        //_informationNodes[iD].Child.
        //Child Bool is Unlockable
    }
}

