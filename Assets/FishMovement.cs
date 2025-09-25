using NUnit.Framework;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject model;
    FishTank tank;
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

        tank = gameObject.GetComponentInParent<FishTank>();
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
            rb.linearVelocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        }
        
        rb.linearVelocity = rb.linearVelocity.normalized * speed * Random.Range(0.7f, 1.4f);
        if (collision.gameObject.CompareTag("Fish"))
        {
            Debug.Log("Collide");
            if (Priority)
            {
                int range = 0;
                if (collision.gameObject.TryGetComponent(out FishMovement comp))
                {
                    Debug.Log("Disabled other");
                    comp.Priority = false;
                }
                if (tank.hunger >= 50)
                {
                    range = 10;
                }
                else if (tank.hunger >= 20)
                {
                    range = 20;
                }
                else
                {
                    range = 3;
                }
                Debug.Log("Collided with " + collision.gameObject);
                if (range == 3)
                {
                    rand = Random.Range(1, range);
                }
                else
                {
                    rand = Random.Range(0, range);
                }

                
                if (rand == 0)
                {
                    tank.RemoveFish(collision.gameObject);
                }
                else
                {
                    tank.AddFish(collision.gameObject.transform);
                }
                currentTimer = 0;
                Priority = false;

                return;
            }
        }
    }
}
