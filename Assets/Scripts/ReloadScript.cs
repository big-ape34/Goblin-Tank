using UnityEngine;
using UnityEngine.InputSystem;

public class ReloadScript : MonoBehaviour
{
    public GameObject expended;
    public GameObject unloaded;
    public GameObject loaded;

    public GameObject chamber;

    public Animator chamberAnimator;

    void Start()
    {
        chamberAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current.downArrowKey.isPressed)
        {
            chamberAnimator.SetBool("IdleOpen", true);
            chamberAnimator.SetBool("IdleLoaded", false);
            chamberAnimator.SetBool("Idle", false);
        } 
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            chamberAnimator.SetBool("IdleOpen", false);
            chamberAnimator.SetBool("IdleLoaded", true);
            chamberAnimator.SetBool("Idle", false);
        } 
        if (Keyboard.current.upArrowKey.isPressed)
        {
            chamberAnimator.SetBool("IdleOpen", false);
            chamberAnimator.SetBool("IdleLoaded", false);
            chamberAnimator.SetBool("Idle", true);
        }

    }

    public void Loaded()
    {
        
    }

}
