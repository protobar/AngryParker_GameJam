using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class StageManager : MonoBehaviour
{
    public List<GameObject> levels;

    void Start()
    {
        int selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 0);
        ActivateLevel(selectedLevel);
    }

    void ActivateLevel(int levelIndex)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(i == levelIndex);
        }
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
