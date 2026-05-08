using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerStats : MonoBehaviour
{
    public int gold = 0;
    public int maxHealth = 10000;
    public int attackPower = 20;

    public int Health=0;

    AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip notSound;

    public AudioClip GameOverSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Health = maxHealth;
    }

    public void TakeDamage(int damage) {
    Health -= damage;
    if (Health <= 0) {
        audioSource.PlayOneShot(GameOverSound);
        Debug.Log("플레이어 사망!");
        RestartScene();
    }
}

    public void AddGold(int amount)
    {
        gold += amount;
        
    }

    public void UpgradeHealth()
    {
        if (gold >= 20) {
            maxHealth += 100;
            gold -= 20;
            audioSource.PlayOneShot(coinSound);
            Debug.Log("체력 증가");
        }
        else{
            audioSource.PlayOneShot(notSound);
            Debug.Log("돈이 부족합니다.");
        }
        
    }

    public void UpgradeAttack()
    {
        if (gold >= 20) {
            attackPower += 15;
            gold -= 20;
            audioSource.PlayOneShot(coinSound);
            Debug.Log("공격력 증가");
        }
        else{
            audioSource.PlayOneShot(notSound);
            Debug.Log("돈이 부족합니다.");
        }
        
    }

    void RestartScene()
    {
        EnemyController.kill = 0;
        SceneManager.LoadScene("vr_pROJECT"); 
    }

}