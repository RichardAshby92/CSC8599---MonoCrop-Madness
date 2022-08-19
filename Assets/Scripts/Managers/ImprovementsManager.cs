using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImprovementsManager : MonoBehaviour
{
    public static ImprovementsManager S_inst;

    [SerializeField]
    private GameObject _gameManagerObject;

    private int _currentImprovement;
    private GameManager _gameManager;
    private UIManager _uiManager;

    private ImprovementsTreeNode[] _nodesData;
    [SerializeField]
    private Button[] _improvementButtons;
    [SerializeField]
    private TextMeshProUGUI[] _improvementTurnText;
    [SerializeField]
    private TextMeshProUGUI[] _improvementCostText;
    [SerializeField]
    private TextMeshProUGUI[] _improvementTitle;

    [SerializeField]
    private Material _finishedMaterial;
    [SerializeField]
    private Material _availableMaterial;
    [SerializeField]
    private Material _inProgressMaterial;
    [SerializeField]
    private Material _lockedMaterial;

    static Dictionary<int, ImprovementsTreeNode> S_IMPROVEMENT_NODES;
    public static int S_ROOT_ID = 0;

    void Awake()
    {
        S_inst = this;

        _gameManager = _gameManagerObject.GetComponent<GameManager>();
        _uiManager = _gameManagerObject.GetComponent<UIManager>();
        _currentImprovement = 0;

        _nodesData = Resources.LoadAll<ImprovementsTreeNode>("NodeData");
        S_IMPROVEMENT_NODES = new Dictionary<int, ImprovementsTreeNode>();

        foreach (ImprovementsTreeNode NodeData in _nodesData)
        {
            S_IMPROVEMENT_NODES[NodeData.ID] = NodeData;
            S_IMPROVEMENT_NODES[NodeData.ID].ResetValues();
        }

        foreach(Button button in _improvementButtons)
        {
            button.interactable = false;
        }

        for(int i = 0; i < _improvementTurnText.Length; i++)
        {
            _improvementTurnText[i].text = S_IMPROVEMENT_NODES[i].ImprovementTime.ToString();
            _improvementCostText[i].text = S_IMPROVEMENT_NODES[i].ImprovementCost.ToString();
            _improvementTitle[i].text = S_IMPROVEMENT_NODES[i].DisplayName;
        }

        UnlockNodes(S_IMPROVEMENT_NODES[S_ROOT_ID]);
        _improvementButtons[S_ROOT_ID].GetComponent<Image>().material = _finishedMaterial;

        for (int i = 0; i < _improvementButtons.Length; i++)
        {
            int tempNum = i; //Needed for C#
            _improvementButtons[i].onClick.AddListener(delegate { StartImprovement(tempNum); });
        }
    }

    public void AccessMenu()
    {
        _uiManager.DisableMenus();
        gameObject.SetActive(true);
    }

    public void ResearchImprovement()
    {          
        if(S_IMPROVEMENT_NODES[_currentImprovement].IsUnlock && !S_IMPROVEMENT_NODES[_currentImprovement].IsFinished)
        {
            S_IMPROVEMENT_NODES[_currentImprovement].ImprovementTime--;
            if(S_IMPROVEMENT_NODES[_currentImprovement].ImprovementTime == 0)
            {
                ApplyEffects(_currentImprovement);
                UnlockNodes(S_IMPROVEMENT_NODES[_currentImprovement]);
            }
        }
    }

    public void StartImprovement(int index) //index Passed from Button
    {
        foreach(ImprovementsTreeNode node in _nodesData)
        {
            if(!node.IsFinished && node.IsUnlock)
            {
                _improvementButtons[node.ID].GetComponent<Image>().material = _availableMaterial;
            }
        }

        _currentImprovement = index;
        _gameManager.Cash -= S_IMPROVEMENT_NODES[_currentImprovement].ImprovementCost;
        _improvementButtons[index].GetComponent<Image>().material = _inProgressMaterial;
    }

    void ApplyEffects(int index)
    {
        S_IMPROVEMENT_NODES[index].IsFinished = true;
        ImprovementNodeActioners.Apply(index-1);
        _improvementButtons[index].GetComponent<Image>().material = _finishedMaterial;
    }

    void UnlockNodes(ImprovementsTreeNode Node)
    {
        foreach (ImprovementsTreeNode NodeData in Node.Children)
        {
            NodeData.IsUnlock = true;
            _improvementButtons[NodeData.ID].interactable = true;
            _improvementButtons[NodeData.ID].GetComponent<Image>().material = _availableMaterial; 
        }
    }
}

