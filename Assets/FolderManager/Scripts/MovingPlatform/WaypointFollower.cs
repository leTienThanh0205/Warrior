using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoint;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed;

    void Update()
    {
        if (Vector2.Distance(waypoint[currentWaypointIndex].transform.position,transform.position) < .1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoint.Length)
            {
                currentWaypointIndex = 0;

            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoint[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
    
}
 