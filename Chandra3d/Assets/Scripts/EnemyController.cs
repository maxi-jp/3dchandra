using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    protected Rigidbody2D rigidbody;

    public CLife life;

    public GameObject player;

    public float maxVelocity = 10.0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (!life)
            life = GetComponent<CLife>();

        if (!player)
            player = GameController.instance.playerShip;
    }
    
    void Start ()
    {
        
    }

    public void Dead ()
    {
        // add points to game controller
        GameController.instance.PlayerScore += life.points;
        Destroy(gameObject);
    }

}
