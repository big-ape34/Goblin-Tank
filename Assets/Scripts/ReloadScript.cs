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

    void Start()
    {
        chamberAnimator = GetComponent<Animator>();
        tick = 1f;
        Debug.Log("tick is " + tick);
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame && tick == 1)
        {
            chamberAnimator.SetBool("IdleOpen", true);
            chamberAnimator.SetBool("IdleLoaded", false);
            chamberAnimator.SetBool("Idle", false);
            tick = 2f;
            Debug.Log("tick is " + tick);
        } 
        else if (Keyboard.current.rKey.wasPressedThisFrame && tick == 2)
        {
            chamberAnimator.SetBool("IdleOpen", false);
            chamberAnimator.SetBool("IdleLoaded", true);
            chamberAnimator.SetBool("Idle", false);
            tick = 3f;
            Debug.Log("tick is " + tick);
        } 
        else if (Keyboard.current.rKey.wasPressedThisFrame && tick == 3)
        {
            chamberAnimator.SetBool("IdleOpen", false);
            chamberAnimator.SetBool("IdleLoaded", false);
            chamberAnimator.SetBool("Idle", true);
            tick = 1f;
            Debug.Log("tick is " + tick);
        }

    }

}
