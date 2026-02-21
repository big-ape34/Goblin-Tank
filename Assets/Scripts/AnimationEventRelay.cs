using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    // Drag the Parent (or any object) here in the Inspector
    public GameObject _turret; 

    // The Animation Event calls this function
    public void SetParentBoolTrue()
    {
        if (_turret != null)
        {
            // Get the script component from the dragged GameObject
            CombatController script = _turret.GetComponent<CombatController>();

            if (script != null)
            {
                script.loaded = true;
            }
        }
    }
}
