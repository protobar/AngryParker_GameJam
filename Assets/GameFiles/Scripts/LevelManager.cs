using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 0;
    public GameObject currentObstacle, currentCarPos, Car;
    public List<GameObject> Obstacles,carPositions;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentLevel = staticVariables.currentLevel;
        if (currentLevel != 0)
        {
            print("Lev = " + currentLevel);
            
            LoadLevelData(currentLevel);
        }
        else
        {
            print("Exp = currentLevel is = 0");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevelData(int level)
    {
        currentObstacle = Obstacles[level];
        currentCarPos = carPositions[level];
        //print("Sent  = " + currentCarPos.name);
        Car.SetActive(false);
        currentObstacle.SetActive(true);
        Car.transform.parent = currentCarPos.transform;
        Car.transform.localPosition = Vector3.zero;
        Car.SetActive(true);
    }
}
