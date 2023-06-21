using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] public Transform ballHolder; // try making this private wihthout messing ballcollider(55)
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TMP_Text ballsText;

    private GameManager gameManager;
    private Transform player;
    private Transform currentBall;
    private Rigidbody2D currentRigidbody;
    private LineRenderer laser;

    float timer;

    private bool shooting = false;
    private bool animating = false;
    private int balls = 20;
    // private int ballsShooted = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player").transform;
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseMenu.activeSelf)
        {
            laser.enabled = false;
            return;
        }
        Vector2 mousePos = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        if(mousePos.y < -4.9f) mousePos = new Vector2(mousePos.x, -4.9f);

        if(!shooting && !animating) laser.enabled = true;
        else
        {
            laser.enabled = false;

            timer += Time.unscaledDeltaTime;
            if(timer > 5f)
            {
                timer = 0;
                try
                {
                    Time.timeScale *= 1.5f;
                }
                catch (System.Exception)
                {}
            }
        }
        
        laser.SetPosition(0, (Vector2)player.position);

        Vector2 dir = mousePos - (Vector2)player.position;
        RaycastHit2D hit1 = Physics2D.Raycast((Vector2)player.position, dir.normalized, 50);
        Vector2 reflect = Vector2.Reflect(dir.normalized, hit1.normal);
        RaycastHit2D hit2 = Physics2D.Raycast(hit1.point + reflect * .01f, reflect, 50);
        if(hit2)
        {
            laser.SetPosition(1, hit1.point);
            laser.SetPosition(2, hit2.point);
        } else
        {
            laser.SetPosition(1, hit1.point);
        }

        if(Input.GetButtonDown("Fire1"))
        {
            if(mousePos.y < 5) StartCoroutine(shoot(dir));
        }
    }

    // public int ballsShootedCount()
    // {
    //     return ballsShooted;
    // }

    public void addBall()
    {
        balls++;
        ballsText.text = "x" + balls;
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
        if(!shooting && !animating)
        {
            timer = 0;
            Time.timeScale = 1f;
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

    public void StartMoveSideways(float where)
    {
        // while(shooting) print(shooting); // aaaaaa
        StartCoroutine(ModeSideways(transform, where));
    }

    private IEnumerator ModeSideways(Transform player, float where)
    {
        animating = true;
        float timer = 0;
        Vector2 startPosition = player.position;
        Vector2 endPosition = new Vector2(where, -5f);

        while(timer < .5f)
        {
            timer += Time.deltaTime;
            // print(timer);
            player.position = Vector2.Lerp(startPosition, endPosition, timer / .5f);
            yield return null;
        }

        animating = false;
    }
}
