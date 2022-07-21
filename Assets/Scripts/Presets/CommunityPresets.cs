using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Community Preset", menuName = "New Community Preset")]

public class CommunityPresets : ScriptableObject
{
    public int idNum;
    public string DisplayName;
    public int cost;
    public GameObject building;
}
