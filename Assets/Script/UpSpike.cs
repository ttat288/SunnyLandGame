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
