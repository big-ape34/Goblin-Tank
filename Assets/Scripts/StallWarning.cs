using UnityEngine;
using UnityEngine.UI;

public class StallWarning : MonoBehaviour
{
    public Image stallImage;

    [SerializeField] GameObject _tankBody;

    public bool isStalled;

    private TankMovement _stallSource;

    public float maxValue = 400;
    public float currentValue = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         // Cache the component so Update doesn't have to "look" for it 60 times a second
        if (_tankBody != null)
        {
            _stallSource = _tankBody.GetComponent<TankMovement>();
        }
    }
    void Update()
    {
        if (_stallSource != null)
        {
            isStalled = _stallSource.stalled;

            if (isStalled == true) currentValue = 150;
            if (isStalled == false) currentValue = 100;

            float fillAmount = Mathf.Clamp01(currentValue / maxValue);
            stallImage.fillAmount = fillAmount;

        }
       
    }

    
}
