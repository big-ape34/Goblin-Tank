using UnityEngine;
using UnityEngine.InputSystem;

public class ReloadScript : MonoBehaviour
{
    public GameObject expended;
    public GameObject unloaded;
    public GameObject loaded;

    public GameObject chamber;

    public Animator chamberAnimator;

    private float tick = 1f;

    public bool readyFire = true;

    void Start()
    {
        chamberAnimator = GetComponent<Animator>();
        tick = 1f;
        Debug.Log("tick is " + tick);

        readyFire = true;
    }

    void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            readyFire = false;
        }

        if (Keyboard.current.rKey.wasPressedThisFrame && tick == 1 && readyFire == false)
        {
            chamberAnimator.SetBool("IdleOpen", true);
            chamberAnimator.SetBool("IdleLoaded", false);
            chamberAnimator.SetBool("Idle", false);
            tick = 2f;
            Debug.Log("tick is " + tick);
        } 
        else if (Keyboard.current.rKey.wasPressedThisFrame && tick == 2 && readyFire == false)
        {
            chamberAnimator.SetBool("IdleOpen", false);
            chamberAnimator.SetBool("IdleLoaded", true);
            chamberAnimator.SetBool("Idle", false);
            tick = 3f;
            Debug.Log("tick is " + tick);
        } 
        else if (Keyboard.current.rKey.wasPressedThisFrame && tick == 3 && readyFire == false)
        {
            chamberAnimator.SetBool("IdleOpen", false);
            chamberAnimator.SetBool("IdleLoaded", false);
            chamberAnimator.SetBool("Idle", true);
            tick = 1f;
            Debug.Log("tick is " + tick);
        }

    }

}
