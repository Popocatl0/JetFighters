using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the score of the ship
/// </summary>
public class Score : MonoBehaviour
{
    public int currentScore {get; private set;}
    ShipController  controller;
    /// <summary>
    /// Assign a ship
    /// </summary>
    /// <param name="_controller"></param>
    public void SetController(ShipController _controller){
        controller = _controller;
    }
    /// <summary>
    /// Add one point when destroy another ship
    /// </summary>
    public void AddPoint(){
        currentScore++;
        UIManager.Instance.UpdateScore(controller.ID, currentScore);
    }
    /// <summary>
    /// Reset the score to zero
    /// </summary>
    public void ResetPoints(){
        currentScore = 0;
        UIManager.Instance.UpdateScore(controller.ID, currentScore);
    }
}
