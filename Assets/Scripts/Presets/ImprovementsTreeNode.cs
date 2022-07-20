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
    public int ImprovementCost;


    [SerializeField]
    private int IntialImprovementTime;
    [SerializeField]
    private bool bIntialIsUnlock;
    [SerializeField]
    private bool bIntialIsFinished;

    public int ImprovementTime;
    public bool bIsUnlock;
    public bool bIsFinished;

    [SerializeField]
    public List<ImprovementsTreeNode> Children;
    [SerializeField]
    public ImprovementsTreeNode Parent;
    public void ResetValues()
    {
        ImprovementTime = IntialImprovementTime;
        bIsUnlock = bIntialIsUnlock;
        bIsFinished = bIntialIsFinished;
    }
}