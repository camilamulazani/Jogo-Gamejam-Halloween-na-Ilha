using UnityEngine;

public class WorldScroller : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    private float destroyLimit = -15f;

    private void Update()
    {
        transform.position +=
            Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < destroyLimit)
        {
            Destroy(gameObject);
        }
    }

}