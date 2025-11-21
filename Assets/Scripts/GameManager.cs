using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image pauseMenu;
    public Image levelsMenu;
    public Image mainMenu;
    CoinsCollictables coinsCollictables;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void NextLevel(string sceneName) { 
        CoinsCollictables.coinsCollected = 0;
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(sceneName);
    }
    
    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        levelsMenu.gameObject.SetActive(true);
    }

   public void QuitGame()
   {
       Application.Quit();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void MainMenu()
    {   
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }


}
