using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handle the transition to other state based 
/// on the method result
/// </summary>
public struct Transition{
    public delegate bool OnDecisionDelegate();
    public string NextID;
    public OnDecisionDelegate OnDecision;

    public Transition(string id, OnDecisionDelegate _onDecision){
        NextID = id;
        OnDecision = _onDecision;
    }
}

/// <summary>
/// Contain the methods and transitions of the state
/// </summary>
public class State{
    public delegate void OnStateEvent();
    [SerializeField] string _id;
    public Transition[] transitions;
    public OnStateEvent OnStart;
    public OnStateEvent OnPerform;
    public OnStateEvent OnExit;

    public string ID{
        get{ return _id;}
    }
    public string nextState {get; private set;}

    public State(string id, Transition[] _transitions){
        _id = id;
        transitions = _transitions;
    }
    /// <summary>
    /// Check all the transitions
    /// </summary>
    /// <returns>If there a change of state or not</returns>
    public bool ChangeState(){
        for (int i = 0; i < transitions.Length; i++){
            if(transitions[i].OnDecision()){
                nextState = transitions[i].NextID;
                return true;
            }
        }
        return false;
    }
}
/// <summary>
/// State Machine, handle the states flow 
/// </summary>
public class ShipIA : MonoBehaviour{
    public bool isEnabled {get; set;}
    State[] stateMachine;
    State currentState;
    public void Init(State[] _states){
        stateMachine = _states;
        isEnabled = true;
        if(stateMachine.Length > 0) 
            currentState = stateMachine[0];
    }

    void Update(){
        Run();
    }
    /// <summary>
    /// Perform the current state and check if exists a transition
    /// </summary>
    void Run(){
        if(!isEnabled || currentState == null) return;
        currentState.OnPerform?.Invoke();
        if(currentState.ChangeState()){
            GetState();
        }
    }
    /// <summary>
    /// Change the current state,
    /// and call the exit and start methods
    /// </summary>
    void GetState(){
        for (int i = 0; i < stateMachine.Length; i++){
            if(stateMachine[i].ID == currentState.nextState){
                currentState.OnExit?.Invoke();
                currentState = stateMachine[i];
                currentState.OnStart?.Invoke();
                break;
            }
        }
    }
}
