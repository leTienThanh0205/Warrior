using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingAI : MonoBehaviour
{
    public float speed;
    public GameObject[] waypoints;
    private float disToPoint;
    private int nextWayPoint = 1;

    private void Update()
    {
        Move();
    }
    void Move()
    {
        disToPoint = Vector2.Distance(transform.position, waypoints[nextWayPoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, waypoints[nextWayPoint].transform.position,
            speed * Time.deltaTime);
        if(disToPoint < 0.2f){
            TakeTurn();
        }
    }
    void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += waypoints[nextWayPoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        ChosseNextWayPoint();
    }
    void ChosseNextWayPoint()
    {
        nextWayPoint++;
        if(nextWayPoint == waypoints.Length)
        {
            nextWayPoint = 0;
        }
    }
}

