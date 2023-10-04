using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    private int health = 2;
    public Transform player;
    public float moveSpeed = 30.0f;
    private Rigidbody rb;

    Transform target;

    private enum State
    {
        CHASE_PLAYER,
        CHASE_HEALTH,
        IDLE,
    }

    private State currentState = State.IDLE;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        setState(1);
    }


    public void setState(int state)
    {
        // currentState = (State)state;

        if (state == 1) //chasing player
        {
            currentState = State.CHASE_PLAYER;
            target = player;
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (state == 2) // chasing health if not chasing player
        {
            currentState = State.CHASE_HEALTH;
            GetComponent<Renderer>().material.color = Color.black;
            target = GameObject.FindGameObjectWithTag("HealthBox").transform;
            if (target == null)
            {
                setState(1);
            }
        }
        if (state == 3) // idle
        {
            currentState = State.IDLE;
            target = null;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;

            if (rb.isKinematic == false)
            rb.velocity = moveDirection * moveSpeed;
        } else {
            setState(2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<PlayerMovement>().Damage(1);

            Destroy(gameObject);
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (health == 1)
        {
            setState(2); // TODO : state CHASE_HEALTH
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
            setState(1); // TODO : state CHASE_PLAYER
        }
        return true;
    }
}
