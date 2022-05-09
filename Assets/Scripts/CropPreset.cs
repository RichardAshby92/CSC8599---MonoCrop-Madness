using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Crop Preset", menuName = "New Crop Preset")]
public class CropPreset : ScriptableObject
{
    public string displayName;
    public int idNum;
    public int cost;

    public int waterUsedPerTurn;
    public int turnsToGrow;
    public int soilChange;

    public GameObject prefab;
}
