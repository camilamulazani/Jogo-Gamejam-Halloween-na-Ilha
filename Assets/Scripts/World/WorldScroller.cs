using UnityEngine;

public class WorldScroller : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float speedChangeInterval = 8f;
    [SerializeField] private float minSpeed = 6f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float speedChangeRate = 0.5f;

    private static float currentSpeed;
    private static float targetSpeed;
    private static float nextSpeedChangeTime;
    private static bool speedInitialized;

    private float destroyLimit = -15f;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetSharedSpeed()
    {
        speedInitialized = false;
        currentSpeed = 0f;
        targetSpeed = 0f;
        nextSpeedChangeTime = 0f;
    }

    protected virtual void Awake()
    {
        if (speedInitialized)
        {
            return;
        }

        currentSpeed = speed;
        targetSpeed = speed;
        nextSpeedChangeTime = Time.time + speedChangeInterval;
        speedInitialized = true;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        if (Time.time >= nextSpeedChangeTime)
        {
            targetSpeed = Random.Range(minSpeed, maxSpeed);
            nextSpeedChangeTime = Time.time + speedChangeInterval;
        }

        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, speedChangeRate * Time.deltaTime);

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (transform.position.x < destroyLimit)
        {
            Destroy(gameObject);
        }
    }
}
