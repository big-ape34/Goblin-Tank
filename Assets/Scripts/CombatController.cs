using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public GameObject missilePrefab;
    public GameObject muzzleFlashSmokePrefab;

    [Header("Settings")]
    public float fireRate = 0.25f;
    public float damage = 5f;

    private float reloadTime;
    [SerializeField] private float setReloadTime;



    void Update()
    {
        if (Mouse.current == null) return;

        if (
            Mouse.current.leftButton.wasPressedThisFrame && 
            reloadTime <= 0
            )
        {
            Fire();
        }

        //Trigger reload
        if(reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
        }
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

        // Set the damage on the missile
        Missile missileScript = missileInstance.GetComponent<Missile>();
        if (missileScript != null)
        {
            missileScript.damage = damage;
        }

        // Spawn the muzzle flash
        if (muzzleFlashSmokePrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashSmokePrefab, firePoint.position, firePoint.rotation);
            Destroy(flash, 1.5f); // destroy after 0.5 seconds (so it doesn't linger)
        }
    }
}
