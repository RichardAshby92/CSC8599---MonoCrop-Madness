using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationNode : MonoBehaviour
{
    public InformationNode Child { get; set; }
    public InformationNode Parent { get; set; }

    public bool IsUnlocked { get; set; }
    public int Cost { get; set; }
    public string TitleText { get; set; }
    public string BodyText { get; set; }
}
