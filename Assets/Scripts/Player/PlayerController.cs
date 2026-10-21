using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float liftForce = 20f;
    public float maxRiseSpeed = 5f;
    public float maxFallSpeed = -5f;

    private Rigidbody2D rb;
    [SerializeField] private bool isFlying;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) 
        {
            isFlying = false;
            return;
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
            rb.AddForce(Vector2.up * liftForce);
        }

        Vector2 velocity = rb.linearVelocity;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, maxFallSpeed, maxRiseSpeed));

        rb.linearVelocity = velocity;
    }
}