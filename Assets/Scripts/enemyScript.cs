using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    public bool isRight;
    public int speed;
    public float raycastMaxDistance = 0.1f;
    public Rigidbody2D rb;

    private bool walled;
    private RaycastHit2D raycast; 

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

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
	}

    RaycastHit2D CheckRaycast(Vector2 direction)
    {
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
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

    // Update is called once per frame
    void Update () {

       
        Vector2 direction = isRight ? new Vector2(1, 0) : new Vector2(-1, 0);
        raycast = CheckRaycast(direction);
        Walled();

        if (isRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
		
	}
}
