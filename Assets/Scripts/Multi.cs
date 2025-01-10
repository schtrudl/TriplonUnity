using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Multi : MonoBehaviour
{
    public GameObject prefab;
    private GameObject player2;

    public void OnMultiPlayerButtonClick()
    {
        AddPlayer2();
    }

    public void OnSinglePlayerButtonClick()
    {
        ResetPlayer2();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //AddPlayer2();
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
        if (player2 == null)
        {
            player2 = Instantiate(prefab);
            player2.name = prefab.name;
        }
    }
    private void ResetPlayer2()
    {
        if (player2 != null)
        {
            Destroy(player2);
            player2 = null;
        }
    }
}
