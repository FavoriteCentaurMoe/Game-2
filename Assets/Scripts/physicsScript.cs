using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script exists so the game does not need to follow normal physics, it can be customized. 
public class physicsScript : MonoBehaviour {

    public float gravityModifier = 1f;
    public float minGroundNormalY = 0.65f;

    protected bool grounded;
    protected Vector2 velocity;
    protected Vector2 groundNormal;
    protected Rigidbody2D objectRiggy;

    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f; //A bit of padding to make sure this never is inside another collider
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);// this is a list of things that actually hit 

    private void OnEnable()
    {
        objectRiggy = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        contactFilter.useTriggers = false; //don't check collision against triggers 
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer)); //Getting a layer mask from project settings for Physics2D
        //Go to Project Settings -> Physics2D to change that 
        contactFilter.useLayerMask = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() //Horizontal and Vertical movement will have seperate calls to Movement to allow for slopes and such 
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        grounded = false; //Each frame starts with grounded false. In that frame there might be a collision with ground that changes this. 
        Vector2 deltaPosition = velocity * Time.deltaTime; //If Time.deltaTime is the time it takes for one frame to render, deltaPosition is the distance that something should move in one frame under this gravity 
        Vector2 move = Vector2.up * deltaPosition.y; // for vertical movement

        Movement(move,true); //This call is for y Movement

    }

    void Movement(Vector2 move, bool yMovement) 
    {
        //If distance is less then minimum, don't do anything
        //Otherwise, modify the distance IF the value is smaller than the shellSize (which means collision with a collider)
        float distance = move.magnitude;

        if(distance >minMoveDistance) //if it is moving at all, check for collisions. Otherwise don't bother
        {
            int count = objectRiggy.Cast(move, contactFilter, hitBuffer, distance + shellRadius); //Check in front of us (4th param), in the direction we are moving (move) for intems in (contactFilter)
            hitBufferList.Clear();
            for(int i = 0;  i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]); //taking all the info from hitBuffer and storing it onto a list 
            }
            for(int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal; 
                if(currentNormal.y > minGroundNormalY) //If the currentNormal.y is less than the minimum slope, we are on the ground.  
                {
                    grounded = true;
                    if(yMovement) 
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal); //Getting the difference between velocity and currentNormal. finding out if the velocity needs to be reduced before crashing into another collider. 
                if(projection < 0)
                {
                    velocity = velocity - projection * currentNormal; //Before making a collision, cancel out the velocity that would be changed by a collision
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        objectRiggy.position = objectRiggy.position + move.normalized * distance;
    }
}
