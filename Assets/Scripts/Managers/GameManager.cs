using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the Core Game Loop
/// </summary>
public class GameManager : MonoBehaviour{
    ShipController[] players;
    ShipEnemy enemy;
    static GameManager _instance;
    public static GameManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager> ();
                if (_instance == null){
                    GameObject obj = new GameObject ();
                    _instance = obj.AddComponent<GameManager> ();
                }
            }
            return _instance;
        }
    }
    /// <summary>
    /// Search the players and enemy AI
    /// </summary>
    void Start(){
        enemy = FindObjectOfType<ShipEnemy>();
        players = FindObjectsOfType<ShipController>();
        for (int i = 0; i < players.Length; i++){
            players[i].SetEnabled(false);
        }
    }
    /// <summary>
    /// Start a new match, with new score
    /// </summary>
    /// <param name="val"></param>
    public void StartMatch(bool val){
        for (int i = 0; i < players.Length; i++){
            players[i].ResetObject(true);
            players[i].Score.ResetPoints();
        }
        WindManager.Instance.Init();
        enemy.SetEnemy(val);
    }
    /// <summary>
    /// Restart the match after one of the players is death
    /// </summary>
    public void Rematch(){
        StartCoroutine(GameManager.Instance.Restart(1.75f));
    }
    /// <summary>
    /// Restore the players settings
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator Restart(float delay){
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < players.Length; i++){
            players[i].ResetObject( !players[i].gameObject.activeSelf );
        }
    }
    /// <summary>
    /// Restart the match and set the score to zero
    /// </summary>
    public void ResetMatch(){
        for (int i = 0; i < players.Length; i++){
            players[i].ResetObject(true);
            players[i].Score.ResetPoints();
        }
        WindManager.Instance.Init();
    }
    /// <summary>
    /// Pause the players actions
    /// </summary>
    public void Pause(){
        foreach (var player in players){
            player.SetEnabled(false);
        }
        WindManager.Instance.Stop();
    }
    /// <summary>
    /// Resume the players actions
    /// </summary>
    public void Resume(){
         foreach (var player in players){
            player.SetEnabled(true);
        }
        WindManager.Instance.Play();
    }
}
