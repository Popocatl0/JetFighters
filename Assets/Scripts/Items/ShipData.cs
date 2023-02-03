using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "shipData", menuName = "JetFighters/Ship Data")]
public class ShipData : ScriptableObject
{
    [Header("Velocity")]
    [Tooltip("Maximum speed for the ship.")]
    public float maxSpeed;
    [Tooltip("How smooth or how much time takes to get the maximum speed. 0 for instant speed, greater values for more slow acceleration.")]
    public float smoothingFactor;

    [Header("Turn")]
    [Tooltip("Speed for turn the ship, Degrees per second.")]
    public float turnSpeed;
    [Tooltip("Speed reduction when turn. 0 for stop the ship, 1 mantain the maximum speed.")]
    [Range(0,1)] public float friction;

    [Header("Boost")]
    [Tooltip("Speed multiplicator for boost action.")]
    public float boostMultiplier;
    [Tooltip("Time that the boost is active.")]
    public float boostTime;
    [Tooltip("Time that the boost is inactive after use.")]
    public float boostRecoverTime;

    [Header("Damage")]
    [Tooltip("How many bullets shoots per second.")]
    public float shootRate;
    [Tooltip("Initial life at the start of each match.")]
    public int maxLife;
}

