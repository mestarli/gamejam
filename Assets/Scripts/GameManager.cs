using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables
    public static GameManager Instance;

    public Slider playerHealthBarSlider;

    private bool pausedGame;
    private GameObject backbtn;
    private GameObject pausebtn;

    public Text rosesCountText;
    public int collectedRosesScore;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        rosesCountText.text = "" + collectedRosesScore;
    }

    // Update is called once per frame
    void Update()
    {
        PauseEsc();
    }

    #region Collectibles

    public void UpdatePickablesScore()
    {
        rosesCountText.text = collectedRosesScore + "";
    }

    public void IncreaseScore()
    {
        collectedRosesScore++;
        UpdatePickablesScore();
    }

    #endregion
    
    #region Play Game

    public void PlayGame()
    {
        SceneManager.LoadScene("TestArea");
        collectedRosesScore = 0;
    }

    #endregion

    #region Options Game

    // Método para pausar el juego con la tecla "Esc"
    private void PauseEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si la variable pausedGame está activa se activará el método Resume()
            if (pausedGame)
            {
                Resume();
            }
            
            // Si la variable no está activa, se activará el método Pause()
            else
            {
                Pause();
            }
        }
    }
    
    // Método para pausar el juego con un botón
    public void Pause()
    {
        // Se pone la variable booleana a true
        pausedGame = true;
        
        // Se detiene el tiempo del juego
        Time.timeScale = 0f;
        
        // Se desactiva el boton de pausa del panel de juego
        pausebtn.SetActive(false);

        // Desactivamos el botón de volver al juego
        backbtn.SetActive(true);
    }

    // Método para despausar el juego
    public void Resume()
    {
        // Se pone la variable booleana a false
        pausedGame = false;
        
        // Se vuelve a poner el tiempo normal del juego
        Time.timeScale = 1f;
        
        // Se activa el botón de pausa en el panel de juego
        pausebtn.SetActive(true);

        // Desactivamos el botón de volver al juego
        backbtn.SetActive(false);
    }

    // Método para ir a la escena del main menu del juego
    public void ToMenu()
    {
        // Se pone la variable booleana a false
        pausedGame = false;
        
        // Se detiene el tiempo del juego
        Time.timeScale = 0f;

        // Cargamos la escena
        SceneManager.LoadScene("MainMenu");

        collectedRosesScore = 0;
    }
    
    // Método para salir del juego
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region WinGame

    public void WinGame()
    {
        SceneManager.LoadScene("WinGame");
        collectedRosesScore = 0;
    }

    #endregion
    
    #region EndGame

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
        collectedRosesScore = 0;
    }

    #endregion
}
