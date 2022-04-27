using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
 
    [SerializeField] private AudioSource clickButton;
    
    public void StartGame()
    {
        clickButton.Play();
        SceneManager.LoadScene("Game");
    }

    public void ShowInstructions(GameObject panel)
    {
        clickButton.Play();
        panel.active = !panel.active;
    }
    
    public void QuitGame()
    {
        clickButton.Play();
        Application.Quit();
    }
    public void soundClickButton()
    {
        clickButton.Play();
    }
}
