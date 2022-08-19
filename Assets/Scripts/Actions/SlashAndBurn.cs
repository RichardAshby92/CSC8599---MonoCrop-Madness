using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlashAndBurn : MonoBehaviour
{
    [SerializeField]
    private GameObject _newFieldPrefab;
    [SerializeField]
    private GameObject _gameManagerObject;
    [SerializeField]
    private GameObject _fieldHealthObject;
    [SerializeField]
    private GameObject _marketGameObject;

    private FieldProperties _fieldProperties;
    private FieldActions _fieldActions;
    private CommunityManager _communityManager;
    private UIManager _uiManager;
    private GameManager _gameManager;

    public static bool S_ImprovementUnlocked { get; set; }
    void Awake()
    {
        _communityManager = _gameManagerObject.GetComponent<CommunityManager>();
        _uiManager = _gameManagerObject.GetComponent<UIManager>();
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        _uiManager.DisableMenus();
        _uiManager.NewFieldMenu.SetActive(true);
        if (!S_ImprovementUnlocked)
        {
            _uiManager.NewFieldButton.interactable = false;
            return;
        }
        else
        {
            _uiManager.NewFieldButton.interactable = true;
        }

        _uiManager.NewFieldButton.onClick.AddListener(BurnField);
    }

    public void BurnField()
    {
        _communityManager.CommunityHealth -= 20;
        _communityManager.TopLimit -= 20;

        //Add Burn Effect
        _gameManager.RemainingActions -= 4;

        _newFieldPrefab = Instantiate(_newFieldPrefab, transform.position, transform.rotation);

        _newFieldPrefab.GetComponent<FieldProperties>().GameManagerObject = _gameManagerObject;
        _newFieldPrefab.GetComponent<FieldActions>().GameManagerObject = _gameManagerObject;
        _newFieldPrefab.GetComponent<FieldActions>().FieldHealthObject = _fieldHealthObject;
        _newFieldPrefab.GetComponent<FieldActions>().MarketGameObject = _marketGameObject;

        _newFieldPrefab.GetComponent<FieldProperties>().Intialise();
        _newFieldPrefab.GetComponent<FieldActions>().Intialise();

        _gameManager.AddField(_newFieldPrefab);

        Destroy(gameObject);
    }
}
