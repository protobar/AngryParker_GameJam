using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitForce : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject animated, physice;
    public Vector3 hitDirection;

    private void Start()
    {
        physice.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with a specific type of object, like a projectile
        if (other.CompareTag("Car"))
        {
            print("Hit");
            
            physice.SetActive(true);
            physice.transform.position = animated.transform.position;
            physice.transform.rotation = animated.transform.rotation;
            animated.SetActive(false);
            //transform.gameObject.GetComponent<Animator>().enabled = false;
            /* 
             // Calculate the direction of the hit
             Vector3 hitDirection = transform.position - other.transform.position;
             hitDirection.Normalize(); // Normalize to get the direction only
            */
            // Apply force in the hit direction
            ApplyForce(hitDirection);
        }
    }

    void ApplyForce(Vector3 direction)
    {
        // You can adjust the force value to control the push intensity
        print("Appied forece on  = " + rb.name);
        float force = 25f;
        rb.AddForce(direction * force, ForceMode.Impulse);
        //Invoke(nameof(AnimatorOff), 1f);
    }
    
    private void AnimatorOff()
    {
        transform.gameObject.GetComponent<Animator>().enabled = false;
    }
}
