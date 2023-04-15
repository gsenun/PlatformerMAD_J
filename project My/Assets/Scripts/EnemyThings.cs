using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThings : MonoBehaviour
{
    public float Hitpoints;
    public float MaxHitpoints = 5;

    void Start()
    {
        Hitpoints = MaxHitpoints;
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        if(Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
