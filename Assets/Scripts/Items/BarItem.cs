using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Update slider value
/// </summary>
public class BarItem : MonoBehaviour
{
    public string ID;
    public Slider fillBar;

    public void UpdateBar(float value, float maxVal){
        fillBar.value = value/maxVal;
    }
}
