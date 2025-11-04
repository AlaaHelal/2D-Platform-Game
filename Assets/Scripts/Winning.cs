using UnityEngine;
using UnityEngine.UI;

public class Winning : MonoBehaviour
{
    public Image winWindow;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            winWindow.gameObject.SetActive(true);
            UnityEngine.Time.timeScale = 0f; // Pause the game
        }
    }
}
