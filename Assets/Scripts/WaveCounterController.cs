using TMPro;
using UnityEngine;

public class WaveCounterController : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private TextMeshProUGUI waveText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Wave " + enemySpawner.GetComponent<EnemySpawner>().waveCount;
    }
}
