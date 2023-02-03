using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contain all the components of the ship
/// Manage the Actions Cycle
/// </summary>
public class ShipController : MonoBehaviour
{
    public Transform startPosition;
    [SerializeField] GameObject propulsor;
    [SerializeField] string _id;
    [SerializeField] ShipData _data;
    [SerializeField] InputManager _input;
    public Rigidbody2D Rigbody {get; private set;}
    public Collider2D Collider {get; private set;}
    public Animator Animator {get; private set;}
    public Health Health {get; private set;}
    public Score Score {get; private set;}
    public SpriteRenderer Render {get; private set;}
    public bool isEnabled {get; private set;}
    public Vector2 currentDir {get; set;}
    public float currentSpeed {get; set;}
    public float currentMaxSpeed {get; set;}
    public float currentScore {get; set;}

    Sprite initSprite;

    #region GETTERS 
    public ShipData Data{
        get{ return _data;}
    }

    public InputManager Input{
        get{ return _input;}
    }
    public String ID{
        get{ return _id;}
    }
    #endregion

    ShipAction[] actions;

    void Awake(){
        Rigbody = GetComponent<Rigidbody2D>();
        Health = GetComponent<Health>();
        Score = GetComponent<Score>();
        Collider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
        Render = GetComponent<SpriteRenderer>();
        initSprite = Render.sprite;
        currentDir = -this.transform.up;
        currentMaxSpeed = Data.maxSpeed;
        currentSpeed = 0;
        actions = GetComponents<ShipAction>();

        foreach(var act in actions){
            act.Init(this);
        }
        _input.SetController(this);
        Health.SetController(this);
        Score.SetController(this);

        if(startPosition != null) transform.position = startPosition.position;
    }

    /// <summary>
    /// Enable or disable itself and its actions
    /// </summary>
    /// <param name="val"></param>
    public void SetEnabled(bool val=true){
        isEnabled = val;
        Collider.enabled = val;
        foreach(var act in actions){
            act.SetActive(val);
            if(!val)
                act.StopAction();
        }
    }
    /// <summary>
    /// When a input is received, call the respective action's method
    /// </summary>
    public void UpdateInput(){
        if(!isEnabled) return;
        foreach(var act in actions){
            if(act.actionEnabled) act.BeginAction();
        }
    }
    /// <summary>
    /// Every frame call the respective action's method
    /// </summary>
    void Update(){
        if(!isEnabled) return;
        foreach(var act in actions){
            if(act.actionEnabled) act.ProcessAction();
        }
    }
    /// <summary>
    /// Every fixed cycle call the respective action's method
    /// </summary>
    void FixedUpdate(){
        if(!isEnabled) return;
        foreach(var act in actions){
            if(act.actionEnabled) act.ProcessFixedAction();
        }
    }
    /// <summary>
    /// Reset all components of the ship and enable it
    /// </summary>
    /// <param name="resetPos"></param>
    public void ResetObject(bool resetPos){
        gameObject.SetActive(true);
        propulsor.SetActive(true);
        Health.Revive();
        Animator.enabled = true;
        if(startPosition != null && resetPos) transform.position = startPosition.position;
        SetEnabled();
    }

    /// <summary>
    /// Destroy the propulsor or flame of the ship 
    /// in the explosion animation
    /// </summary>
    void DetroyPart(){
        propulsor.SetActive(false);
    }

    /// <summary>
    /// Called in the end of the explosion animation,
    /// disable the ship
    /// </summary>
    void DetroyObject(){
        GameManager.Instance.Rematch();
        Animator.SetBool("expl", false);
        Animator.enabled = false;
        Render.sprite = initSprite;
        gameObject.SetActive(false);
    }
}
