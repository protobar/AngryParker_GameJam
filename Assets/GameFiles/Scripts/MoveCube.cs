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
    public float angleLimit = 45f;
    public float jiggleForceLimit = 2000f;
    public float jiggleTorqueLimit = 2000f;
    public float jiggleDuration = 0.3f; // Duration of jiggle effect
    public float jiggleFrequency = 20f; // Frequency of jiggle oscillation
    public float jiggleReductionFactor = 0.05f; // Reduction factor for jiggle effect

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
        if (Input.GetKeyDown(KeyCode.Space) && launchAllowed && delayCompleted)
        {
            DriveCar();
        }

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

            // Add jiggle effect for sharp turns
            float angle = Vector3.Angle(transform.forward, direction);
            if (angle > angleLimit)
            {
                Debug.Log("Angle limit reached");
                StartCoroutine(JiggleEffect());
            }

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

    IEnumerator JiggleEffect()
    {
            // Apply a jiggle force sideways to simulate a more dramatic bounce
            Vector3 jiggleForce = transform.up * Mathf.Sin(jiggleFrequency) * jiggleForceLimit;
            rb.AddForce(jiggleForce, ForceMode.Impulse); // Use Impulse for a more noticeable effect

            // Apply a torque around the forward axis to simulate a more dramatic tilt
            Vector3 jiggleTorque = transform.forward * Mathf.Sin(jiggleFrequency) * jiggleTorqueLimit;
            rb.AddTorque(jiggleTorque, ForceMode.Impulse); // Use Impulse for a more noticeable effect

            yield return null;
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
