using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public float launchForceMultiplier = 10f;
    public float torqueMultiplier = 5f;
    public SpringJoint[] springs;
    public float springForce = 10f;
    public float springDamper = 0.5f;
    public float dragStartZ = -1.0f;
    public float dragEndZ = 1.0f;

    private Vector3 dragStartPos;
    private Vector3 dragEndPos;
    private bool isDragging = false;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        foreach (SpringJoint spring in springs)
        {
            spring.spring = springForce;
            spring.damper = springDamper;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragStartPos.z = dragStartZ;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragEndPos.z = dragEndZ;
            LaunchCar();
            isDragging = false;
        }
    }

    void LaunchCar()
    {
        Vector3 dragVector = dragStartPos - dragEndPos;
        carRigidbody.AddForce(dragVector * launchForceMultiplier, ForceMode.Impulse);
        carRigidbody.AddTorque(Random.insideUnitSphere * torqueMultiplier, ForceMode.Impulse);
    }
}
