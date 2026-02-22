using UnityEngine;

public class ShellEject : MonoBehaviour
{
    // Drag the child GameObject (the one with the Sprite & RB2D) here
    public GameObject childToLaunch;

    private Rigidbody2D childRb;
    private SpriteRenderer childSr;

    private ParticleSystem childPs;

    void Awake()
    {
        if (childToLaunch != null)
        {
            childRb = childToLaunch.GetComponent<Rigidbody2D>();
            childSr = childToLaunch.GetComponent<SpriteRenderer>();
            childPs = childToLaunch.GetComponent<ParticleSystem>();
            
            // Initialize: Hide and turn off physics
            childSr.enabled = false;
            childRb.simulated = false;
            childPs.Stop();
        }
    }

    public void LaunchChild()
    {
        if (childToLaunch == null) return;

        // 1. Show and Enable Physics
        childSr.enabled = true;
        childRb.simulated = true;
        childPs.Play();

        // 2. Reset Position RELATIVE to this parent script
        childToLaunch.transform.localPosition = new Vector3(-0.5f, -1.8f, 0f);
        
        // 3. Reset Velocity (prevents weird physics bugs)
        childRb.linearVelocity = Vector2.zero;
        childRb.angularVelocity = 0f;

        // 4. Eject! (Adjust these numbers to taste)
        childRb.AddForce(new Vector2(4f, 6f), ForceMode2D.Impulse);
        childRb.AddTorque(15f, ForceMode2D.Impulse);
    }
}
