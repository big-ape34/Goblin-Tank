using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject _turret;
    [SerializeField] private GameObject _tankBody;

    public Vector2 AimDirection => _turret ? (Vector2)_turret.transform.up : Vector2.up;

    void Update()
    {
        if(_turret)
        {
            if(_tankBody)
            {
                transform.position = _tankBody.transform.position;
            }

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