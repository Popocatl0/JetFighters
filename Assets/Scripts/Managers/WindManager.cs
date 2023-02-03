using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets and update the direction of the wind
/// </summary>
public class WindManager : MonoBehaviour{
    static WindManager _instance;
    public static WindManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<WindManager> ();
                if (_instance == null){
                    GameObject obj = new GameObject ();
                    _instance = obj.AddComponent<WindManager> ();
                }
            }
            return _instance;
        }
    }
    public Vector2 Direction {get; private set;}
    float timer;
    bool isEnabled;

    /// <summary>
    /// Initialize the wind settings
    /// </summary>    
    public void Init(){
        isEnabled = true;
        SetWind();
    }

    /// <summary>
    /// Stop to change the wind directions
    /// </summary>
    public void Stop(){
        isEnabled = false;
    }
    /// <summary>
    /// Start to change the wind directions
    /// </summary>
    public void Play(){
        isEnabled = true;
    }
    /// <summary>
    /// Set a new wind direction
    /// </summary>
    void SetWind(){
        float force = Random.Range(-0.1f, 0.1f);
        int dir = Random.Range(0,2);
        Direction = dir==0? Vector2.right * force: Vector2.up * force;
        timer = Random.Range(20.0f, 90.0f);
        UIManager.Instance.UpdateWind(Direction);
    }
    /// <summary>
    /// Change the wind after a certain amount of time
    /// </summary>
    void ChangeWind(){
        timer -= Time.deltaTime;
        if(timer <= 0){
            SetWind();
        }
    }

    void Update(){
        if(isEnabled) ChangeWind();
    }

}
