using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void NextLevel(string sceneName) { 
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(sceneName);
    }

    public void FirstLevel()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("GamePlay");
    }
    }
