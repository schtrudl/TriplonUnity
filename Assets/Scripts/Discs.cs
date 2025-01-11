using UnityEngine;

public class Discs : MonoBehaviour
{
    [SerializeField]
    private GameObject discPrefab; // Assign your coin prefab in the Inspector

    public int totalCoins = 9; // Number of coins to maintain
    private Vector3 spawnAreaMin = new Vector3(-300, 1, -100); // Minimum spawn position (for x and y)
    private Vector3 spawnAreaMax = new Vector3(300, 1, 100); // Maximum spawn position (for x and y)
    private float topFloorY = 2.5f; // Fixed Y value for the top floor
    private float bottomFloorY = -17.5f; // Fixed Y value for the bottom floor

    private Vector3 discScale = new Vector3(1.3f, 1.3f, 1.3f); // Scale for all coins

    void Start()
    {
        // Spawn the initial set of coins as children of CoinManager
        for (int i = 0; i < totalCoins; i++)
        {
            SpawnCoin();
        }
    }

    void Update()
    {
        // Check and maintain the total number of coins
        if (transform.childCount < totalCoins)
        {
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        // Generate a random position within the specified range
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomZ = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
        float randomY = Random.Range(0, 2) == 0 ? bottomFloorY : topFloorY;

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        // Instantiate the coin and set it as a child of the CoinManager
        GameObject disc = Instantiate(discPrefab, randomPosition, Quaternion.identity);
        disc.transform.parent = transform; // Set CoinManager as the parent

        // Set the coin's scale
        disc.transform.localScale = discScale;

        // Add a Sphere Collider to the disc
        SphereCollider sphereCollider = disc.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true; // Optional: Set to true if you want trigger behavior instead of collision
        disc.AddComponent<DiscCollection>();
    }
}
