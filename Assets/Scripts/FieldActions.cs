using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldActions : MonoBehaviour
{
    public GameObject ActionMenu;
    public GameObject gameManager;

    private void OnMouseDown()
    {
        //Use Scriptable Objects?
        ActionMenu.SetActive(true);
        //Highlight Effect
        //Transform canvas
    }

    public void PlantField() //Pass in delegate to tell it which crop to load
    {
        gameManager.GetComponent<GameManager>().ActionRemaining();

        /*if(GetComponent<FieldProperties>().crop.displayName == "Barren")
        {
            GetComponent<FieldProperties>().crop = Resources.Load<CropPreset>("CropPresets/SugcarCane");
        }*/
    }
    
}
