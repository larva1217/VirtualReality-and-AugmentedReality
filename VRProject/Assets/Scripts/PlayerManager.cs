using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats playerStats;

    public TMP_Text healthText;
    public TMP_Text goldText;
    public TMP_Text attackText;
    public TMP_Text KillText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerStats != null)
        {
            healthText.text = "Health: " + playerStats.Health;
            attackText.text = "Attack: " + playerStats.attackPower;
            goldText.text = "Gold: " + playerStats.gold;
            KillText.text = "Kill: " + EnemyController.kill;
        }
    }
}
