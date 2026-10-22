using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject scorePointPrefab;
    [SerializeField] private GameObject collectiblePrefab;

    [SerializeField] private Transform spawnPoint;

    [Header("Score Points Spawn Settings")]
    [SerializeField] private float scorePointsSpawnInterval = 2f;
    [SerializeField] private int scorePointsPerPattern = 5;
    [SerializeField] private float scorePointsSpacing = 0.8f;

    private enum scorePointsPattern
    {
        Horizontal,
        Arc,
        ZigZag,
        Diagonal
    }


    [Header("Collectible Spawn Settings")]
    [SerializeField] private float CollectibleMinSpawnInterval = 8f;
    [SerializeField] private float CollectibleMaxSpawnInterval = 12f;


    [Header("Spawn Area")]
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;

    private float scorePointsTimer;
    private float collectibleTimer;
    private float nextCollectibleSpawn;



    private void Start()
    {
        ScheduleNextCollectible();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        return;

        UpdateScorePointsSpawner();
        UpdateCollectibleSpawner();
    }

    private void UpdateScorePointsSpawner()
    {
        scorePointsTimer += Time.deltaTime;

        if (scorePointsTimer >= scorePointsSpawnInterval)
        {
            SpawnScorePointsPattern();
            scorePointsTimer = 0f;
        }
    }

    private void UpdateCollectibleSpawner()
    {
        collectibleTimer += Time.deltaTime;

        if (collectibleTimer >= nextCollectibleSpawn)
        {
            SpawnCollectible();
            collectibleTimer = 0f;
            ScheduleNextCollectible();
        }
    }

    private void SpawnScorePointsPattern()
    {
        scorePointsPattern pattern = (scorePointsPattern)Random.Range(0, 4);

        float baseY = Random.Range(minY, maxY);

        for (int i = 0; i < scorePointsPerPattern; i++)
        {
            float x = spawnPoint.position.x + i * scorePointsSpacing;
            float y = baseY;

            switch (pattern)
            {
                case scorePointsPattern.Horizontal:
                    y = baseY;
                    break;

                case scorePointsPattern.Arc:
                    float middle = (scorePointsPerPattern - 1) / 2f;
                    float distance = i - middle;

                    y = baseY + 1.5f -
                        distance * distance * 0.25f;
                    break;

                case scorePointsPattern.ZigZag:
                    y = baseY + (i % 2 == 0 ? 0.8f : -0.8f);
                    break;

                case scorePointsPattern.Diagonal:
                    y = baseY + i * 0.35f;
                    break;
            }

            Vector3 position = new Vector3(x, Mathf.Clamp(y, minY, maxY), spawnPoint.position.z);

            Instantiate(scorePointPrefab, position, scorePointPrefab.transform.rotation);
        }
    }

    private void SpawnCollectible()
    {
        if (collectiblePrefab == null || spawnPoint == null)
            return;

        Vector3 position = new Vector3(spawnPoint.position.x, Random.Range(minY, maxY), spawnPoint.position.z);

        Instantiate(collectiblePrefab, position, collectiblePrefab.transform.rotation);
    }

    private void ScheduleNextCollectible()
    {
        nextCollectibleSpawn = Random.Range(CollectibleMinSpawnInterval, CollectibleMaxSpawnInterval);
    }
}
