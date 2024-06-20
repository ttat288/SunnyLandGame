using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hole : MonoBehaviour
{
    private TilemapRenderer tmRender;
    private BoxCollider2D collider2D;

    private void Start()
    {
        tmRender = GetComponent<TilemapRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tmRender.enabled = false;

        }
    }
}
