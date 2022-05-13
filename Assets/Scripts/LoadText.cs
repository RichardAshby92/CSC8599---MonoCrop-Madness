using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadText : MonoBehaviour
{
    public TextAsset textFile;

    public string[,] data;

    void Awake()
    {
        string allText = textFile.text; //load CSV File with Information
        string[] lines = allText.Split('\n'); //Split into Sections by line
        string[] columns = lines[0].Split(',');
        data = new string[lines.Length, columns.Length];

        for (int i = 0; i < lines.Length; i++) //Split Line into Catergories
        {
            columns = lines[i].Split(',');
            for (int j = 0; j < columns.Length; j++)
            {                
                data[i,j] = columns[j];
            }
        }
    }
}
