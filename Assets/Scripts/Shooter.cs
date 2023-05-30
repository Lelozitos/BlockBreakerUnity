using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] public Transform ballHolder; // try making this private wihthout messing ballcollider(55)

    private GameManager gameManager;
    private Transform player;
    private Transform currentBall;
    private Rigidbody2D currentRigidbody;

    private bool shooting = false;
    private int balls = 1;
    // private int ballsShooted = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            if(mousePos.y <= -5) mousePos = new Vector3(mousePos.x, -4.9f, 0);

            StartCoroutine(shoot(mousePos - player.position));
        }
    }

    // public int ballsShootedCount()
    // {
    //     return ballsShooted;
    // }

    public void addBall()
    {
        balls++;
    }

    public bool isShooting()
    {
        return shooting;
    }

    public void changeShooting()
    {
        shooting = !shooting;
    }

    private IEnumerator shoot(Vector2 dir)
    {
        if(!shooting)
        {
            shooting = true;
            // ballsShooted = balls;
            for(int i = 0; i < balls; i++)
            {
                currentBall = Instantiate(ball, ballHolder);
                currentBall.position = player.position;
                currentRigidbody = currentBall.GetComponent<Rigidbody2D>();
                currentRigidbody.velocity = new Vector2(dir.x, dir.y).normalized * 10;
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
