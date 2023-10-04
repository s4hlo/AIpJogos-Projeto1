using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEngine : MonoBehaviour
{
    // if bullet collide with object with tag "Enemy" then destroy bullet and enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //reduce life of enemy
            collision.gameObject.GetComponent<EnemyMoviment>().Damage(1);
            Destroy(gameObject);
        }
    }
    
}
