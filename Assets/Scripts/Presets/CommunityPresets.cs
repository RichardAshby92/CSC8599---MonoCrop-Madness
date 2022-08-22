using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Community Preset", menuName = "New Community Preset")]

public class CommunityPresets : ScriptableObject
{
    [field: SerializeField]
    public int IdNum { get; set; }
    [field: SerializeField]
    public string DisplayName { get; set; }
    [field: SerializeField]
    public int Cost {get; set;}
    [field: SerializeField]
    public GameObject Building { get; set; }
    [field: SerializeField]
    public Vector3 Position{get; set;}
    [field: SerializeField]
    public Quaternion Rotation { get; set; }

}
