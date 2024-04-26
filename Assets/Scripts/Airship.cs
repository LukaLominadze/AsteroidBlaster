using UnityEngine;
using UnityEngine.SceneManagement;

public class Airship : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [Space(5)]
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] private float movementSpeedX;
    [SerializeField] private float movementSpeedY;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float boostMultiplier;

    private float directionX;
    private float directionY;
    private float original_cooldownTime;

    private bool attackInput;
    private bool boostInput;

    enum AttackState { ready, cooldown }

    AttackState attackState = AttackState.ready;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        original_cooldownTime = cooldownTime;
    }

    void Update()
    {
        directionX = Input.GetAxis("Horizontal");
        directionY = Input.GetAxis("Vertical");
        attackInput = Input.GetKey(KeyCode.Space);
        boostInput = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        //move
        if (!boostInput)
        {
            rb.velocity = new Vector2(directionX * movementSpeedX, directionY * movementSpeedY);
            GameLogic.Boost(false);
        }
        else
        {
            rb.velocity = new Vector2(directionX * movementSpeedX * boostMultiplier, directionY * movementSpeedY * boostMultiplier);
            GameLogic.Boost(true);
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f),
                                         Mathf.Clamp(transform.position.y, -4.3f, 4.3f));

        //attack
        switch (attackState)
        {
            case AttackState.ready:
                if (attackInput)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
                    AudioManager.audioDict["Bullet"].Play();
                    attackState = AttackState.cooldown;
                }
                break;
            case AttackState.cooldown:
                cooldownTime -= Time.fixedDeltaTime;
                if(cooldownTime <= 0)
                {
                    cooldownTime = original_cooldownTime;
                    attackState = AttackState.ready;
                }
                break;
        }
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}
