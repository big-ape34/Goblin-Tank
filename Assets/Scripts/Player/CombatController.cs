using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public Transform firePoint_machineGun;

    public GameObject missilePrefab;
    public GameObject muzzleFlashSmokePrefab;
    public GameObject cursorRadius;

    [Header("Turret Settings")]
    public float fireRate = 0.25f;
    public float damage = 5f;
    [SerializeField] private float explosionRadius = 2f;

    [Header("Machine Gun Settings")]
    [SerializeField] private float machineGunDamage = 5f;
    [SerializeField] private float machineGunRange = 15f;
    [SerializeField] private LayerMask shootableLayers;

    [SerializeField] private float machineGunfireRate = 0.1f;
    private float nextFireTime;

    private float reloadTime;
    [SerializeField] private float setReloadTime;

    private Vector3 fireDestination; //used for where missile lands



    void Update()
    {
        if (Mouse.current == null) return;

        //Main turret fire
        if (
            Mouse.current.leftButton.wasPressedThisFrame && 
            reloadTime <= 0
            )
        {
            Fire();
        }

        //machien gun fire
        if (Mouse.current.rightButton.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            FireMachineGun();
        }

        //Trigger reload
        if (reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
        }

        CursorRadiusHandler();
    }

    void CursorRadiusHandler()
    {
        if (Mouse.current == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        cursorRadius.transform.position = mouseWorldPos;
        cursorRadius.transform.localScale = Vector3.one * explosionRadius * 2f;

        fireDestination = mouseWorldPos;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        if (Mouse.current == null)
            return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(mouseWorldPos, explosionRadius);
    }

    void Fire()
    {
        reloadTime = setReloadTime; //trigger reload time

        // Spawn missile
        GameObject missileInstance = Instantiate(
            missilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        // Set the stats on the missile
        Missile missileScript = missileInstance.GetComponent<Missile>();
        if (missileScript != null)
        {
            missileScript.damage = damage;
            missileScript.explosionRadius = explosionRadius;
            missileScript.fireDestination = fireDestination;
        }

        // Spawn the muzzle flash
        if (muzzleFlashSmokePrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashSmokePrefab, firePoint.position, firePoint.rotation);
            Destroy(flash, 1.5f); // destroy after 0.5 seconds (so it doesn't linger)
        }
    }

    void FireMachineGun()
    {
        Vector2 origin = firePoint_machineGun.position;
        Vector2 direction = firePoint_machineGun.right;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction,
            machineGunRange,
            shootableLayers
        );

        Debug.DrawRay(origin, direction * machineGunRange, Color.red, 0.1f);
        Debug.Log("firing machien gun");
        if (hit.collider != null)
        {
            EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(machineGunDamage);
            }
        }
    }
}
