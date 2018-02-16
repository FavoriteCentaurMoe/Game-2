using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour {


    public Sprite fight;
    public Sprite chill;

    private float height;

   

    public float time;
    public LayerMask whatIsGround;





    private RaycastHit2D raycast;

    // Use this for initialization
    void Start () {
        StartCoroutine("changeMode");
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log("Oh the look the size is  " + height);
    }


    private void FixedUpdate()
    {
        raycast = CheckRaycast(Vector2.down);
        if(raycast)
        {
            if (raycast.transform.tag == "Ground" && this.gameObject.GetComponent<SpriteRenderer>().sprite != fight)
            {
                return;
            }
            if (raycast.transform.tag == "Player" && this.gameObject.GetComponent<SpriteRenderer>().sprite != fight)
            {
                gameManager.KillPlayer(raycast.transform.GetComponent<playerScript>());
            }
        }
    }

    RaycastHit2D CheckRaycast(Vector2 direction)
    {
        
        Vector2 startingPosition = new Vector2(transform.position.x , transform.position.y + (height / 2));
        Vector2 test = new Vector2(direction.x  , direction.y * height);
        Debug.DrawRay(startingPosition, test, Color.red);
        return Physics2D.Raycast(startingPosition, direction,height);
    }


    IEnumerator changeMode()
    {
        while (true)
        {

            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == fight)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = chill;
            }
            else if (this.gameObject.GetComponent<SpriteRenderer>().sprite == chill)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = fight;
            }

            yield return new WaitForSeconds(time);
        }
    }

    // Update is called once per frame
    void Update () {
        
    }
}
