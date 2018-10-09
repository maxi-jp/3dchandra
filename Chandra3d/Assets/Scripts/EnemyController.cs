using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Rigidbody2D rigidbody;

    public CLife life;

    public GameObject player;

    public float maxVelocity = 10.0f;
    public float pushPower = 100.0f;

    public float timeToPushShip = 1.0f;
    private float timeToPushShipAux = 0.0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (!life)
            life = GetComponent<CLife>();
    }

    // Use this for initialization
    void Start ()
    {
        
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (timeToPushShipAux <= 0.0f)
        {
            // push the ship

            Vector2 moveForce = player.transform.position - transform.position;

            Vector2 pushVector = moveForce.normalized * pushPower;
            rigidbody.AddForce(pushVector);

            Debug.DrawLine
            (
                transform.position,
                new Vector3
                (
                    pushVector.x + transform.position.x,
                    pushVector.y + transform.position.y,
                    transform.position.z
                ),
                Color.blue,
                0.5f
            );

            // velocity clamp
            float actualVelX = rigidbody.velocity.x;
            float actualVelY = rigidbody.velocity.y;
            actualVelX = Mathf.Clamp(actualVelX, -maxVelocity, maxVelocity);
            actualVelY = Mathf.Clamp(actualVelY, -maxVelocity, maxVelocity);

            rigidbody.velocity = new Vector2(actualVelX, actualVelY);

            timeToPushShipAux = timeToPushShip;
        }

        timeToPushShipAux -= Time.deltaTime;
    }

    public void Dead ()
    {
        Destroy(gameObject);
    }

}
