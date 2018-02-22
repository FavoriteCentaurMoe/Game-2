using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {


    public Animator anim;


    public float speed;
    private float spood;
 

    private float speedMultipliyer;
    public int jumpIntensity;
    public float moveX;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public bool betterJump;
    public bool grounded;
    public bool walled = false;
    public bool facingRight = true;
    public bool wallJumpWasFacingRight = true;
    public bool wallJump = false;
    public bool isJumping;

    public bool gunOn = false;

    [SerializeField]
    public float groundRadius;
    private Rigidbody2D playerRiggy;
    private RaycastHit2D raycast;
    
    public float originOffset = 0.5f;
    public float raycastMaxDistance = 0.1f;

    public int health = 100;
  
    public bool wallSliding;
    public float wallSlideSpeed = 2f;


    public LayerMask whatIsGround;
    [SerializeField]
    private Transform[] groundPoints;

    public Vector2 wallClimb;
    public Vector2 wallLeap;
    //public Vector2 wallJump;

    public int fireRate = 0;

    public int damage = 10;

    public LayerMask notToHit;

    float timeToFire;


    public GameObject bullet;
    public GameObject gun;
    public int bulletSpeed; 

    private void Awake()
    {
        playerRiggy = GetComponent<Rigidbody2D>();
        fallMultiplier = 2.5f;
        lowJumpMultiplier = 2f;
        grounded = true;
        isJumping = false;
        gun.SetActive(false);
    }

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        Physics2D.queriesStartInColliders = false;

        bool thing = gameManager.gunStatus();

        if(thing)
        {
            Debug.Log("YUUUP");
            gunOn = true;
            gun.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y <= -50)
        {
            DamagePlayer(200);
        }
    }

    public void DamagePlayer(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            gameManager.KillPlayer(this);
        }
    }

    RaycastHit2D CheckRaycast(Vector2 direction)
    {
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 test = new Vector2(direction.x * raycastMaxDistance, direction.y);
        //Debug.DrawRay(startingPosition, test, Color.red);       

        return Physics2D.Raycast(startingPosition, direction, raycastMaxDistance,whatIsGround);
    }

    bool IsGrounded()
    {
        float directionOriginOffset = originOffset * (Vector2.down.y > 0 ? 1 : -1);
        // Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y + directionOriginOffset);
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y );
       // Vector2 test = new Vector2(Vector2.down.x * raycastMaxDistance, Vector2.down.y);
        //Debug.DrawRay(startingPosition, test, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(startingPosition, Vector2.down,0.5f);
        if (hit)
        {
            //Debug.Log(hit.transform.tag);

            transform.parent = hit.transform.tag == "Moving Platform" ? hit.transform.parent.transform : null;



            wallJump = true;
            return true;
        }
        else
        {
            transform.parent = null;
            return false;
        }
    }

    bool IsWalled()
    {
        if (raycast)
        {
            return true;
        }
        return false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "gun")
        {
             gunOn = true;
             gun.SetActive(true);
            gameManager.gunActive();
            Destroy(collision.gameObject);
        }

        if(collision.tag == "coin")
        {
            gameManager.moreCoins();
            Destroy(collision.gameObject);
        }
    }

    public void gunsON()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       // if(collision.transform.tag == "Moving Platform")
       // {
       //     Debug.Log("YUP");
       //     transform.parent = collision.transform;
       // }
        //isJumping = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

       // Debug.Log("NO");
       // if (collision.transform.tag == "Moving Platform")
       // {
       //     transform.parent = null;
       // }
    }

    void ifMovingP()
    {

    }

    public void effect()
    {
        float change = facingRight == true ? 0.0f : -1f;
        float rotChange = facingRight == true ? 1f : -1f;

        Vector3 pos = new Vector3(transform.position.x + (change * 3), transform.position.y, transform.position.z);
        Rigidbody2D clone;
      //  Instantiate(bullet, transform.position, transform.rotation);
        //clone.velocity = transform.TransformDirection(Vector3.forward * 10);
         clone = Instantiate(bullet, pos, transform.rotation).GetComponent<Rigidbody2D>();

        //clone.velocity = transform.TransformDirection(Vector3.forward * 100);
        clone.velocity = new Vector2( (bulletSpeed * rotChange) , clone.velocity.y);
      //  playerRiggy.velocity = new Vector2(moveX * speed * speedMultipliyer, playerRiggy.velocity.y);

    }
    public void shoot()
    {
        if(gunOn == false)
        {
            return;
        }
        Vector2 direction = facingRight ? new Vector2(1, 0) : new Vector2(-1, 0);
        Vector2 startingPosition = new Vector2(transform.position.x , transform.position.y);
        Vector2 test = new Vector2(direction.x * 15f, direction.y);
        Debug.DrawRay(startingPosition, test, Color.red);       
        RaycastHit2D hit =  Physics2D.Raycast(startingPosition, direction, 15f, notToHit );
        effect();
        if(hit)
        {
            if (hit.transform.tag == "Enemy")
            {
                gameManager.KillEnemy(hit.transform.GetComponent<enemyScript>());
            }
        }
    }


    private void FixedUpdate()
    {

        grounded = IsGrounded();
        Vector2 direction = facingRight ? new Vector2(1, 0) : new Vector2(-1, 0);
        raycast = CheckRaycast(direction);
        walled = IsWalled();
    //    if(walled && !grounded && playerRiggy.velocity.y <= 0)
      //  {
           
        //    if(playerRiggy.velocity.y < -wallSlideSpeed)
          //  {
                
    //        }

  //      }
        move();
        BetterJump();
        ifMovingP();


        spood =  playerRiggy.velocity.x;

        if(spood == 0 && facingRight == true)
        {
           // Debug.Log("Yes");
            gun.transform.localScale = new  Vector3(-3f, 3f, 1f);
        }
        else
        {
            gun.transform.localScale = new Vector3(3f, 3f, 1f);
        }

        if(spood > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (spood < -0.1)
        {
            spood = -spood;
        
        }

        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                shoot();
            }

        }


        anim.SetFloat("spood", spood);
        anim.SetBool("grounded", grounded);


    }



    void BetterJump()
    {
        if (betterJump == true)
        {
            if (playerRiggy.velocity.y < 0)
            {
                //Debug.Log("Doing this ");
                playerRiggy.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (playerRiggy.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                playerRiggy.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
        //grounded = IsGrounded();
        if (grounded == false)
        {
            speedMultipliyer = 0.7f;
        }
        else
        {
            speedMultipliyer = 1f;
        }
    }
    void jump()
    {

        playerRiggy.velocity = Vector2.up * jumpIntensity;
        //playerRiggy.AddForce(Vector2.up * jumpIntensity);

    }
    void move()
    {
        moveX = Input.GetAxis("Horizontal");

        playerRiggy.velocity = new Vector2(moveX * speed * speedMultipliyer, playerRiggy.velocity.y);

       
        if (playerRiggy.velocity.x > 0)
        {
            facingRight = true;
        }
        if (playerRiggy.velocity.x < 0)
        {
            facingRight = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(grounded == true)
            {
                jump();
            }
            else if ((walled == true && wallJump)||(walled == true && (wallJumpWasFacingRight!=facingRight)))
            {
                jump();
                wallJump = false;
                wallJumpWasFacingRight = facingRight;
                StartCoroutine("TurnIt");
                //Debug.Log("HRY");
                playerRiggy.velocity = new Vector2(speed * raycast.normal.x, speed);
                StartCoroutine("TurnIt");
            }
            if( walled == true)
            {
              //  Debug.Log("HRY");
              //wallJump = false;
              //  wallJumpWasFacingRight = facingRight;
                //playerRiggy.velocity = new Vector2(-playerRiggy.velocity.x, playerRiggy.velocity.y);
            }
        }

    }

    IEnumerator TurnIt()
    {
      //  Debug.Log("Is this even doing shit");
        transform.localScale = transform.localScale.x == 1 ? new Vector2(1, -1) : Vector2.one;
        yield return new WaitForFixedUpdate();
        
      //  Debug.Log("Is thNOOOOOOOOOOOis even doing shit");

    }
}
