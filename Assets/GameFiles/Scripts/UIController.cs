using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public MoveCube moveCube;
    public DrawLine drawLineScript;
    public CameraController cameraController;

    public List<GameObject> NPC_List;

    public void ReloadLevel()
    {
        Debug.Log("EventsListener: Reload Level");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void DriveCar() 
    {
        // Enable NPC Movement
        foreach(GameObject npc in NPC_List)
        {
            npc.GetComponent<Animator>().enabled = true;
            npc.GetComponent<NPC_Movement>().enabled = true;
        }

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
