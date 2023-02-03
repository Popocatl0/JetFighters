using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for handle a one time Impulse Action
/// </summary>
public class ShipBoost : ShipAction
{
    float currentRecoverTime;
    public override void Init(ShipController _contr){
        base.Init(_contr);
        currentRecoverTime = 0;
    }

    /// <summary>
    /// Restore the recover time and update de UI indicator
    /// </summary>
    public override void StopAction(){
        if(currentRecoverTime > 0){
            currentRecoverTime = 0;
            UIManager.Instance.UpdateBoost(controller.ID, controller.Data.boostRecoverTime, controller.Data.boostRecoverTime);
        }
    }

    /// <summary>
    /// When the input is received, boost the ship
    /// </summary>
    public override void BeginAction(){
        if(currentRecoverTime == 0 && controller.Input.isBoost)
            StartCoroutine(Boost());
    }
    /// <summary>
    /// Called every frame
    /// </summary>
    public override void ProcessAction(){
        Recover();
    }
    /// <summary>
    /// Update the recovery time and update the UI indicator
    /// </summary>
    void Recover(){
        if(currentRecoverTime > 0){
            currentRecoverTime -= Time.deltaTime;
            currentRecoverTime = Mathf.Clamp(currentRecoverTime, 0, controller.Data.boostRecoverTime);
            UIManager.Instance.UpdateBoost(controller.ID, controller.Data.boostRecoverTime - currentRecoverTime, controller.Data.boostRecoverTime);
        }
    }
    /// <summary>
    /// Modify the ship's maximum speed for a certain amount of time and start the recover time,
    /// after that restore de maximum speed
    /// </summary>
    /// <returns></returns>
    IEnumerator Boost(){
        currentRecoverTime = controller.Data.boostRecoverTime;
        controller.currentMaxSpeed = controller.Data.boostMultiplier * controller.Data.maxSpeed;
        controller.currentSpeed = controller.currentMaxSpeed;
        yield return new WaitForSeconds(controller.Data.boostTime);
        controller.currentMaxSpeed = controller.Data.maxSpeed;
        controller.currentSpeed = controller.currentMaxSpeed;
    }
}
