using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Information : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;

    private GameManager _gameManager;
    private UIManager _uiManager;

    [SerializeField]
    private TextAsset _tipFile;
    private string[,] _tipString;

    private InformationNode[] _informationNodes;
    [SerializeField]
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
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
        _uiManager = _gameManagerObject.GetComponent<UIManager>();

        _tipString = LoadData.LoadCSVToStringArray(_tipFile);

        _informationNodes = GetComponentsInChildren<InformationNode>();

        for (int i = 0; i < _tipButtons.Length; i++)
        {
            int tempNum = i; //Needed for C#
            _tipButtons[i].interactable = false;
            _tipButtons[i].onClick.AddListener(delegate { BuyTip(tempNum); });
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

        transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
    }

    public void AccessMenu()
    {
        _uiManager.DisableMenus();
        gameObject.SetActive(true);
    }

    private void BuyTip(int iD)
    {
        if(!_informationNodes[iD].IsUnlocked)
        {
            _gameManager.Cash -= _informationNodes[iD].Cost;
            _informationNodes[iD].IsUnlocked = true;
            _tipButtons[iD].GetComponent<Image>().material = _unlockedColour;

            //Unlock Next Button
            if(iD + 1 == _tipButtons.Length)
            {
                return;
            }
            _tipButtons[iD + 1].interactable = true;
            _uiManager.UpdateUIText();
        }

        _tipImage.SetActive(true);
        _uiManager._level2Menu = true;
        _TitleText.text = _tipString[iD + 1, 2];
        _BodyText.text = _tipString[iD + 2, 3];
    }
}

