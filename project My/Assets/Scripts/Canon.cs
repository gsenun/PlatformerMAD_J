using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Canon Attributes")]
    [SerializeField] int missleCooldown;
    [SerializeField] GameObject misslePrefab;
    [SerializeField] GameObject barrelPosition;

    float timer;

    private void Update()
    {
        shootInSeconds();
    }

    private void shootInSeconds()
    {
        if (timer < missleCooldown)
        {
            timer += 1 * Time.deltaTime;
        }
        else
        {
            Instantiate(misslePrefab, barrelPosition.transform.position, Quaternion.identity);
            timer = 0;
        }
    }

    private void Start()
    {
        timer = 0;
    }
}
