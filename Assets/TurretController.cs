using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject _turret;

    void Update()
    {
        if(_turret)
        {
            if (Mouse.current == null) return;

            Vector2 mouseScreen = Mouse.current.position.ReadValue();
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
            mouseWorld.z = 0f;

            Vector2 direction = mouseWorld - _turret.transform.position;

            // This assumes your turret sprite faces UP by default
            _turret.transform.up = direction;
        } 
    }
}