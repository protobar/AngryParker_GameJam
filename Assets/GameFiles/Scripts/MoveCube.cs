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

    void Start()
    {
        points = new List<Vector3>();
        targetIndex = 0;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DelayCar());
        }

        if (points.Count > 0 && targetIndex < points.Count)
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

    IEnumerator DelayCar()
    {
        yield return new WaitForSeconds(3f);

        points = drawLine.GetPoints();
        if (points.Count > 0)
        {
            transform.position = points[0];
            targetIndex = 1;
        }
    }

    public void LaunchCar()
    {
        points = drawLine.GetPoints();
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
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Car Collided");
        }
    }
}
