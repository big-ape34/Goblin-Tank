using System.Collections;
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
    [SerializeField] private GameObject machineGunBulletPrefab;
    [SerializeField] private float machineGunfireRate = 0.1f;
    private float nextFireTime;

    public bool loaded = true;

    private float reloadTime;
    [SerializeField] private float setReloadTime;

    private Vector3 fireDestination; //used for where missile lands

    void Update()
    {
        if (Mouse.current == null) return;

        // Update fireDestination and cursor radius
        UpdateCursorAndDestination();

        // Main turret fire
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (loaded == true)
            {
                Debug.Log("loaded is true");
                FireMissile();
                loaded = false;
            } 
            else Debug.Log("loaded is false");          
            
        }

        // Machine gun fire
        if (Mouse.current.rightButton.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + machineGunfireRate;
            FireMachineGun();
        }

        // Reload timer
        if (reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
        }
    }

    void UpdateCursorAndDestination()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        cursorRadius.transform.position = mouseWorldPos;
        cursorRadius.transform.localScale = Vector3.one * explosionRadius * 2f;

        fireDestination = mouseWorldPos;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying || Mouse.current == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(mouseWorldPos, explosionRadius);
    }

    void FireMissile()
    {
        // Destroy all ExplosionFire(clone) objects
        GameObject[] oldExplosions = GameObject.FindGameObjectsWithTag("ExplosionFire");

        foreach (GameObject obj in oldExplosions)
        {
            Destroy(obj);
        }

        reloadTime = setReloadTime; //trigger reload time

        // Spawn missile
        GameObject missileInstance = Instantiate(
            missilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        // Set missile stats
        Missile missileScript = missileInstance.GetComponent<Missile>();
        if (missileScript != null)
        {
            missileScript.damage = damage;
            missileScript.explosionRadius = explosionRadius;
            missileScript.fireDestination = fireDestination;
        }

        // Spawn muzzle flash
        if (muzzleFlashSmokePrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashSmokePrefab, firePoint.position, firePoint.rotation);
            Destroy(flash, 1.5f);
        }
    }

    void FireMachineGun()
    {
        GameObject bulletObj = Instantiate(machineGunBulletPrefab, firePoint_machineGun.position, firePoint_machineGun.rotation);
    }
    
}