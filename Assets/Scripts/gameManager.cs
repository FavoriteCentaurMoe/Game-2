using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    public static gameManager gm;

    public Transform playerPrefab;
    public Transform spawnPoint;
    private Transform cam;

    private Vector3 previousCamPosition;

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smooth = 1;


    [SerializeField]
    public static int lives = 5;

    private void Awake()
    {
        cam = Camera.main.transform;
    }


    // Use this for initialization
    void Start () {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>();
        }
    }


    public void RespawnPlayer()
    {
        if (lives >= 0)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }

    }

    public static void KillPlayer(playerScript player)
    {
        Destroy(player.gameObject);
        lives = lives - 1;
        if (lives <= 0)
        {
            lives = 5;
            //gm.EndGame(player);
            //return;
        }
        gm.RespawnPlayer();
    }

    public static void changeCheckpoint(Transform point)
    {
        gm.spawnPoint = point;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
