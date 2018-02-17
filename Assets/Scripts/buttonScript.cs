using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour {

    public Sprite off;
    public Sprite on;


    public doorScript door;

    public int num;

    public bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = on;
            door.openThis();
        }
    }


    // Use this for initialization
    void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = off;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
