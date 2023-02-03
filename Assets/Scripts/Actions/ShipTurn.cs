using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that handle the direction of the ship
/// </summary>
public class ShipTurn : ShipAction {
    float turnDir;
    public override void Init(ShipController _contr){
        base.Init(_contr);
        turnDir = 0;
    }

    /// <summary>
    /// Called when a input is received
    /// </summary>
    public override void BeginAction(){
        BeginTurn();
    }
    /// <summary>
    /// Called every frame
    /// </summary>
    public override void ProcessAction(){
        Turn();
    }

    /// <summary>
    /// Check the input value and modify the ship's speed
    /// 0 => Max Speed
    /// 1 o -1 => Reduce Speed 
    /// </summary>
    public void BeginTurn(){
        turnDir = controller.Input.movement.x;
        controller.currentMaxSpeed = controller.Input.movement.magnitude>0? (controller.Data.friction * controller.Data.maxSpeed) : controller.Data.maxSpeed;
        controller.currentSpeed = controller.currentSpeed > controller.currentMaxSpeed? controller.currentMaxSpeed : controller.currentSpeed * controller.Data.friction;
    }
    /// <summary>
    /// Handle the ship's turn direcction :
    /// 0 => Not turn
    /// 1 => Clockwise turn
    ///-1 => Inverse Clockwise turn
    /// </summary>
    void Turn(){
        if(turnDir != 0){
            controller.currentDir = Quaternion.Euler(0,0, -turnDir * controller.Data.turnSpeed * Time.deltaTime) * controller.currentDir;
            this.transform.up = -controller.currentDir;
        }
    }
}
