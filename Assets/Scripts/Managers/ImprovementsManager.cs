using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementsManager : MonoBehaviour
{
    public static ImprovementsManager inst;

    [SerializeField]
    private GameObject gameManagerObject;

    private int currentImprovement;
    private GameManager gameManager;
    private UIManager uIManager;

    ImprovementsTreeNode[] NodesData;
    Button[] ImprovementButtons;

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
        }

        foreach(Button button in ImprovementButtons)
        {
            button.interactable = false;
        }

        //Set Root Sate Finished (Locked)
        //Set Unlocked States
        ImprovementsManager.SetUnlockedNodes(Arr);

    }

    public void AccessMenu()
    {
        uIManager.DisableMenus();
        gameObject.SetActive(true);
    }

    public static void SetUnlockedNodes(int UnlockedNodeIDs)
    {
        foreach (int ID in UnlockedNodeIDs)
        {
            if (IMPROVEMENT_NODES.ContainsKey(ID))
            {
                IMPROVEMENT_NODES[ID].unlock();
            }
        }
    }

    public static void ResearchImprovement()
    {          
        /*if(!Improvements[currentImprovement].bIsComplete && currentImprovement != 0)
        {
            Improvements[currentImprovement].turnsRemaining--;
            if(Improvements[currentImprovement].turnsRemaining == 0)
            {
                Improvements[currentImprovement].bIsComplete = true;
                ApplyEffects(currentImprovement);
            }
        }*/

        //if unlock
            //then applyeffects
    }

    public void StartImprovement(int index) //index Passed from Button
    {
        currentImprovement = index;
        gameManager.cash -= IMPROVEMENT_NODES[currentImprovement].ImprovementCost;
        //Change Node Colour
    }

    void ApplyEffects(int index)
    {
        ImprovementNodeActioners.Apply(index);
        IMPROVEMENT_NODES[index].chi
    }
}

