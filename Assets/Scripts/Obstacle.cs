using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] UnityEvent additionalEvent;

    const string PLAYER = "Player";

    private void Start()
    {
        additionalEvent?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER))
        {
            collision.gameObject.GetComponent<Health>().DamageObject(1);
            Destroy(gameObject);
        }
    }
}
