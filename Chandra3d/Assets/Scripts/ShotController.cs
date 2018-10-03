using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{

    public float velocity = 10.0f;
    public float damage = 1.0f;

    void Start ()
    {
        
    }
    
    void Update ()
    {
        transform.Translate(Vector3.right * velocity * Time.deltaTime);
    }

}
