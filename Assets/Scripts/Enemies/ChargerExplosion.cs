using UnityEngine;

public class ChargerExplosion : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
