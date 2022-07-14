using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadData : MonoBehaviour
{
    public static string[,] LoadCSVToStringArray(TextAsset textAsset)
    {
        string allText = textAsset.text;
        string[] lines = allText.Split('\n');
        string[] columns = lines[0].Split(',');
        string[,] data = new string[lines.Length, columns.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            columns = lines[i].Split(',');
            for (int j = 0; j < columns.Length; j++)
            {
                data[i, j] = columns[j];
            }
        }

        return data;
    }

    public static Improvement[] LoadImprovementsData(TextAsset textAsset)
    {
        string[,] data = LoadCSVToStringArray(textAsset); 

        Improvement[] improvements =new Improvement[data.GetLength(0)-2];
        for(int i = 0; i < improvements.Length; i++)
        {
            improvements[i].id = int.Parse(data[i+1, 0]);
            improvements[i].name = data[i+1, 1];
            improvements[i].turnsRemaining = int.Parse(data[i+1, 2]);
            improvements[i].IntialCost = int.Parse(data[i+1, 3]);
            improvements[i].bIsComplete = bool.Parse(data[i+1, 4]);
        }
        return improvements;
    }

}
