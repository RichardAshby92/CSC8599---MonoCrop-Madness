using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityManager : MonoBehaviour
{
    public static CommunityManager S_inst;

    private GameManager _gameManager;
    private UIManager _uiManager;

    [field: SerializeField]
    public int CommunityHealth { get; set; }
    [field: SerializeField]
    public int TopLimit { get; set; }

    [SerializeField]
    private CommunityPresets[] _communityImprovement;
    [SerializeField]
    private Button[] _communityButtons;

    private TerrainLayer[] _terrainLayers;
    [SerializeField]
    private GameObject _lake;
    [SerializeField]
    private GameObject _river;

    [SerializeField]
    private Texture2D _goodGrass;
    [SerializeField]
    private Texture2D _averageGrass;
    [SerializeField]
    private Texture2D _deadGrass;
    [SerializeField]
    private Material _goodWater;
    [SerializeField]
    private Material _averageWater;
    [SerializeField]
    private Material _badWater;

    void Awake()
    {
        S_inst = this;
        _gameManager = GetComponent<GameManager>();
        _uiManager = GetComponent<UIManager>();

        _terrainLayers = Terrain.activeTerrain.terrainData.terrainLayers;
        _terrainLayers[0].diffuseTexture = _averageGrass;   
    }

    public void CheckHealth()
    {
        CommunityHealth++;
        CommunityHealth = Mathf.Clamp(CommunityHealth, 0, TopLimit);

        switch (CommunityHealth)
        {
            case int n when n >= 80:
                ChangeHealth(_goodWater, _goodGrass);
                break;
            case int n when n >= 60:
                ChangeHealth(_averageWater, _goodGrass);
                break;
            case int n when n >= 40:
                ChangeHealth(_averageWater, _averageGrass);
                break;
            case int n when n >= 20:
                ChangeHealth(_badWater, _averageGrass);
                break;
            case int n when n >= 0:
                ChangeHealth(_badWater, _deadGrass);
                break;
            default:
                break;
        }
    }

    void ChangeHealth(Material water, Texture2D grass)
    {
        _lake.GetComponent<Renderer>().material = water;
        _river.GetComponent<Renderer>().material = water;
        _terrainLayers[0].diffuseTexture = grass;
    }

    public void AddImprovement(int index)
    {
        _gameManager.Cash -= _communityImprovement[index].Cost;
        CommunityHealth += 10;
        GameObject Building = _communityImprovement[index].Building;
        Instantiate(Building, _communityImprovement[index].Position, _communityImprovement[index].Rotation);
        _communityButtons[index].interactable = false;
        _uiManager.UpdateUIText();
    }
}
