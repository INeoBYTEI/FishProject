using UnityEngine;

public class FishMovement : MonoBehaviour
{
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(1, 1, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
        rb.linearVelocity = rb.linearVelocity * -1;
    }
}
