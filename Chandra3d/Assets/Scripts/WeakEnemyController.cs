using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyController : EnemyController
{

    /// <summary>
    /// Position where the bullets came out from the ship
    /// </summary>
    public Transform canonPoint;

    public float chasingSpeed = 2.0f;
    public float shootingSpeed = 4.0f;
    public float rotationSpeed = 2.0f;


    public float shootCadency = 0.5f;
    public float shootPower = 1.0f;

    public float bulletsVelocity = 20.0f;

    public float distanceToPlayerToEndChasing = 20.0f;
    private float distanceToPlayerToEndChasingSqr;

    private float timeSinceLastShoot = float.MaxValue;

    private bool chasing = true;

    void Start ()
    {
        if (!canonPoint)
            canonPoint = transform.Find("canon point").transform;

        distanceToPlayerToEndChasingSqr = distanceToPlayerToEndChasing * distanceToPlayerToEndChasing;
    }
    

    void Update ()
    {
        // movement

        // face the player
        transform.right = Vector3.Lerp
        (
            transform.right,
            player.transform.position - transform.position,
            Time.deltaTime * rotationSpeed
        );

        // compute if chasing or not
        float xdif = transform.position.x - player.transform.position.x,
              ydif = transform.position.y - player.transform.position.y;
        float sqrDistToPlayer = (xdif * xdif) + (ydif * ydif);

        chasing = (sqrDistToPlayer > distanceToPlayerToEndChasingSqr);

        if (chasing)
        {
            transform.position = Vector3.MoveTowards
            (
                transform.position,
                player.transform.position,
                chasingSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.RotateAround
            (
                player.transform.position,
                Vector3.forward,
                shootingSpeed * sqrDistToPlayer * 0.12f * Time.deltaTime
            );
            /*Vector3 desiredPosition = (transform.position - player.transform.position).normalized *
                distanceToPlayerToEndChasing + player.transform.position;
            Debug.Log("position: " + transform.position + ", desired: " + desiredPosition);
            transform.position = Vector3.MoveTowards
            (
                transform.position,
                desiredPosition,
                Time.deltaTime * 1.0f
            );*/
        }

        // shooting
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
