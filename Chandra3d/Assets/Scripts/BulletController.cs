using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float velocity = 10.0f;
    public float damage = 1.0f;

    private int shotableMask;

    public string shooterTag;

    private void Start()
    {
        shotableMask = LayerMask.GetMask("shotable");
    }

    /// <summary>
    /// Called when the game object is set enable
    /// </summary>
    private void OnEnable ()
    {
        Invoke("Destroy", 2.0f);
    }

    private void OnDisable ()
    {
        CancelInvoke();
    }

    void Update ()
    {
        transform.Translate(Vector3.right * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
        //Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.tag != shooterTag)
        {
            CLife cLife = collider.GetComponent<CLife>();
            cLife.Damage(damage);
        }
    }

    private void Destroy ()
    {
        gameObject.SetActive(false);
    }

}
