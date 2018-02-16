using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour {


    public Sprite on;
    public Sprite off;

    private Transform test;

    private bool first;


	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = off;
        test = this.gameObject.GetComponent<Transform>();
        first = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player" && first == true)
        {
            gameManager.changeCheckpoint(test);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = on;
            first = false;
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
