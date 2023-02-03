using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Class for handle the Attack or Shoot 
/// </summary>
public class ShipAttack : ShipAction {
    [SerializeField] BulletItem bulletItem;
    public Transform shootPos;
    public int maxPoolSize;
    Stack<BulletItem> bulletPool;
    float shootTimer;

     public override void Init(ShipController _contr){
        base.Init(_contr);
        bulletPool = new Stack<BulletItem>(maxPoolSize);
        shootTimer = 0;
    }
    
    /// <summary>
    /// Only shoot if the input hold pressed
    /// </summary>
    public override void ProcessAction(){
        if(controller.Input.isFire)
            Shoot();
    }
    /// <summary>
    /// Check the timer and shoot one bullet from the pool
    /// </summary>
    void Shoot(){
        if(shootTimer > 0){
            shootTimer -= Time.deltaTime;
        }
        if(shootTimer <= 0){
            shootTimer = controller.Data.shootRate == 0? 0 : 1/controller.Data.shootRate;
            GetBullet();
        }
    }

    /// <summary>
    /// Get one bullet from the pool or create new one
    /// </summary>
    void GetBullet(){
        BulletItem bullet;
        if(!bulletPool.TryPop(out bullet)){
            bullet = Instantiate(bulletItem);
            bullet.onCollision += OnBulletCollision;
        }
        bullet.Set(controller.currentDir, shootPos.position);
    }

    /// <summary>
    /// Destroy the bullet and return it to the pool
    /// Also call the respective Damage and Score methods
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="target"></param>
    void OnBulletCollision(BulletItem bullet, ShipController target){
        if(target == controller) return;
        bullet.gameObject.SetActive(false);
        bulletPool.Push(bullet);
        if(target != null && target.Health.Damage(1)){
            controller.Score.AddPoint();
        }
    }
}
