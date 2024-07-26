using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    public List<Transform> PointsToMove;
    public Transform currentPoint;
    public int currentIndex = 0;
    public float speed = 0f;
    public float stoppingDistance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (PointsToMove.Count > 0)
        {
            currentIndex = 0;
        }
        else
        {
            Debug.LogWarning("PointsToMove list is empty!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PointsToMove.Count == 0) return; // If no points, do nothing

        // Get the current target point
        Transform currentPoint = PointsToMove[currentIndex];

        // Move the character towards the target position
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);
        transform.LookAt(currentPoint);
        // Check if the character has reached the current point
        if (Vector3.Distance(transform.position, currentPoint.position) <= stoppingDistance)
        {
            // Move to the next point
            currentIndex++;
            if (currentIndex >= PointsToMove.Count)
            {
                currentIndex = 0;
            }
        }
    }
}
