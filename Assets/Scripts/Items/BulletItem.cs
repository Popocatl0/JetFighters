using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the speed,direction and collision of the bullet 
/// </summary>
public class BulletItem : MonoBehaviour{
    public float speed;
    public float lifeDistance;
    Rigidbody2D rigbody;

    public delegate void OnCollisionDelegate(BulletItem item, ShipController target);
    public OnCollisionDelegate onCollision;

    float lifeTimer;

     void Awake(){
        rigbody = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// Check its life time and destroy it
    ///
    /// </summary>
    void Update(){
        if(lifeTimer > 0){
            lifeTimer -= Time.deltaTime;
        }
        if(lifeTimer <= 0){
            onCollision(this, null);
        }
        ModifyTrayectory();
    }
    /// <summary>
    /// Change the direction by wind influence
    /// </summary>
    void ModifyTrayectory(){
        if(WindManager.Instance.Direction.magnitude > 0){
            rigbody.velocity =  Vector2.Lerp(rigbody.velocity, rigbody.velocity + WindManager.Instance.Direction, 0.05f);
            this.transform.up = -rigbody.velocity.normalized;
        }
    }
    /// <summary>
    /// Initialize the direction and speed
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="pos"></param>
    public void Set(Vector3 dir, Vector3 pos){
        this.gameObject.SetActive(true);
        this.transform.position = pos;
        this.transform.up = -dir.normalized;
        rigbody.velocity = dir.normalized * speed;
        lifeTimer = lifeDistance/speed;
    }
    /// <summary>
    /// When collides with a player, call a delegate for handle the damage and score
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            onCollision(this, other.GetComponent<ShipController>());
        }
    }
}
