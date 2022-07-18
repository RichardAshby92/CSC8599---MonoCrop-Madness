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
}
