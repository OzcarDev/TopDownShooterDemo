using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missileText;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;

    [SerializeField] private PlayerController player;
    [SerializeField] private HealthSystem playerHealthSystem;

    void Update()
    {
        missileText.text = player.MissileAmount + "/" + player.MaxMissileAmount;
        scoreText.text = "Score: " + GameManager.Instance.PlayerScore;
        healthBar.fillAmount = (playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth);
        shieldBar.fillAmount = (playerHealthSystem.CurrentShield / playerHealthSystem.MaxShield);

    }

    
}
