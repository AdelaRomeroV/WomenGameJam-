using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 velocidad;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().sharedMaterial;
        rb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        offset = (rb2D.velocity.x * 0.1f) * velocidad * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
