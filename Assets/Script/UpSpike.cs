using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpSpike : MonoBehaviour
{
    public float speed;
    private Vector3 targetPos;
    public GameObject ways;
    public Transform[] wayPoints;
    private int pointIndex;
    private int pointCount;
    private int direction = 1;
    private bool isPlayerInside = false;
    private BoxCollider2D boxCollider2D;
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            if (transform.position == targetPos)
            {
                NextPoint();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Player has collided with the spike!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(capsuleCollider2D != null && capsuleCollider2D.IsTouching(collision.collider) )         
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.HandleHealth(true);
            }
        }
        if (boxCollider2D != null && boxCollider2D.IsTouching(collision.collider))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.HandleHealth(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;

        }
    }

    private void NextPoint()
    {
        if (pointIndex == pointCount - 1) // Arrived at the last point
        {
            direction = -1;
        }
        if (pointIndex == 0) // Arrived at the first point
        {
            direction = 1;
        }
        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
    }
}
