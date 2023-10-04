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
        Debug.Log("Health: " + health );
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player is dead");
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
    }

    public void Shoot()
    {
        GameObject closestEnemy = null;
        Vector3 direction = Vector3.forward;
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
            direction = (closestEnemy.transform.position - transform.position).normalized;

            if (ammo > 0)
            {
                ammo--;
                // Instantiate the bullet at a position slightly in front of the player
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

}
