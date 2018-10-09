using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolerScript : MonoBehaviour
{

    public static BulletPoolerScript instance;

    public GameObject pooledBullet;
    public int bulletPoolAmount = 100;
    public bool willGrow = true;

    List<GameObject> pooledBullets;

    private void Awake ()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        // create and fill the bullet pool
        pooledBullets = new List<GameObject>();
        for (int i = 0; i < bulletPoolAmount; i++)
        {
            GameObject obj = Instantiate(pooledBullet);
            obj.SetActive(false);
            pooledBullets.Add(obj);
        }
    }

    public GameObject GetPooledBullet ()
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (pooledBullets[i].activeInHierarchy)
                return pooledBullets[i];
        }

        // no unactive bullet in the pool: create a new one
        if (willGrow)
        {
            GameObject obj = Instantiate(pooledBullet);
            pooledBullets.Add(obj);
            return obj;
        }

        return null;
    }
}
