using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public SpawnerScript spawner;

    Rigidbody2D rb;

    public float speed;

    public bool killed = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!killed)
            rb.velocity = Vector2.left * speed + Vector2.up * rb.velocity.y;
        
        if (transform.position.x < spawner.birdTeleportPos) 
        {
            transform.position = Vector3.right * spawner.chunkWidth * spawner.numOfChunks + Vector3.up * transform.position.y;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!killed)
        {
            if (collision.gameObject.tag == "Player")
            {
                killed = true;
                rb.gravityScale = 1f;
            }
        }
    }
}
