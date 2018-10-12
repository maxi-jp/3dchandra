using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLife : MonoBehaviour
{

    public float life = 100;
    public int points = 100;  // TODO this could be in a better component than the life component

    public void Damage (float damage)
    {
        life -= damage;

        if (life <= 0)
            SendMessage("Dead");
    }

}
