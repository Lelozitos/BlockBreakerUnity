using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockCollider : MonoBehaviour
{
    private int hp;
    private TextMeshPro currentText;

    // Start is called before the first frame update
    void Start()
    {
        currentText = gameObject.GetComponentInChildren<TextMeshPro>();
        hp = int.Parse(currentText.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.name;

        if(name == "Ball(Clone)")
        {
            hp--;
            if(hp == 0)
            {
                Destroy(gameObject);
            }
            else currentText.text = "" + hp;
        }
    }
}
