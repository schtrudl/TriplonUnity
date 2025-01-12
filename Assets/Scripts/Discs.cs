using UnityEngine;

public class Discs : MonoBehaviour
{
    [SerializeField]
    private GameObject discPrefab;

    public int totalDiscs = 10; 
    public Vector3 spawnAreaMin = new Vector3(-300, 1, -100); 
    public Vector3 spawnAreaMax = new Vector3(300, 1, 100); 
    public float topFloorY = 2.5f; 
    public float bottomFloorY = -17.5f;
    public Vector3 discScale = new Vector3(1.3f, 1.3f, 1.3f); 

    void Start()
    {
        // Spawn the initial set of discs as children of Discs
        for (int i = 0; i < totalDiscs; i++)
        {
            SpawnDisc();
        }
    }

    void Update()
    {
        // Check and maintain the total number of discs
        if (transform.childCount < totalDiscs)
        {
            SpawnDisc();
        }
    }

    void SpawnDisc()
    {
        // Generate a random position within the specified range
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomZ = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
        float randomY = Random.Range(0, 2) == 0 ? bottomFloorY : topFloorY;

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        // Instantiate the disc and set it as a child of the Discs
        GameObject disc = Instantiate(discPrefab, randomPosition, Quaternion.identity);
        disc.transform.parent = transform; // Set Discs as the parent

        // Set the disc's scale
        disc.transform.localScale = discScale;

        // Add a Sphere Collider to the disc
        SphereCollider sphereCollider = disc.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true; 
        disc.AddComponent<DiscCollection>();
    }
}
