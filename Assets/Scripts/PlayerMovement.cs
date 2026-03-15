using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public Rigidbody rb;
    private bool isGround = true;
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z​;
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGround)
        {
            rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
            isGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
}