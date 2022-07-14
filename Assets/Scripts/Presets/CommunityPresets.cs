using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Community Preset", menuName = "New Community Preset")]

public class CommunityPresets : ScriptableObject
{
    int idNum;
    string DisplayName;

    int cost;
    GameObject building;
}
