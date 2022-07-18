using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Improvements Preset", menuName = "New Improvements Node")]
public class ImprovementsTreeNode : ScriptableObject
{
    [SerializeField]
    int ID;
    [SerializeField]
    string DisplayName;
    [SerializeField]
    int ImprovementTime;
    [SerializeField]
    int ImprovementCost;
    bool bIsUnlocked;

    [SerializeField]
    List<ImprovementsTreeNode> Children;
    [SerializeField]
    ImprovementsTreeNode Parent;

    static Dictionary<int, ImprovementsTreeNode> IMPROVEMENT_NODES;

    public static int ROOT_ID = 0;

    public static void LoadImprovementsTree()
    {
        IMPROVEMENT_NODES = new Dictionary<int, ImprovementsTreeNode>();
        Dictionary<int, int> Parents = new Dictionary<int, int>();

        ImprovementsTreeNode[] NodesData = Resources.LoadAll<ImprovementsTreeNode>("NodeData");

        foreach(ImprovementsTreeNode NodeData in NodesData)
        {
            NodeData.Parent = null;
            NodeData.bIsUnlocked = false;

            IMPROVEMENT_NODES[NodeData.ID] = NodeData;
            foreach(ImprovementsTreeNode child in NodeData.Children)
            {
                Parents[child.ID] = NodeData.ID;
            }
        }

        foreach(KeyValuePair<int, int> p in Parents)
        {
            IMPROVEMENT_NODES[p.Key].Parent = IMPROVEMENT_NODES[p.Value];
        }

        IMPROVEMENT_NODES[ROOT_ID].bIsUnlocked = true;
    }

    public int[] GetUnlockedNodeIDs()
    {
        List<int> UnlockedIDs = new List<int>();

        foreach(KeyValuePair<int, ImprovementsTreeNode> p in IMPROVEMENT_NODES)
        {
            if(p.Value.bIsUnlocked)
            {
                UnlockedIDs.Add(p.Key);
            }
        }

        return UnlockedIDs.ToArray();
    }

    public static void SetUnlockedNodes(int[] UnlockedNodeIDs)
    {
        foreach(int ID in UnlockedNodeIDs)
        {
            if(IMPROVEMENT_NODES.ContainsKey(ID))
            {
                IMPROVEMENT_NODES[ID].unlock();
            }
        }
    }

    void unlock()
    {
        if (Parent != null && !Parent.bIsUnlocked)
        {
            return;
        }
        if(bIsUnlocked)
        {
            return;
        }

        bIsUnlocked = true;
    }

    void StartImprovement()
    {

    }
}