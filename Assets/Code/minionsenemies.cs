using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionsenemies : MonoBehaviour
{
    public float speed = 5f; 
    public Vector2 punto1; 
    public Vector2 punto2; 

    private Vector2 direction;
    private Vector2 moveD;

    void Start()
    {
        direction = Vector2.right;
        moveD= direction * speed;
    }

    void Update()
    {
        transform.Translate(moveD * Time.deltaTime);

        if (transform.position.x > punto2.x || transform.position.x < punto1.x)
        {
            direction = -direction; 
            moveD = direction * speed; 
        }
    }
}