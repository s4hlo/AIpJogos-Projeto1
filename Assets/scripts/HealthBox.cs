using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            bool isHealed = collision.gameObject.GetComponent<EnemyMoviment>().Heal(1);
            if (isHealed)
            {

                Destroy(gameObject);
            }

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            bool isHealed = collision.gameObject.GetComponent<PlayerMovement>().Heal(20);
            if (isHealed)
            {

                Destroy(gameObject);
            }

        }
    }

}
