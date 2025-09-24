using NUnit.Framework;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject model;
    public int speed;
    bool right;
    float prevVelo = 1000000;

    float maxTime = 300;
    float currentTimer = 0;

    bool Priority = false;
    
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
        if (rb.linearVelocity.x > 0 && this.transform.localScale == new Vector3(-newScale.x, newScale.y, newScale.z)) //rotate fish
        {
            this.transform.localScale = new Vector3(newScale.x, newScale.y, newScale.z);

        }
        if (rb.linearVelocity.x < 0 && this.transform.localScale == new Vector3(newScale.x, newScale.y, newScale.z)) //rotate fish
        {
            this.transform.localScale = new Vector3(-newScale.x, newScale.y, newScale.z);
            //transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        prevVelo = rb.linearVelocity.x;

        

        if (currentTimer <= maxTime)
        {
            currentTimer += 10 * Time.deltaTime;
        }
        else if (currentTimer >= maxTime)
        {
            Priority = true;
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        var rand = Random.Range(0, 3);
        if (rand == 1)
        {
            Debug.Log("switching up");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * Random.Range(0.7f, 1.3f), rb.linearVelocity.y * Random.Range(0.7f, 1.3f));
        }
        
        rb.linearVelocity = rb.linearVelocity.normalized * speed * Random.Range(0.7f, 1.4f);
        if (collision.gameObject.CompareTag("Fish"))
        {
            Debug.Log("Collide");
            if (Priority)
            {
                Debug.Log("Collided with " + collision.gameObject);
                if (collision.gameObject.TryGetComponent(out FishMovement comp))
                {
                    Debug.Log("Disabled other");
                    comp.Priority = false;
                }

                rand = Random.Range(0, 3);
                switch (rand)
                {
                    case 0:
                        Instantiate(collision.gameObject);
                        break;
                    case 1:
                        Instantiate(collision.gameObject);
                        break;
                    case 2:

                        Destroy(collision.gameObject);
                        break;
                }
                currentTimer = 0;
                Priority = false;

                return;
            }
        }
    }
}
