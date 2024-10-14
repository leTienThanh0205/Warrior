using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0, 5)]
    public float speed;
    [Range(0, 2)]
    public float waitDuration;

    public GameObject ways;
    Vector3 targesPos;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    int speedMultiplier = 1;

    private void Awake()
    {
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;

        }
    }
    void Start()
    {
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targesPos = wayPoints[pointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speedMultiplier * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targesPos, step);
        if (transform.position == targesPos)
        {
            NextPoint();
        }
    }
    void NextPoint()
    {
        if (pointIndex == pointCount - 1)
        {
            direction = -1;
            //Debug.Log(direction);
        }
        if (pointIndex == 0)
        {
            direction = 1;
            //  Debug.Log(direction);

        }
        pointIndex += direction;
        //Debug.Log(pointIndex);
        targesPos = wayPoints[pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());
    }
    IEnumerator WaitNextPoint()
    {
        speedMultiplier = 0;
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;
    }
}
