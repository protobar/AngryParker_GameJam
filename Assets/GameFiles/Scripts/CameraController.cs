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
        CameraInitialPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShiftCamera();
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

    public void CameraInitialPosition()
    {
        mainCamera.transform.position = orthographicPosition.position;
        mainCamera.transform.rotation = orthographicPosition.rotation;
        mainCamera.orthographic = true;  // Ensure the camera is in orthographic mode
        isPerspective = false;           // Ensure the camera mode flag is reset
    }

    public void ShiftCamera()
    {
        if (MoveCube.launchAllowed)
        {
            isPerspective = !isPerspective;
            mainCamera.orthographic = !isPerspective;
        }
    }
}
