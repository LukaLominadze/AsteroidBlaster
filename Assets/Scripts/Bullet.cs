using UnityEngine;

public class Bullet : MonoBehaviour
{
    const string OBSTACLE = "Obstacle";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(OBSTACLE))
        {
            collision.gameObject.GetComponent<Health>().DamageObject(1);
            Destroy(gameObject);
        }
    }
}
