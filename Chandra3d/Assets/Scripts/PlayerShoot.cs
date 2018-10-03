using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject shotPrefab;

    public float shootCadency = 0.5f;
    public float shootPower = 1.0f;

    private float timeSinceLastShoot = float.MaxValue;

    private List<ShotController> shots;

    // Use this for initialization
    void Start ()
    {
        shots = new List<ShotController>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        timeSinceLastShoot += Time.deltaTime;

        if (Input.GetButton("Fire1") && timeSinceLastShoot > shootCadency)
        {
            // Shoot!
            GameObject newShot = Instantiate(shotPrefab, transform.position, transform.rotation);
            shots.Add(newShot.GetComponent<ShotController>());

            timeSinceLastShoot = 0.0f;
        }
    }

}

