using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Vertical") * speed * Time.deltaTime, 0, -Input.GetAxis("Horizontal") * speed * Time.deltaTime);

    }
}
