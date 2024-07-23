using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public MoveCube moveCube;
    public DrawLine drawLineScript;
    public CameraController cameraController;

    public void ReloadLevel()
    {
        Debug.Log("EventsListener: Reload Level");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void DriveCar() 
    {
        cameraController.ShiftCamera();
        moveCube.DriveCar();
    }
    public void OnResetLineButtonClicked()
    {
        MoveCube.launchAllowed = false;
        MoveCube.delayCompleted = false;
        drawLineScript.ClearLines();
        cameraController.CameraInitialPosition();
    }

}
