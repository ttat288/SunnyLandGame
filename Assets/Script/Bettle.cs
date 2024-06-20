using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bettle : Enemy
{
    [SerializeField] private Vector2[] points;
    [SerializeField] private int pointNumber = 0;
    [SerializeField] private float tolerance;
    [SerializeField] private float speed;
    [SerializeField] private float delayTime;
    private float delayStart;

    private void Start()
    {
        base.Start();
        if (points.Length > 0)
        {
            transform.position = points[0];
        }
      
    }
  

    private void Update()
    {
        if (transform.position != (Vector3)points[pointNumber])
        {
            Move();
        }
        else
        {
            UpdateTarget();
        }
    }

    private void Move()
    {
        // Calculate the direction vector
        Vector3 direction = (points[pointNumber] - (Vector2)transform.position).normalized;

        // Flip the Bettle sprite based on the direction
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Move the Bettle
        transform.position = Vector3.MoveTowards(transform.position, points[pointNumber], speed * Time.deltaTime);
    }


    private void UpdateTarget()
    {
        if (Time.time - delayStart > delayTime)
        {
            NextPlatform();
        }
    }

    private void NextPlatform()
    {
        pointNumber = (pointNumber + 1) % points.Length;
        delayStart = Time.time;
    }

}
