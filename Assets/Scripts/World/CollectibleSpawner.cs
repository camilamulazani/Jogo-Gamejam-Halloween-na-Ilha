using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject scorePointPrefab;
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private GameObject reverseControlsPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject restoreHealthPrefab;

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

    [Header("Special Collectible Spawn Settings")]
    [SerializeField] private float specialMinSpawnInterval = 18f;
    [SerializeField] private float specialMaxSpawnInterval = 25f;


    [Header("Spawn Area")]
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;

    private float scorePointsTimer;
    private float collectibleTimer;
    private float nextCollectibleSpawn;
    private float specialCollectibleTimer;
    private float nextSpecialCollectibleSpawn;



    private void Start()
    {
        ScheduleNextCollectible();
        ScheduleNextSpecialCollectible();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        return;

        UpdateScorePointsSpawner();
        UpdateCollectibleSpawner();
        UpdateSpecialCollectibleSpawner();
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

    private void UpdateSpecialCollectibleSpawner()
    {
        specialCollectibleTimer += Time.deltaTime;

        if (specialCollectibleTimer >= nextSpecialCollectibleSpawn)
        {
            SpawnSpecialCollectible();
            specialCollectibleTimer = 0f;
            ScheduleNextSpecialCollectible();
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
        SpawnCollectible(collectiblePrefab, CollectibleType.Collectible);
    }

    private void SpawnSpecialCollectible()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                SpawnCollectible(reverseControlsPrefab, CollectibleType.ReverseControls);
                break;
            case 1:
                SpawnCollectible(shieldPrefab, CollectibleType.Shield);
                break;
            default:
                SpawnCollectible(restoreHealthPrefab, CollectibleType.RestoreHealth);
                break;
        }
    }

    private void SpawnCollectible(GameObject prefab, CollectibleType type)
    {
        if (prefab == null || spawnPoint == null)
        {
            Debug.LogWarning($"Cannot spawn {type}: assign its prefab and the spawn point in the Inspector.", this);
            return;
        }

        Vector3 position = new Vector3(spawnPoint.position.x, Random.Range(minY, maxY), spawnPoint.position.z);

        GameObject spawnedCollectible = Instantiate(prefab, position, prefab.transform.rotation);
        Collectible collectible = spawnedCollectible.GetComponent<Collectible>();
        if (collectible == null)
        {
            Debug.LogError("The collectible prefab must have a Collectible component.", spawnedCollectible);
            Destroy(spawnedCollectible);
            return;
        }

        collectible.collectibleType = type;
    }

    private void ScheduleNextCollectible()
    {
        nextCollectibleSpawn = Random.Range(CollectibleMinSpawnInterval, CollectibleMaxSpawnInterval);
    }

    private void ScheduleNextSpecialCollectible()
    {
        nextSpecialCollectibleSpawn = Random.Range(specialMinSpawnInterval, specialMaxSpawnInterval);
    }
}
