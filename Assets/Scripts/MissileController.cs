using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

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
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Missile strikes and does " + damage + " damage!");

            EnemyBase enemy = other.GetComponent<EnemyBase>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
