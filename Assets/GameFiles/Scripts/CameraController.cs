using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform orthographicPosition;
    public Transform perspectivePosition;
    public float transitionSpeed = 2f;
    private bool isPerspective = false;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Set the camera to orthographic mode initially
        mainCamera.orthographic = true;
        mainCamera.transform.position = orthographicPosition.position;
        mainCamera.transform.rotation = orthographicPosition.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPerspective = !isPerspective;
            mainCamera.orthographic = !isPerspective;
        }

        if (isPerspective)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, perspectivePosition.position, Time.deltaTime * transitionSpeed);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, perspectivePosition.rotation, Time.deltaTime * transitionSpeed);
        }
        else
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, orthographicPosition.position, Time.deltaTime * transitionSpeed);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, orthographicPosition.rotation, Time.deltaTime * transitionSpeed);
        }
    }

}
