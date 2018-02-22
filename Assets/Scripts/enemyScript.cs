using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    public bool isRight;
    public int speed;
    public int health;
    public float raycastMaxDistance;
    public float raycastCheckUp;
    public float raycastCheckAhead;
    
    public Rigidbody2D rb;

    private Vector2 movement;
    private bool walled;
    private RaycastHit2D raycast;
    private RaycastHit2D goombaDeath;

    public bool previousDirection;

    public bool checkDown;

    public float height;

    public bool facingRight = false;

    public bool abnormal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            gameManager.KillPlayer(collision.transform.GetComponent<playerScript>());
        }
        if (collision.transform.tag == "Laser")
        {
            //gameManager.KillPlayer(collision.transform.GetComponent<playerScript>());
            DamageEnemy(100);
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
        previousDirection = isRight;
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
        if(abnormal)
        {
            if (raycast)
            {
                if (raycast.transform.tag == "Player")
                {
                    if (isRight)
                    {
                        movement = new Vector2(speed, rb.velocity.y);
                    }
                    else
                    {
                        movement = new Vector2(-speed, rb.velocity.y);
                    }

                    if (rb.velocity.x > 0)
                    {
                        transform.localScale = new Vector3(1f, 1f, 1f);
                        facingRight = true;
                    }
                    if (rb.velocity.x < 0)
                    {
                        facingRight = false;
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                }
            }
            else
            {
                return;
            }
        }


        if (isRight)
        {
            movement = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            movement = new Vector2(-speed, rb.velocity.y);
        }

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            facingRight = true;
        }
        if (rb.velocity.x < 0)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

      

    }

    private void grounded()
    {

        if (raycast)
        {
            Debug.Log(raycast.transform.tag);
            if (raycast.transform.tag != "Ground")
           {
              
                /*
               if (isRight)
               {
                   isRight = false;
                }
              else
              {
                    isRight = true;
              }
              */
            }
        }
        else
        {
            StartCoroutine("changeDirection");
        }
    }

    IEnumerator changeDirection()
    {
        Debug.Log("Uip");
        yield return new WaitForSeconds(0.5f);
        if (isRight)
        {
            Debug.Log("isRight is false");
            isRight = false;
        }
        else
        {
            Debug.Log("isRight is true");
            isRight = true;
        }
        yield return new WaitForSeconds(0.5f);

    }

    // Update is called once per frame
    void FixedUpdate () {

       /*
        if(checkDown)
        {
            raycast = CheckRaycast(Vector2.down);            
            grounded();
        }
        else
        {
    */
            Vector2 direction = isRight ? new Vector2(1, 0) : new Vector2(-1, 0);
        raycastMaxDistance = raycastCheckAhead;
            raycast = CheckRaycast(direction);
        raycastMaxDistance = raycastCheckUp;
            if(!checkDown)
            {
                goombaDeath = CheckRaycast(Vector2.up);
                if (goombaDeath)
                {
                    DamageEnemy(200);
                }
                goombaDeath = CheckRaycast(Vector2.up, (height / 2));
                if (goombaDeath)
                {
                    DamageEnemy(200);
                }
                goombaDeath = CheckRaycast(Vector2.up, (-height / 2));
                if (goombaDeath)
                {
                    DamageEnemy(200);
                }
            }
        raycastMaxDistance = raycastCheckAhead;

        Walled();
        //}


        

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
