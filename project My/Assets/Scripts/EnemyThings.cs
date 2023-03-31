using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThings : MonoBehaviour
{
    [SerializeField] private float healthPoints = 5.0f;
    [SerializeField] private float appleDmg = 1.0f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            if (healthPoints > 0)
            {
                healthPoints -= appleDmg;
                Destroy(collision.gameObject);
            }
            if (healthPoints <= 0)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
