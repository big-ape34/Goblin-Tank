using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class HeatGagueUIController : MonoBehaviour
{

    public Image fillImage;
    [SerializeField] GameObject _tankBody;
    public float maxValue = 400f; 
    public float currentValue = 0f;

    private HeatGague _heatSource; // Store the reference here

    void Start()
    {
        // Cache the component so Update doesn't have to "look" for it 60 times a second
        if (_tankBody != null)
        {
            _heatSource = _tankBody.GetComponent<HeatGague>();
        }
    }

    void Update()
    {
        if (_heatSource != null)
        {
            currentValue = _heatSource.heat;

            // Use Mathf with an 'f'
            float fillAmount = Mathf.Clamp01(currentValue / maxValue);
            fillImage.fillAmount = fillAmount;
            
        }
    }

}
