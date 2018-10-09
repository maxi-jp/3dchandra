using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject shotPrefab;

    /// <summary>
    /// Position where the bullets came out from the ship
    /// </summary>
    public Transform canonPoint;

    public float shootCadency = 0.5f;
    public float shootPower = 1.0f;

    private float timeSinceLastShoot = float.MaxValue;

    public int bulletPoolAmount = 10;

    private List<BulletController> bulletsPool;

    
    // Use this for initialization
    void Start ()
    {
        // create and fill the bullet pool
        bulletsPool = new List<BulletController>();
        for (int i = 0; i < bulletPoolAmount; i++)
        {
            GameObject obj = Instantiate(shotPrefab);
            obj.SetActive(false);
            obj.GetComponent<BulletController>().shooterTag = tag;
            bulletsPool.Add(obj.GetComponent<BulletController>());
        }

        if (!canonPoint)
            canonPoint = transform.Find("canon point").transform;
    }
    
    // Update is called once per frame
    void Update ()
    {
        timeSinceLastShoot += Time.deltaTime;

        if (Input.GetButton("Fire1") && timeSinceLastShoot > shootCadency)
        {
            // Shoot!
            bool freeBulletFound = false;
            int bulletIndex = 0;
            GameObject bulletFound = null;
            // search for a bullet in the pool
            while (!freeBulletFound && bulletIndex < bulletsPool.Count)
            {
                if (!bulletsPool[bulletIndex].gameObject.activeInHierarchy)
                {
                    bulletFound = bulletsPool[bulletIndex].gameObject;
                    freeBulletFound = true;
                }
                else
                    bulletIndex++;
            }

            if (freeBulletFound)
            {
                bulletFound.transform.position = canonPoint.position;
                bulletFound.transform.rotation = canonPoint.rotation;
                bulletFound.SetActive(true);

                timeSinceLastShoot = 0.0f;
            }
            else
            {
                //Debug.LogWarning("Unable to launch a new bullet: not an unactive bullet in the pool");
                Debug.Log("Unable to launch a new bullet: not an unactive bullet in the pool. Instantiating a new one.");

                GameObject obj = Instantiate(shotPrefab);
                bulletsPool.Add(obj.GetComponent<BulletController>());
                obj.GetComponent<BulletController>().shooterTag = tag;
                obj.transform.position = canonPoint.position;
                obj.transform.rotation = canonPoint.rotation;

                timeSinceLastShoot = 0.0f;
            }

        }
    }

}

