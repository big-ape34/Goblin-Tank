using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public GameObject missilePrefab;

    [Header("Settings")]
    public float fireRate = 0.25f;

    private float nextFireTime;

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire()
    {
        Instantiate(
            missilePrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}
