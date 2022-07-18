using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Improvements Preset", menuName = "New Improvements Node")]
public class ImprovementsTreeNode : ScriptableObject
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string DisplayName;
    [SerializeField]
    public int ImprovementTime;
    [SerializeField]
    public int ImprovementCost;
    public bool bIsUnlocked;

    [SerializeField]
    List<ImprovementsTreeNode> Children;
    [SerializeField]
    ImprovementsTreeNode Parent;

    public void unlock()
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
}