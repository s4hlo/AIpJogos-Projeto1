using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    private int health = 2;
    public Transform player;
    public float moveSpeed = 30.0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 moveDirection = (player.position - transform.position).normalized;

            rb.velocity = moveDirection * moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");

            Destroy(gameObject);
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }

        if (health == 1)
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
    }

    public bool Heal(int heal)
    {
        if (health == 2)
        {
            return false;
        }
        health += heal;
        if (health > 2)
        {
            health = 2;
        }
        if (health == 2)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        return true;
    }
}
