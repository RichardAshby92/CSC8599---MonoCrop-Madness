using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadText : MonoBehaviour
{
    public TextAsset textFile;
    

    void Start()
    {
        string test = textFile.text;
        string[] texts = test.Split("/n"); //load CSV File with Information
        string[] subTexts = test.Split(",");
    }
}
