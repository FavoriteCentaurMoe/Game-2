using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{

    public Animator anim;

    public bool close = true;

    public Sprite closed;

    public BoxCollider2D boxx;

    public void closeThis()
    {
        close = true;
       // anim.SetBool("close", close);
    }

    public void openThis()
    {
       // Debug.Log("Called");
        boxx.isTrigger = true;
        close = false;
       
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.GetComponent<SpriteRenderer>().sprite = closed;

        boxx = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("close", close);
    }
}
