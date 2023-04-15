using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] private float radius = 3.0f;

    [SerializeField] private bool hasExploded = false;

    [SerializeField] private float delay = 3.0f;
    private float countdown;

    private List<Collider2D> overlappingDestructibles = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && !hasExploded)
        {
            CheckForDestructible();
            hasExploded = true;
        }
    }

    private void CheckForDestructible()
    {
        var contactFilter = new ContactFilter2D().NoFilter();

        if (Physics2D.OverlapCircle(transform.position, radius, contactFilter, overlappingDestructibles) > 0)
        {
            foreach (Collider2D colliding in overlappingDestructibles)
            {
                Destroy(gameObject);

                if (colliding.GetComponent<EnemyThings>())
                {
                    Debug.Log("BOOM!");
                    EnemyThings enemyHealth = colliding.GetComponent<EnemyThings>();
                    enemyHealth.TakeHit(5);
                }
            }
        }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
