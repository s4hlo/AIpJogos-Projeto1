using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
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
            // use addforce instead of velocity
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
}
