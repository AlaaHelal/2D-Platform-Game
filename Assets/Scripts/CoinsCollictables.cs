using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CoinsCollictables : MonoBehaviour
{
    public AudioClip coinClip;
    private TextMeshProUGUI coinText;


    public static int coinsCollected = 0;

    private void Start()
    {
        coinText = GameObject.FindGameObjectWithTag("coinText").gameObject.GetComponent<TextMeshProUGUI>();
        coinText.text = "Coins: " + coinsCollected;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().PlaySound(coinClip);
            Destroy(gameObject);
            coinsCollected ++;
            coinText.text = "Coins: " + coinsCollected;
        }
    }
}
