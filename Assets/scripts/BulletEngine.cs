using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEngine : MonoBehaviour
{

    private void Update()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyMoviment>().Damage(1);
        }
    }
    
}
