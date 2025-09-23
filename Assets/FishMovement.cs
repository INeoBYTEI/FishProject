using UnityEngine;

public class FishMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity.x > 0)
        {
        
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        rb.linearVelocity *= Random.Range(0.5f, 1.5f);
        rb.linearVelocity = rb.linearVelocity.normalized;
    }
}
