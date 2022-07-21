using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImprovementsManager : MonoBehaviour
{
    public static ImprovementsManager inst;

    [SerializeField]
    private GameObject gameManagerObject;

    private int currentImprovement;
    private GameManager gameManager;
    private UIManager uIManager;

    ImprovementsTreeNode[] NodesData;
    [SerializeField]
    Button[] ImprovementButtons;
    [SerializeField]
    TextMeshProUGUI[] ImprovementTurnText;
    [SerializeField]
    TextMeshProUGUI[] ImprovementCostText;
    [SerializeField]
    TextMeshProUGUI[] ImprovementTitle;

    [SerializeField]
    Material Finished;
    [SerializeField]
    Material Available;
    [SerializeField]
    Material InProgress;
    [SerializeField]
    Material Locked;

    static Dictionary<int, ImprovementsTreeNode> IMPROVEMENT_NODES;
    public static int ROOT_ID = 0;

    void Awake()
    {
        inst = this;

        gameManager = gameManagerObject.GetComponent<GameManager>();
        uIManager = gameManagerObject.GetComponent<UIManager>();
        currentImprovement = 0;

        NodesData = Resources.LoadAll<ImprovementsTreeNode>("NodeData");
        IMPROVEMENT_NODES = new Dictionary<int, ImprovementsTreeNode>();

        foreach (ImprovementsTreeNode NodeData in NodesData)
        {
            IMPROVEMENT_NODES[NodeData.ID] = NodeData;
            IMPROVEMENT_NODES[NodeData.ID].ResetValues();
        }

        foreach(Button button in ImprovementButtons)
        {
            button.interactable = false;
        }

        for(int i = 0; i < ImprovementTurnText.Length; i++)
        {
            ImprovementTurnText[i].text = IMPROVEMENT_NODES[i].ImprovementTime.ToString();
            ImprovementCostText[i].text = IMPROVEMENT_NODES[i].ImprovementCost.ToString();
            ImprovementTitle[i].text = IMPROVEMENT_NODES[i].DisplayName;
        }

        UnlockNodes(IMPROVEMENT_NODES[ROOT_ID]);
        ImprovementButtons[ROOT_ID].GetComponent<Image>().material = Finished;

        for (int i = 0; i < ImprovementButtons.Length; i++)
        {
            int tempNum = i; //Needed for C#
            ImprovementButtons[i].onClick.AddListener(delegate { StartImprovement(tempNum); });
        }
    }

    public void AccessMenu()
    {
        uIManager.DisableMenus();
        gameObject.SetActive(true);
    }

    public void ResearchImprovement()
    {          
        if(IMPROVEMENT_NODES[currentImprovement].bIsUnlock && !IMPROVEMENT_NODES[currentImprovement].bIsFinished)
        {
            IMPROVEMENT_NODES[currentImprovement].ImprovementTime--;
            if(IMPROVEMENT_NODES[currentImprovement].ImprovementTime == 0)
            {
                ApplyEffects(currentImprovement);
                UnlockNodes(IMPROVEMENT_NODES[currentImprovement]);
            }
        }
    }

    public void StartImprovement(int index) //index Passed from Button
    {
        foreach(ImprovementsTreeNode node in NodesData)
        {
            if(!node.bIsFinished && node.bIsUnlock)
            {
                ImprovementButtons[node.ID].GetComponent<Image>().material = Available;
            }
        }

        currentImprovement = index;
        gameManager.cash -= IMPROVEMENT_NODES[currentImprovement].ImprovementCost;
        ImprovementButtons[index].GetComponent<Image>().material = InProgress;
    }

    void ApplyEffects(int index)
    {
        IMPROVEMENT_NODES[index].bIsFinished = true;
        ImprovementNodeActioners.Apply(index);
        ImprovementButtons[index].GetComponent<Image>().material = Finished;
    }

    void UnlockNodes(ImprovementsTreeNode Node)
    {
        foreach (ImprovementsTreeNode NodeData in Node.Children)
        {
            NodeData.bIsUnlock = true;
            ImprovementButtons[NodeData.ID].interactable = true;
            ImprovementButtons[NodeData.ID].GetComponent<Image>().material = Available; 
        }
    }
}

