using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public float liftForce = 20f;
    public float maxRiseSpeed = 5f;
    public float maxFallSpeed = -5f;
    [SerializeField] private float reverseControlsDuration = 8f;

    private Rigidbody2D rb;
    [SerializeField] private bool isFlying;
    private float reverseControlsTimer;
    private bool controlsReversed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Instance = this;
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) 
        {
            isFlying = false;
            return;
        }

        if (controlsReversed)
        {
            reverseControlsTimer -= Time.deltaTime;
            if (reverseControlsTimer <= 0f)
            {
                controlsReversed = false;
            }
        }

        isFlying = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
 
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) 
        {
            return;
        }

        if (isFlying)
        {
            rb.AddForce((controlsReversed ? Vector2.down : Vector2.up) * liftForce);
        }

        Vector2 velocity = rb.linearVelocity;
        velocity.y = Mathf.Clamp(velocity.y, maxFallSpeed, maxRiseSpeed);
        rb.linearVelocity = velocity;
    }

    public void ReverseControls()
    {
        controlsReversed = true;
        reverseControlsTimer = reverseControlsDuration;
    }
}