using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static State;
using static Transition;

/// <summary>
/// Handle the construct a state object
/// </summary>
public class StateBuilder{
    State state;
    List<Transition> transitions;
    public StateBuilder SetState(string id){
        state = new State(id, null);
        return this;
    }

    public StateBuilder SetOnStart(OnStateEvent stateEvent){
        if(state != null){
            state.OnStart += stateEvent;
        }
        return this;
    }

    public StateBuilder SetOnPerform(OnStateEvent stateEvent){
        if(state != null){
            state.OnPerform += stateEvent;
        }
        return this;
    }

    public StateBuilder SetOnExit(OnStateEvent stateEvent){
        if(state != null){
            state.OnExit += stateEvent;
        }
        return this;
    }

    public StateBuilder AddTransition(string next, OnDecisionDelegate _onDecision){
        if(transitions == null)
            transitions = new List<Transition>();
        transitions.Add(new Transition(next, _onDecision));
        return this;
    }

    public State GetState(){
        if(state != null)
            state.transitions = transitions.ToArray();
        return state;
    }
}

/// <summary>
/// Handle the enemy AI 
/// </summary>
public class ShipEnemy : MonoBehaviour
{
    public float shootConeAnlge;
    [SerializeField] string targetID; 
    InputManager input;
    ShipIA machine;
    ShipController target;
    float currentAngle;

    void Start(){
        machine = GetComponent<ShipIA>();
        input = GetComponent<InputManager>();
        ShipController[] players = FindObjectsOfType<ShipController>();
        for (int i = 0; i < players.Length; i++){
            if(players[i].ID == targetID){
               target = players[i];
                break;
            }
        }
        CreateMachine();
    }
    /// <summary>
    /// Set the ship for a player or AI
    /// </summary>
    /// <param name="val"></param>
    public void SetEnemy(bool val){
        if(val){
            input.SetType(InputManager.InputType.AI);
            machine.enabled = true;
        }
        else{
            input.SetType(InputManager.InputType.Player);
            machine.enabled = false;

            input.OnBoost(false);
            input.OnFire(false);
            input.OnTurn(Vector2.zero);
        }
    } 
    /// <summary>
    /// Create the state machine to handle de AI
    /// (Turn State) <--> (Shoot State)
    /// </summary>
    void CreateMachine(){
        State turnState = new StateBuilder()
        .SetState("Turn")
        .SetOnPerform(TurnPerform)
        .SetOnExit(ExitTurn)
        .AddTransition("Shoot", TurnDecision)
        .GetState();

        State shootState = new StateBuilder()
        .SetState("Shoot")
        .SetOnStart(StartShoot)
        .SetOnExit(ExitShoot)
        .AddTransition("Turn", ShootDecision)
        .GetState();

        machine.Init( new State[]{turnState, shootState} );
    }

    /// <summary>
    /// TURN STATE -->
    /// If the ship points to the target at a certain angle -->
    /// SHOOT STATE
    /// </summary>
    /// <returns></returns>
    bool TurnDecision(){
        currentAngle = Vector2.SignedAngle(input.controller.currentDir, target.transform.position - input.controller.transform.position);
        return Mathf.Abs(currentAngle) <= (shootConeAnlge/2);
    }
    /// <summary>
    /// TURN STATE
    /// Every frame, turn the ship toward the target
    /// </summary>
    void TurnPerform(){
        input.OnTurn(Vector2.left * Mathf.Sign(currentAngle));
    }
    /// <summary>
    /// TURN STATE
    /// When change state, stop turn the ship
    /// </summary>
    void ExitTurn(){
        input.OnTurn(Vector2.zero);
    }
    /// <summary>
    /// SHOOT STATE -->
    /// If the ship isn't point to the target at a certain angle -->
    /// TURN STATE
    /// </summary>
    /// <returns></returns>
    bool ShootDecision(){
        currentAngle = Vector2.SignedAngle(input.controller.currentDir, target.transform.position - input.controller.transform.position);
        return (Mathf.Abs(currentAngle) > (shootConeAnlge/2));
    }
    /// <summary>
    /// SHOOT STATE
    /// At the start of the state, start shooting
    /// </summary>
    void StartShoot(){
        input.OnFire(true);
    }
    /// <summary>
    /// SHOOT STATE
    /// When change state, stop shooting
    /// </summary>
    void ExitShoot(){
        input.OnFire(false);
    }
}
