using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovRandowEnemy : MonoBehaviour
{
    public float speedE;
    public float range;
    public GameObject movementArea;

    private Vector2 wayPoint;
    public BoxCollider2D movSprite;

    void Start()
    {
        LimitSprite();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speedE * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            LimitSprite();
        }
    }

    void LimitSprite()
    {
        wayPoint = new Vector2(Random.Range(movSprite.bounds.min.x, movSprite.bounds.max.x), Random.Range(movSprite.bounds.min.y, movSprite.bounds.max.y));
    }

    public Vector2 GetCurrentPosition()
    {
        return transform.position;
    }

    void OnDrawGizmos()
    {
        if (movementArea != null)
        {
            Gizmos.color = new Color(1f, 1f, 1f, 0.3f);
            Gizmos.DrawIcon(transform.position, "Limit", true);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }

}