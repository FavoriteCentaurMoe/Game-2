﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour {


    public Sprite on;
    public Sprite off;

    private Transform test;


    public int rank;


    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = off;
        test = this.gameObject.GetComponent<Transform>();
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player" )
        {
            gameManager.changeCheckpoint(test,rank);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = on;
           
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
