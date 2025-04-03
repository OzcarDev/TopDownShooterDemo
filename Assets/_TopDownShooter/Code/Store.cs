using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    private int playerMoney = 0;
    public int PlayerMoney {  get { return playerMoney; } set { playerMoney = value; } }

    [Header("Player Systems")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private HealthSystem healthSystem;

    [Header("Upgrades")]
    [SerializeField] private float maxHealthUpgrade;
    [SerializeField] private float maxShieldUpgrade;
    [SerializeField] private float shieldRegenUpgrade;
    [SerializeField] private int missileUpgrade;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerMoneyUI;
    [SerializeField] private TextMeshProUGUI totalWaves;
    [SerializeField] private GameObject errorMessage;

    private static Store instance;
    public static Store Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void DrawUI()
    {
        playerMoneyUI.text = playerMoney.ToString();
        totalWaves.text = "Wave " + GameManager.Instance.WaveIndex.ToString() + " Completed!";
    }
    private bool Buy(int Price)
    {
        if (playerMoney >= Price) 
        {
            playerMoney -= Price;
            DrawUI();
            return true;
        }
        else
        {
            StartCoroutine(ShowErrorMessage());
            return false;
        }
    }

    private IEnumerator ShowErrorMessage()
    {
        errorMessage.SetActive(true);
        yield return new WaitForSeconds(3);
        errorMessage.SetActive(false);
    }

    public void RestoreHealth(int price)
    {
        if (Buy(price))
        {
            healthSystem.RestoreHealth();
        }
    }

    public void IncreaseMaxHealth(int price)
    {
        if (Buy(price)) 
        {
            healthSystem.MaxHealth += maxHealthUpgrade;
        }
    }

    public void IncreaseMaxShield(int price)
    {
        if (Buy(price))
        {
            healthSystem.MaxShield += maxShieldUpgrade;
        }
    }

    public void IncreaseShieldRegen(int price)
    {
        if (Buy(price))
        {
            healthSystem.ShieldRegenRate += shieldRegenUpgrade;
        }
    }

    public void RestoreMissile(int price)
    {
        if (Buy(price))
        {
            playerController.RestoreMissile();
        }
    }

    public void IncreaseMissileCapacity(int price)
    {
        if (Buy(price))
        {
            playerController.MaxMissileAmount += missileUpgrade;
        }
    }
}
