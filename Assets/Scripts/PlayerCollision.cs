using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    [SerializeField] private float raycastLenght;

    private bool onGround;

    public bool OnGround()
    {
        onGround = Physics2D.Linecast(transform.position, (Vector2)transform.position - Vector2.up * raycastLenght, groundLayer);

        return onGround;
    }
}
