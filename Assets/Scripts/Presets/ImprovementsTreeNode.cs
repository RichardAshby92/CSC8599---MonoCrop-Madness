using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Improvements Preset", menuName = "New Improvements Node")]
public class ImprovementsTreeNode : ScriptableObject
{
    [field:SerializeField]
    public int ID { get; set; }
    [field:SerializeField]
    public string DisplayName { get; set; }
    [field:SerializeField]
    public int ImprovementCost { get; set; }


    [field:SerializeField]
    private int IntialImprovementTime;
    [field:SerializeField]
    private bool bIntialIsUnlock;
    [field:SerializeField]
    private bool bIntialIsFinished;

    public int ImprovementTime { get; set; }
    public bool IsUnlock { get; set; }
    public bool IsFinished { get; set; }

    [SerializeField]
    public List<ImprovementsTreeNode> Children;
    [SerializeField]
    public ImprovementsTreeNode Parent;
    public void ResetValues()
    {
        ImprovementTime = IntialImprovementTime;
        IsUnlock = bIntialIsUnlock;
        IsFinished = bIntialIsFinished;
    }
}