using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    public static gameManager gm;

    public GameObject winGame;

    public Transform playerPrefab;
    public Transform spawnPoint;
    private int spawnRank = 0;
    private Transform cam;

    public bool gunOn = false;

    //public GameObject gun;


    private Vector3 previousCamPosition;

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smooth = 1;

    public static int coins = 0;

    [SerializeField]
    public static int lives = 5;

    private void Awake()
    {
        winGame.SetActive(false);
        cam = Camera.main.transform;
    }


    public static void moreCoins()
    {
        coins += 1;
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

    public static void openDoor(int num)
    {

    }

    public static void gunActive()
    {
        gm.gunOn = true;
        //gun.SetActive(true);
    }

    public static bool gunStatus()
    {
        return gm.gunOn;
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

    public static void changeCheckpoint(Transform point, int rank)
    {
        if (rank > gm.spawnRank)
        {
            gm.spawnPoint = point;
            gm.spawnRank = rank;
        }
    }

    public static void endGame()
    {
        Time.timeScale = 0f;
        gm.winGame.SetActive(true);
    }

    public static void KillEnemy(enemyScript enemy)
    {
        Destroy(enemy.gameObject);
    }




    // Update is called once per frame
    void Update () {
		
	}
}
