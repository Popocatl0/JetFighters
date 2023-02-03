using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handle the health and damages of the ship
/// </summary>
public class Health : MonoBehaviour
{
    public int currentHealth {get; private set;}
    ShipController  controller;

    /// <summary>
    /// Assign a ship 
    /// </summary>
    /// <param name="_controller"></param>
    public void SetController(ShipController _controller){
        controller = _controller;
        currentHealth = controller.Data.maxLife;
    }
    /// <summary>
    /// Restore all health
    /// </summary>
    public void Revive(){
        currentHealth = controller.Data.maxLife;
        UIManager.Instance.UpdateHealth(controller.ID, currentHealth, controller.Data.maxLife);
    }
    /// <summary>
    /// Reduce the health by the damage amount,
    /// if all health is reduced run the explosion animation
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>Is death or not</returns>
    public bool Damage(int damage){
        currentHealth -= damage;
        UIManager.Instance.UpdateHealth(controller.ID, currentHealth, controller.Data.maxLife);
        if(currentHealth <= 0){
            controller.SetEnabled(false);
            controller.Animator.SetBool("expl", true);
            return true;
        }
        return false;
    }
}
