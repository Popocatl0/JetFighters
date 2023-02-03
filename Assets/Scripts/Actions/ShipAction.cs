using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class to be overriden for Ship's Actions
/// </summary>
public abstract class ShipAction : MonoBehaviour
{
    public bool actionEnabled {get; protected set;}
    protected ShipController controller;
    public virtual void Init(ShipController _contr){
        controller = _contr;
    }

    public virtual void SetActive(bool val=true){
        actionEnabled = val;
    }

    /// <summary>
    /// Method called when a input is recibed
    /// </summary>
    public virtual void BeginAction(){}
    /// <summary>
    /// Method called every frame in the Update
    /// </summary>
    public virtual void ProcessAction(){}
    /// <summary>
    /// Mecthod called every cycle in the FixedUpdate
    /// </summary>
    public virtual void ProcessFixedAction(){}
    /// <summary>
    /// Method called when the Action is disabled
    /// </summary>
    public virtual void StopAction(){}

}
