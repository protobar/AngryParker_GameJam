using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    public List<Button> levelButtons; 
    public List<Sprite> unlockedLevelImages; 
    private int maxLevelUnlocked;

    void Start()
    {
        
        maxLevelUnlocked = PlayerPrefs.GetInt("MaxLevelUnlocked", 1);

        
        UpdateLevelButtons();
    }

    void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (i < maxLevelUnlocked)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponent<Image>().sprite = unlockedLevelImages[i];
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void SelectLevel(int levelIndex)
    {
        
        PlayerPrefs.SetInt("SelectedLevel", levelIndex);
        SceneManager.LoadScene("Gameplay");
    }

    public void UnlockNextLevel()
    {
        if (maxLevelUnlocked < levelButtons.Count)
        {
            maxLevelUnlocked++;
            PlayerPrefs.SetInt("MaxLevelUnlocked", maxLevelUnlocked);
            UpdateLevelButtons();
        }
    }
}
