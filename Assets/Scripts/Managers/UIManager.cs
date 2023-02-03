using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the screens and UI elements
/// </summary>
public class UIManager : MonoBehaviour{
    static UIManager _instance;
    public static UIManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<UIManager> ();
                if (_instance == null){
                    GameObject obj = new GameObject ();
                    _instance = obj.AddComponent<UIManager> ();
                }
            }
            return _instance;
        }
    }
    public CanvasGroup Menu, Game, Pause;
    public Transform WindImg;

    public BarItem[] HealthBars, BoostBars;
    public ScoreItem[] ScoreText;

    Dictionary<string, BarItem> HealthHash, BoostHash;

    Dictionary<string, ScoreItem> ScoreHash;


    /// <summary>
    /// Initialize de UI elements Dictionaries
    /// </summary>
    void Start(){
        HealthHash = new Dictionary<string, BarItem>();
        BoostHash = new Dictionary<string, BarItem>();
        ScoreHash = new Dictionary<string, ScoreItem>();
        for (int i = 0; i < HealthBars.Length; i++){
            HealthHash.Add(HealthBars[i].ID, HealthBars[i]);
        }
        for (int i = 0; i < BoostBars.Length; i++){
            BoostHash.Add(BoostBars[i].ID, BoostBars[i]);
        }
        for (int i = 0; i < ScoreText.Length; i++){
            ScoreHash.Add(ScoreText[i].ID, ScoreText[i]);
        }
    }
    /// <summary>
    /// Fade a canvas, enable or disable the element based in the alpha value
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="time"></param>
    /// <param name="alphaEnd"></param>
    /// <returns></returns>
    IEnumerator Fade(CanvasGroup canvas, float time, float alphaEnd){
        if(alphaEnd > 0) canvas.gameObject.SetActive(true);
        float alphaStart = canvas.alpha;
        float timer = 0;
        while(timer < time){
            canvas.alpha = Mathf.Lerp(alphaStart, alphaEnd, timer/time);
            timer += Time.deltaTime;
            yield return null;
        }
        //canvas.alpha = alphaEnd;
        if(alphaEnd == 0) canvas.gameObject.SetActive(false);
    }
    /// <summary>
    /// Run a event after a delay time
    /// </summary>
    /// <param name="gameEvent"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator RunEvent(Action gameEvent, float delay){
        yield return new WaitForSeconds(delay);
        gameEvent();
    }
    /// <summary>
    /// Set the game screen
    /// </summary>
    /// <param name="ai"></param>
    public void StartGame(bool ai){
        StartCoroutine(Fade(Menu, 0.5f, 0));
        StartCoroutine(Fade(Game, 0.5f, 1));
        GameManager.Instance.StartMatch(ai);
    }
    /// <summary>
    /// Set the pause screen
    /// </summary>
    public void PauseGame(){
        StartCoroutine(Fade(Game, 0.5f, 0));
        StartCoroutine(Fade(Pause, 0.5f, 1));
        GameManager.Instance.Pause();
    }
    /// <summary>
    /// Resume the game screen after a pause
    /// </summary>
    public void ResumeGame(){
        StartCoroutine(Fade(Pause, 0.5f, 0));
        StartCoroutine(Fade(Game, 0.5f, 1));
        StartCoroutine(RunEvent(GameManager.Instance.Resume, 0.5f));
    }
    /// <summary>
    /// Back to the Main Menu 
    /// </summary>
    public void BackMenu(){
        StartCoroutine(Fade(Pause, 0.5f, 0));
        StartCoroutine(Fade(Menu, 0.5f, 1));
    }
    /// <summary>
    /// Resume the game screen with a reseted match
    /// </summary>
    public void ResetGame(){
        StartCoroutine(Fade(Pause, 0.5f, 0));
        StartCoroutine(Fade(Game, 0.5f, 1));
        StartCoroutine(RunEvent(GameManager.Instance.ResetMatch, 0.5f));
    }

    public void CloseGame(){
        Application.Quit();
    }

    /// <summary>
    /// Update the health bar of its respective player
    /// </summary>
    /// <param name="id"></param>
    /// <param name="val"></param>
    /// <param name="max"></param>
    public void UpdateHealth(string id, float val, float max){
        if(HealthHash.ContainsKey(id)){
            HealthHash[id].UpdateBar(val, max);
        }
    }
    /// <summary>
    /// Update the boots bar of its respective player
    /// </summary>
    /// <param name="id"></param>
    /// <param name="val"></param>
    /// <param name="max"></param>
    public void UpdateBoost(string id, float val, float max){
        if(HealthHash.ContainsKey(id)){
            BoostHash[id].UpdateBar(val, max);
        }
    }

    /// <summary>
    /// Update the score text of its respective player
    /// </summary>
    /// <param name="id"></param>
    /// <param name="val"></param>
    public void UpdateScore(string id, int val){
        if(ScoreHash.ContainsKey(id)){
            ScoreHash[id].UpdateScore(val);
        }
    }
    /// <summary>
    /// Update the wind indicator
    /// </summary>
    /// <param name="wind"></param>
    public void UpdateWind(Vector2 wind){
        WindImg.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, wind.normalized));
    }
}
