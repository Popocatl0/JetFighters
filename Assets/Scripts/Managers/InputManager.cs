using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Handle the inputs and trigger the actions for the ShipController
/// </summary>
public class InputManager : MonoBehaviour{
    public enum InputType { Player, AI}
    InputType type = InputType.Player;
    public ShipController controller{ get; private set;}

    #region INPUT_VALUES
    public Vector2 movement {get; private set;}
    public bool isFire {get; private set;}
    public bool isBoost {get; private set;}
    #endregion

    #region  INPUT_ACTIONS
    public InputActionAsset inputAsset;
    private InputActionMap playerControls;
    private InputAction inputMovement;
    private InputAction inputFire;
    private InputAction inputBoost;
    #endregion

    /// <summary>
    /// Assign a ship
    /// </summary>
    /// <param name="_controller"></param>
    public void SetController(ShipController _controller){
        controller = _controller;
        SetInputs();
    }
    
    /// <summary>
    /// Search a input map by ship/controller ID,
    /// and set the Input Values with its respective Input Action
    /// </summary>
    void SetInputs(){
        playerControls = inputAsset.FindActionMap(controller.ID);
        inputMovement = playerControls.FindAction("Turn");
        inputFire = playerControls.FindAction("Fire");
        inputBoost = playerControls.FindAction("Boost");

        inputMovement.performed += context => OnTurn(context.ReadValue<Vector2>());
        inputFire.performed += context => OnFire(context.performed);
        inputBoost.started += context => OnBoost(context.started);

        inputMovement.canceled += context => OnTurn(context.ReadValue<Vector2>());
        inputFire.canceled += context => OnFire(context.performed);

        playerControls.Enable();
    }

    /// <summary>
    /// Set if the inputs are get by a player controls or a AI
    /// </summary>
    /// <param name="_type"></param>
    public void SetType(InputType _type){
        type = _type;
        switch (type) {
            default:
            case InputType.Player:
                playerControls.Enable();
                break;
            case InputType.AI:
                playerControls.Disable();
                break;
        }
    }
    public void OnFire(bool val){
        isFire = val;
        controller.UpdateInput();
    }
    public void OnTurn(Vector2 dir){
        movement = dir;
        controller.UpdateInput();
    }

    public void OnBoost(bool val){
        isBoost = val;
        controller.UpdateInput();
        isBoost = false;
    }
}
