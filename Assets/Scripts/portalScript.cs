using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalScript : MonoBehaviour {

    public int level;
    public bool last;
    public int rank;
    public Transform test;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(last)
            {
                gameManager.endGame();
            }
            else
            {
                Debug.Log("H");
                gameManager.changeCheckpoint(test, rank);
                gameManager.KillPlayer(collision.transform.GetComponent<playerScript>());
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
