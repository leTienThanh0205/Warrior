using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    //starting position for the parallax game object
    Vector2 startingPosition;

    //start z value of parallax game object
    float startingz;

    //distance that the camera has moves from the starting position of the parallax object
    Vector2 canMoveSinceStart => (Vector2)cam.transform.position - startingPosition;
    
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    //if object is in front of target, use near clip plane. if behind target, use farclipplane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    // The futher the object from the player, the faster the parallaxEffect object will move. Drag it/s z value closer to the target to make it move sloner
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    //start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingz = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        ///when the target moves, move parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + canMoveSinceStart * parallaxFactor;
        //the x/y position changes based on target travel speed times the parallax factor, but z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingz);
    }
}
