using UnityEngine;

public class Multi : MonoBehaviour
{
    public GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddPlayer2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds second player
    /// </summary>
    public void AddPlayer2()
    {
        GameObject player2 = Instantiate(prefab);
        player2.name = prefab.name;
    }
}
