using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {


    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;




	// Use this for initialization
	void Start () {
        // player = GameObject.FindGameObjectWithTag("Player");
        smoothSpeed = 15.0f;
    }
	
    



	// Update is called once per frame
	void FixedUpdate () {

        if(target == null)
        {
            
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }


        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            Vector3 desired = new Vector3(desiredPosition.x, Mathf.Clamp(desiredPosition.y, -13, Mathf.Infinity), desiredPosition.z);


            transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
        }
    }
}
