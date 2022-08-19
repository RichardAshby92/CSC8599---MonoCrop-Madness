using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Crop Preset", menuName = "New Crop Preset")]
public class CropPreset : ScriptableObject
{
    [field:SerializeField]
    public string DisplayName { get; set; }
    [field: SerializeField]
    public int IdNum { get; set; }
    [field: SerializeField]
    public int Cost { get; set; }
    [field: SerializeField]
    public int WaterUsedPerTurn { get; set; }
    [field: SerializeField]
    public int TurnsToGrow { get; set; }
    [field: SerializeField]
    public int SoilChange { get; set; }
    [field: SerializeField]
    public bool FoodCrop { get; set; }
    [field: SerializeField]
    public GameObject RipePrefab { get; set; }
    [field: SerializeField]
    public GameObject UnripePrefab { get; set; }
}
