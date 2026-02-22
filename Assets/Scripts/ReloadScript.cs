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
        

        if (Mouse.current.leftButton.wasPressedThisFrame && tick == 1 && readyFire == true)
        {
            readyFire = false;
            chamberAnimator.SetBool("IdleOpen", true);
            chamberAnimator.SetBool("IdleLoaded", false);
            chamberAnimator.SetBool("Idle", false);
            Wait();
            tick = 2f;
            Debug.Log("tick is " + tick);
        } 
        else if (Keyboard.current.rKey.wasPressedThisFrame && tick == 2 && readyFire == false)
        {
            chamberAnimator.SetBool("IdleOpen", false);
            chamberAnimator.SetBool("IdleLoaded", true);
            chamberAnimator.SetBool("Idle", false);
            Wait();
            tick = 3f;
            Debug.Log("tick is " + tick);
        } 
        else if (Keyboard.current.rKey.wasPressedThisFrame && tick == 3 && readyFire == false)
        {
            chamberAnimator.SetBool("IdleOpen", false);
            chamberAnimator.SetBool("IdleLoaded", false);
            chamberAnimator.SetBool("Idle", true);
            Wait();
            tick = 1f;
            Debug.Log("tick is " + tick);
        }

    }

    async void Wait()
    {
        if (tick == 1) await System.Threading.Tasks.Task.Delay(500);
        if (tick == 2) await System.Threading.Tasks.Task.Delay(500);
        if (tick == 3) await System.Threading.Tasks.Task.Delay(100);
    }


}


