using UnityEngine;

public class FishMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject model;
    public int speed;
    bool right;
    float prevVelo = 1000000;

    Vector3 newScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, speed);
        newScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity.x > 0) //rotate fish
        {
            this.transform.localScale = new Vector3(newScale.x, newScale.y, newScale.z);
            Debug.Log(prevVelo);
        }
        if (rb.linearVelocity.x < 0) //rotate fish
        {
            this.transform.localScale = new Vector3(-newScale.x, newScale.y, newScale.z);
            //transform.rotation = Quaternion.Euler(180, 0, 0);
            Debug.Log("works 2");
            Debug.Log(prevVelo);

        }
        prevVelo = rb.linearVelocity.x;
    }
    void OnCollisionEnter(Collision collision)
    {
        rb.linearVelocity *= Random.Range(0.5f, 1.5f);
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }
}
