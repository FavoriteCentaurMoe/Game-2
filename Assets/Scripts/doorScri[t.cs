using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSdcript : MonoBehaviour {

    public Animator anim;

    public bool close = true;

    public Sprite closed;

    public void closeThis()
    {
        close = true;
        anim.SetBool("close", close);
    }

    public void openThis()
    {
        close =  false;
        anim.SetBool("close", close);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        gameObject.GetComponent<SpriteRenderer>().sprite = closed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
