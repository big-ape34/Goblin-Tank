using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 10f;
    public float damage;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Missile strikes and does " + damage + " damage!");
        Destroy(gameObject);
    }
}