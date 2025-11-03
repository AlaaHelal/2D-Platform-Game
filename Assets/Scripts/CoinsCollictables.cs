using UnityEngine;

public class CoinsCollictables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().coinsCollected += 1;
            Destroy(gameObject);
        }
    }
}
