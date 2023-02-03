    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handle the ship's inertia and maximum speed
/// </summary>
public class ShipMovement : ShipAction{    
    /// <summary>
    /// Called every cycle in FixedUpdate
    /// </summary>
    public override void ProcessFixedAction(){
        Impulse();
    }

    /// <summary>
    /// Stop all ship's movement 
    /// </summary>
    public override void StopAction(){
        if(controller.currentSpeed > 0){
            controller.currentSpeed = 0;
            controller.Rigbody.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Handle the intertia
    /// </summary>
    void Impulse(){
        if (controller.currentSpeed < controller.currentMaxSpeed){
            controller.currentSpeed = controller.Data.smoothingFactor == 0? controller.currentMaxSpeed : Mathf.Lerp(controller.currentSpeed, controller.currentMaxSpeed, 1/controller.Data.smoothingFactor);
        }
        controller.Rigbody.velocity = controller.currentDir.normalized * controller.currentSpeed;
    }
}
