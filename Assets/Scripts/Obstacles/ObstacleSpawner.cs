
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;

    private float timer;

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        int index = Random.Range(0, obstacles.Length);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, randomY, 0f);

        Instantiate(obstacles[index], spawnPosition, obstacles[index].transform.rotation);

    }
}
