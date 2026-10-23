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
    public bool controlsReversed;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

                // Zera apenas a velocidade vertical.
                Vector2 velocity = rb.linearVelocity;
                velocity.y = 0f;
                rb.linearVelocity = velocity;
            }
        }

        isFlying = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);

        UpdateAnimation();

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null &&
         GameManager.Instance.IsGameOver)
        {
            return;
        }

        Vector2 direction;

        if (controlsReversed)
        {
            // Mecânica alternativa:
            // sobe sem pressionar e desce ao pressionar.
            direction = isFlying ? Vector2.down : Vector2.up;
        }
        else
        {
            // Movimentação normal:
            // sobe ao pressionar.
            direction = isFlying ? Vector2.up : Vector2.zero;
        }


        if (direction != Vector2.zero)
        {
            rb.AddForce(direction * liftForce);
        }

        Vector2 velocity = rb.linearVelocity;

        velocity.y = Mathf.Clamp(
            velocity.y,
            maxFallSpeed,
            maxRiseSpeed
        );

        rb.linearVelocity = velocity;
    }



    public void ActivateInvertedMovement()
    {
        controlsReversed = true;
        reverseControlsTimer = reverseControlsDuration;
    }

    public void DeactivateInvertedMovement()
    {
        controlsReversed = false;

        Vector2 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;
    }

    public bool IsControlsReversed()
    {
        return controlsReversed;
    }
    private void UpdateAnimation()
    {
        animator.SetBool("ControlsReversed", controlsReversed);
    }

}