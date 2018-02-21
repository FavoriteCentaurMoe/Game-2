using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    public bool isRight;
    public int speed;
    public int health;
    public float raycastMaxDistance = 0.1f;
    public Rigidbody2D rb;

    private Vector2 movement;
    private bool walled;
    private RaycastHit2D raycast;
    private RaycastHit2D goombaDeath;

    public float height;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            gameManager.KillPlayer(collision.transform.GetComponent<playerScript>());
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            gameManager.KillPlayer(collision.transform.GetComponent<playerScript>());
        }
    }

    public void DamageEnemy(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            gameManager.KillEnemy(this);
        }
    }

    // Use this for initialization
    void Start () {
        height = height = GetComponent<SpriteRenderer>().bounds.size.x;
        rb = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
	}

    RaycastHit2D CheckRaycast(Vector2 direction, float change = 0)
    {
        Vector2 startingPosition = new Vector2(transform.position.x + change, transform.position.y);
        Vector2 test = new Vector2(direction.x * raycastMaxDistance, direction.y);
        Debug.DrawRay(startingPosition, test, Color.red);

        return Physics2D.Raycast(startingPosition, direction, raycastMaxDistance);
    }


     void Walled()
    {
        if (raycast )
        {
           // Debug.Log(raycast.transform.tag);
            if(raycast.transform.tag == "Ground")
            {
                if (isRight)
                {
                    isRight = false;
                }
                else
                {
                    isRight = true;
                }
            }

        }      
    }

    private void Update()
    {
        if (isRight)
        {
            movement = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            movement = new Vector2(-speed, rb.velocity.y);
        }
    }


    // Update is called once per frame
    void FixedUpdate () {

       
        Vector2 direction = isRight ? new Vector2(1, 0) : new Vector2(-1, 0);
        raycast = CheckRaycast(direction);
        goombaDeath = CheckRaycast(Vector2.up);
        if(goombaDeath)
        {
            DamageEnemy(200);
        }
        goombaDeath = CheckRaycast(Vector2.up, (height/2));
        if (goombaDeath)
        {
            DamageEnemy(200);
        }
        goombaDeath = CheckRaycast(Vector2.up,(-height/2));
        if (goombaDeath)
        {
            DamageEnemy(200);
        }

        Walled();

        /*
        if (isRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        */

        rb.velocity = movement;

        if (transform.position.y <= -50)
        {
            DamageEnemy(200);
        }

    }
}
