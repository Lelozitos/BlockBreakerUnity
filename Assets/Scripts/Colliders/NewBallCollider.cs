using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBallCollider : MonoBehaviour
{
    private Shooter shooter;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        shooter = GameObject.FindWithTag("Player").GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string name = collision.gameObject.name;

        if(name == "Ball(Clone)")
        {
            shooter.addBall();
        }

        Destroy(gameObject);
    }
}
