using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MoveCube : MonoBehaviour
{
    public DrawLine drawLine;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    private List<Vector3> points;
    private int targetIndex;
    private Rigidbody rb;
    public static bool launchAllowed = false; // Ensure default is false
    public static bool delayCompleted = false;

    void Start()
    {
        points = new List<Vector3>();
        targetIndex = 0;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        launchAllowed = false;
        delayCompleted = false;
    }

    void Update()
    {
        Debug.Log("launchAllowed: " + launchAllowed);

        // Check if the car should be driven based on the flag and input
        if (Input.GetKeyDown(KeyCode.Space) && launchAllowed && delayCompleted)
        {
            DriveCar();
        }

        // Movement logic only if launchAllowed and points are set
        if (points.Count > 0 && targetIndex < points.Count && delayCompleted)
        {
            Vector3 targetPosition = points[targetIndex];
            Vector3 direction = targetPosition - transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
            }

            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime));

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetIndex++;
            }
        }
    }

    public void DriveCar()
    {
        points = drawLine.GetPoints();
        if (points.Count > 0 && launchAllowed)
        {
            StartCoroutine(DelayCar());
        }
        else
        {
            Debug.Log("No line drawn or launch not allowed. Cannot drive the car.");
        }
    }

    IEnumerator DelayCar()
    {
        yield return new WaitForSeconds(3f);
        delayCompleted = true;

        if (points.Count > 0)
        {
            transform.position = points[0];
            targetIndex = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parking"))
        {
            Debug.Log("Car Parked!");
            ScoreManager.Instance.UpdateScore(1);
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Car collided with obstacle: " + col.gameObject.name);
        }
    }
}
