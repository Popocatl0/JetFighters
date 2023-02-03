using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Set score text
/// </summary>
public class ScoreItem : MonoBehaviour
{
    public string ID;
    public Text textScore;
    public string PointsPattern = "00";

    public void UpdateScore(int value){
        textScore.text = value.ToString(PointsPattern);
    }
}
