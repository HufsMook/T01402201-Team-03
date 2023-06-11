using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMotion : MonoBehaviour
{
    public float minDelay = 1f;
    public float maxDelay = 3f;
    public float objectSpeed = 5f;
    public float bounceForce = 10f;
    public bool isBouncing = false;
    public bool isMoving = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        float delay = GetRandomDelay();
        Invoke("StartBouncing", delay);
        Invoke("StartMoving", delay);
        Invoke("DestroyObject", delay + 5f);
    }

    private void Update()
    {
        if (isBouncing)
        {
            rb.velocity = new Vector2(rb.velocity.x, -objectSpeed);
        }

        if (isMoving)
        {
            rb.velocity = new Vector2(objectSpeed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isBouncing)
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            Invoke("DestroyObject", 5f);
        }

        if (collision.gameObject.CompareTag("Wall") && isMoving)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            Invoke("DestroyObject", 5f);
        }
    }

    private void StartBouncing()
    {
        isBouncing = true;
    }

    private void StartMoving()
    {
        isMoving = true;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private float GetRandomDelay()
    {
        return Random.Range(minDelay, maxDelay);
    }
}
