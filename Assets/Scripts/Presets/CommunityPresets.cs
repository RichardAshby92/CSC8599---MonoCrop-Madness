using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Community Preset", menuName = "New Community Preset")]

public class CommunityPresets : ScriptableObject
{
    [field: SerializeField]
    public int idNum { get; set; }
    [field: SerializeField]
    public string DisplayName { get; set; }
    [field: SerializeField]
    public int cost {get; set;}
    [field: SerializeField]
    public GameObject building { get; set; }
    [field: SerializeField]
    public Vector3 Position{get; set;}
}
