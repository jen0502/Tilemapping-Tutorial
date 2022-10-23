using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover5 : MonoBehaviour
{
   // Transforms to act as start and end markers for the journey.
public Transform startMarker5;
public Transform endMarker5;

// Movement speed in units/sec.
public float speed = 1.0F;

// Time when the movement started.
private float startTime;

// Total distance between the markers.
private float journeyLength;

void Start()
     {
     // Keep a note of the time the movement started.
          startTime = Time.time;

     // Calculate the journey length.
          journeyLength = Vector2.Distance(startMarker5.position, endMarker5.position);
     }

// Follows the target position like with a spring
void Update()
     {
     // Distance moved = time * speed.
          float distCovered = (Time.time - startTime) * speed;

     // Fraction of journey completed = current distance divided by total distance.
          float fracJourney = distCovered / journeyLength;

     // Set our position as a fraction of the distance between the markers and pingpong the movement.
          transform.position = Vector2.Lerp(startMarker5.position, endMarker5.position, Mathf.PingPong (fracJourney, 1));
     }
}