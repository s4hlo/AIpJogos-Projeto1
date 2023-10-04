using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int health = 90;
    public int ammo = 3;
    private float moveSpeed = 10.0f;
    public GameObject bulletPrefab;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && ammo > 0)
        {
            Shoot();
        }

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = movement * moveSpeed;
            }
            else
            {
                transform.Translate(movement * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void Damage(int damage)
    {
        Debug.Log("Health: " + health + " Ammo: " + ammo);
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player is dead");
            // Application.Quit();
        }
    }

    public bool Heal(int heal)
    {
        if (health == 100)
        {
            return false;
        }
        health += heal;
        if (health > 100)
        {
            health = 100;
        }
        return true;
    }

    public void AddAmmo(int ammo)
    {
        this.ammo += ammo;
        Debug.Log("Health: " + health + " Ammo: " + ammo);
    }

    public void Shoot()
    {
        GameObject closestEnemy = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10.0f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                closestEnemy = collider.gameObject;
                break;
            }
        }

        if (closestEnemy != null)
        {
            Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;

            if (ammo > 0)
            {
                ammo--;
                Debug.Log("Health: " + health + " Ammo: " + ammo);
                Vector3 bulletSpawnPosition = transform.position + direction * 1.5f;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(direction * 1000);
            }
            else
            {
                Debug.Log("No ammo");
            }
        }

    }

    public (int, int) GetStatus()
    {
        return (health, ammo);
    }


}
