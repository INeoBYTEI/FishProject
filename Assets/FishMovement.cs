using UnityEngine;

public class FishMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject model;
    public int speed;
    bool right;
    float prevVelo = 1000000;

    bool Priority = true;

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
            
        }
        if (rb.linearVelocity.x < 0) //rotate fish
        {
            this.transform.localScale = new Vector3(-newScale.x, newScale.y, newScale.z);
            //transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        prevVelo = rb.linearVelocity.x;
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        rb.linearVelocity = rb.linearVelocity.normalized * speed * Random.Range(0.5f, 1.5f);
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

                var rand = Random.Range(0, 3);
                switch (rand)
                {
                    case 0:
                        comp.Priority = true;
                        comp.Priority = true;
                        Instantiate(collision.gameObject);
                        break;
                    case 1:
                        comp.Priority = true;
                        Instantiate(collision.gameObject);
                        break;
                    case 2:
                        comp.Priority = true;
                        Instantiate(collision.gameObject);
                        break;
                }
                return;
            }
        }
    }
}
