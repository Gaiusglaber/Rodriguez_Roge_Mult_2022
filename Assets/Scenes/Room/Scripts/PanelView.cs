using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelView : MonoBehaviour
{
    public TMPro.TMP_Text nameTxt = null;
    public TMPro.TMP_Text scoreValueTxt = null;

    public void Configure(Color color,string name)
    {
        nameTxt.text = name;
        nameTxt.color = color;
        scoreValueTxt.color = color;
        scoreValueTxt.text = "0";
    }
    public void UpdateValue(int value)
    {
        scoreValueTxt.text = value.ToString();
    }
}
