using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityManager : MonoBehaviour
{
    public static CommunityManager inst;

    private GameManager gameManager;

    [field: SerializeField]
    public int communityHealth { get; set; }
    [field: SerializeField]
    public int TopLimit { get; set; }

    [SerializeField]
    private CommunityPresets[] CommunityImprovement;
    [SerializeField]
    private Button[] CommunityButtons;

    private TerrainLayer[] TerrainLayers;
    [SerializeField]
    GameObject Lake;
    [SerializeField]
    GameObject River;

    [SerializeField]
    Vector3[] BuildingSpawnPositions; 

    [SerializeField]
    private Texture2D GoodGrass;
    [SerializeField]
    private Texture2D AverageGrass;
    [SerializeField]
    private Texture2D DeadGrass;
    [SerializeField]
    private Material GoodWater;
    [SerializeField]
    private Material AverageWater;
    [SerializeField]
    private Material BadWater;

    void Awake()
    {
        inst = this;
        gameManager = GetComponent<GameManager>();

        TerrainLayers = Terrain.activeTerrain.terrainData.terrainLayers;
        TerrainLayers[0].diffuseTexture = AverageGrass;

        int tempNum = 0;
        foreach (Button button in CommunityButtons)
        {
            tempNum++;
            button.onClick.AddListener(delegate { AddImprovement(tempNum); });
        }
       
    }

    public void CheckHealth()
    {
        communityHealth++;
        communityHealth = Mathf.Clamp(communityHealth, 0, TopLimit);

        switch (communityHealth)
        {
            case int n when n >= 80:
                ChangeHealth(GoodWater, GoodGrass);
                break;
            case int n when n >= 60:
                ChangeHealth(AverageWater, GoodGrass);
                break;
            case int n when n >= 40:
                ChangeHealth(AverageWater, AverageGrass);
                break;
            case int n when n >= 20:
                ChangeHealth(BadWater, AverageGrass);
                break;
            case int n when n >= 0:
                ChangeHealth(BadWater, DeadGrass);
                break;
            default:
                break;
        }
    }

    void ChangeHealth(Material water, Texture2D grass)
    {
        Lake.GetComponent<Renderer>().material = water;
        River.GetComponent<Renderer>().material = water;
        TerrainLayers[0].diffuseTexture = grass;
    }

    public void AddImprovement(int index)
    {
        gameManager.cash -= CommunityImprovement[index].cost;
        GameObject Building = CommunityImprovement[index].building;
        Instantiate(Building, BuildingSpawnPositions[index], transform.rotation);
    }
}
