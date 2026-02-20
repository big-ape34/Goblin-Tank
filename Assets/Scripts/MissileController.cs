using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 10f;
    public float damage;

    private float expiryTime = 10f;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        if(expiryTime > 0)
        {
            expiryTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Missile strikes and does " + damage + " damage!");
        Destroy(gameObject);
    }
}