using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
{
    private Rigidbody2D currentRigidbody;
    private Shooter shooter;
    private GameManager gameManager;

    private static int returnedBalls;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentRigidbody = gameObject.GetComponent<Rigidbody2D>();
        shooter = GameObject.FindWithTag("Player").GetComponent<Shooter>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.name;

        if(name == "Ball(Clone)") return;
        if(name == "Bottom")
        {
            Destroy(gameObject);
            returnedBalls++;

            if(returnedBalls == 1)
            {
                float newLocal = collision.contacts[0].point.x;
                if(newLocal < -3.2f) newLocal = -3.2f;
                if(newLocal > 3.2f) newLocal = 3.2f;
                StartCoroutine(ModeSideways(shooter.GetComponent<Transform>(), newLocal));
            }

            // So ATUALIZA A POSICAO NO FINAL DO TURNO
            // TURNO COMECANDO ANTES

            // if(returnedBalls == shooter.ballsShootedCount())
            // {
            //     returnedBalls = 0;
            //     shooter.changeShooting();
            //     gameManager.nextLevel();
            // }

            if(shooter.ballHolder.GetComponent<Transform>().childCount == 1)
            {
                returnedBalls = 0;
                shooter.changeShooting();
                gameManager.nextLevel();
            }
        }
        else currentRigidbody.velocity = Vector2.Reflect(currentRigidbody.velocity, collision.GetContact(0).normal);
    }

    private IEnumerator ModeSideways(Transform block, float where)
    {
        float timer = 0;
        Vector2 startPosition = block.position;
        Vector2 endPosition = new Vector2(where, -5f);

        while(timer < .5f)
        {
            timer += Time.deltaTime;
            print(timer);
            block.position = Vector2.Lerp(startPosition, endPosition, timer / .5f);
            yield return null;
        }
    }
}
