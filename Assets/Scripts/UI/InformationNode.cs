using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationNode : MonoBehaviour
{
    public InformationNode Child;
    public InformationNode Parent;

    public bool IsUnlocked;
    public int Cost;
    public string TitleText;
    public string BodyText;
}
