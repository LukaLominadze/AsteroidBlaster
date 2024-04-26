using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [Space(5)]
    [SerializeField] PlayerCollision collision;

    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJump_fallMultiplier;
    
    private float direction;
    private float gravity;
    private bool jumpInput;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<PlayerCollision>();

        gravity = Physics2D.gravity.y;
    }

    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        //walk
        rb.velocity = new Vector2(direction * horizontalSpeed, rb.velocity.y);

        //jump
        if (collision.OnGround() && jumpInput && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }

        //variable fall
        if(rb.velocity.y > 0.01f && !jumpInput)
        {
            rb.velocity += lowJump_fallMultiplier * Time.fixedDeltaTime * gravity * Vector2.up;
        }
        else if(!collision.OnGround() && rb.velocity.y < -0.01f)
        {
            rb.velocity += fallMultiplier * Time.fixedDeltaTime * gravity * Vector2.up;
        }

        //enable variable for one frame jump
        if (collision.OnGround() && !canJump)
        {
            canJump = true;
        }
    }
}
