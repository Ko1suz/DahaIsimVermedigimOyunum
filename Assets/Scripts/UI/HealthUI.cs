using UnityEngine.UI;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Text healthText;

    void Start()
    {
        healthText = GetComponent<Text>();
    }

    void Update()
    {
        healthText.text = "Health : "+PlayerStatsScript.instance.currnetHealth.ToString();
    }
}
