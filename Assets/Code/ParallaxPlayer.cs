using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 velocidad;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugadorRb;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().sharedMaterial;
        jugadorRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        offset = (jugadorRb.velocity.x * 0.1f) * velocidad * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
