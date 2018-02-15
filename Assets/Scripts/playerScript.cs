using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {




    public float speed;
    
 

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
    private bool isJumping;

    [SerializeField]
    public float groundRadius;
    private Rigidbody2D playerRiggy;
    private RaycastHit2D raycast;
    
    public float originOffset = 0.5f;
    public float raycastMaxDistance = 0.0001f;

    public int health = 100;
  
    public bool wallSliding;
    public float wallSlideSpeed = 2f;


    public LayerMask whatIsGround;
    [SerializeField]
    private Transform[] groundPoints;

    public Vector2 wallClimb;
    public Vector2 wallLeap;
    //public Vector2 wallJump;

    private void Awake()
    {
        playerRiggy = GetComponent<Rigidbody2D>();
        //speed = 10;
        fallMultiplier = 2.5f;
        lowJumpMultiplier = 2f;
        grounded = true;
        isJumping = false;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void DamagePlayer(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            // GameController.KillPlayer(this);

        }
    }

    RaycastHit2D CheckRaycast(Vector2 direction)
    {
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 test = new Vector2(direction.x * raycastMaxDistance, direction.y);
        Debug.DrawRay(startingPosition, test, Color.red);       

        return Physics2D.Raycast(startingPosition, direction, raycastMaxDistance,whatIsGround);
    }

    bool IsGrounded()
    {
        float directionOriginOffset = originOffset * (Vector2.down.y > 0 ? 1 : -1);
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y + directionOriginOffset);
        RaycastHit2D hit = Physics2D.Raycast(startingPosition, Vector2.down,0.01f);
        if (hit)
        {
            wallJump = true;
            return true;
        }
        else
        {
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


    private void FixedUpdate()
    {

        grounded = IsGrounded();
        Vector2 direction = facingRight ? new Vector2(1, 0) : new Vector2(-1, 0);
        raycast = CheckRaycast(direction);
        walled = IsWalled();
        if(walled && !grounded && playerRiggy.velocity.y <= 0)
        {
           
            if(playerRiggy.velocity.y < -wallSlideSpeed)
            {
              //  playerRiggy.velocity = new Vector2(playerRiggy.velocity.x, wallSlideSpeed);
                
            }

        }
        move();
        BetterJump();




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


        //playerRiggy.AddForce(new Vector2(moveX * speed * speedMultipliyer, playerRiggy.velocity.y));
        if (Input.GetButtonDown("Jump"))
        {
            if(grounded == true)
            {
                isJumping = true;
                jump();
            }
            else if ((walled == true && wallJump)||(walled == true && (wallJumpWasFacingRight!=facingRight)))
            {
                Debug.Log("HRY");
                isJumping = true;
                jump();
                wallJump = false;
                wallJumpWasFacingRight = facingRight;
                int thing;
                if(!wallJumpWasFacingRight)
                {
                    thing = 40;
                }
                else
                {
                    thing = -40;
                }
                //playerRiggy.velocity = new Vector2(thing, playerRiggy.velocity.y);
                //playerRiggy.AddForce(new Vector2(thing*10, playerRiggy.velocity.y));
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
        yield return new WaitForFixedUpdate();
        transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;

    }
}
