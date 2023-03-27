using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Range(0f, 999f)]
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
