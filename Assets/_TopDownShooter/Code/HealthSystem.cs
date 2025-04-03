using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    public float MaxHealth { set { maxHealth = value; } get { return maxHealth; } }
    [SerializeField] private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } }

    [Header("Shield")]
    [SerializeField] private float maxShield;
    public float MaxShield { set { maxShield = value; } get { return maxShield; } }
    [SerializeField] private float currentShield;
    public float CurrentShield { get { return currentShield; } }
    [SerializeField] private float shieldRegenRate;
    public float ShieldRegenRate { set { shieldRegenRate = value; } get { return shieldRegenRate; } }
    [SerializeField] private float timeBeforeRestoreShield;
   

    public UnityEvent OnDeath;
    private Coroutine shieldRegenCoroutine;

    private void Start()
    {
        RestoreHealth();
        shieldRegenCoroutine = StartCoroutine(RestoreShield());

    }
    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        if (GameManager.Instance.IsPaused) return;   
        if (currentShield > 0)
        {
            float damageToShield = Mathf.Min(damage, currentShield);
            currentShield -= damageToShield;
            damage -= (int)damageToShield;
        }
        if(damage > 0)
        {
            currentHealth -= damage;
        }

        if(currentHealth <= 0)
        {
            if (shieldRegenCoroutine != null)
            {
                StopCoroutine(shieldRegenCoroutine);
            }
            OnDeath.Invoke();
            return;
        }
        
        if(shieldRegenCoroutine != null)
        {
            StopCoroutine(shieldRegenCoroutine);
        }
        shieldRegenCoroutine = StartCoroutine(RestoreShield());
    }

   
    IEnumerator RestoreShield()
    {
        yield return new WaitForSeconds(timeBeforeRestoreShield);
       
        while (currentShield < maxShield)
        {
            currentShield += shieldRegenRate * Time.deltaTime;
            currentShield = Mathf.Min(currentShield, maxShield);
            yield return null;
        }

    }

}
