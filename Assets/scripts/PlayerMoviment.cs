using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

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
}
