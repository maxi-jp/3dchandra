using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyController : EnemyController
{

    /// <summary>
    /// Position where the bullets came out from the ship
    /// </summary>
    public Transform canonPoint;

    public float chasingSpeed = 10.0f;
    public float shootingSpeed = 4.0f;
    
    public float shootCadency = 0.5f;
    public float shootPower = 1.0f;

    public float bulletsVelocity = 20.0f;

    private float timeSinceLastShoot = float.MaxValue;

    void Start ()
    {
        if (!canonPoint)
            canonPoint = transform.Find("canon point").transform;
    }
    

    void Update ()
    {
        timeSinceLastShoot += Time.deltaTime;

        if (timeSinceLastShoot > shootCadency)
        {
            GameObject newBulletGo = BulletPoolerScript.instance.GetPooledBullet();
            newBulletGo.transform.position = canonPoint.position;
            newBulletGo.transform.rotation = canonPoint.rotation;
            BulletController newBulletC = newBulletGo.GetComponent<BulletController>();
            newBulletC.velocity = bulletsVelocity;
            newBulletC.shooterTag = tag;

            // activate the bullet
            newBulletGo.SetActive(true);

            timeSinceLastShoot = 0.0f;
        }
    }

}
