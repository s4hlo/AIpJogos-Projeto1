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
    }
}
