using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private Pool enemyPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float spawnRate = 1.0f;
    


    [Header("Default Enemy Values")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float bulletDamage = 1f;
    [SerializeField] private float missileDamage = 10f;
    [SerializeField] private float shootingSpeed = 1f;
    [SerializeField] private float missileChance = 10;
    [SerializeField] private float health = 10;
    

    [Header("Difficulty Multipliers")]
    [SerializeField] private float movementSpeedMulti = 1.1f;
    [SerializeField] private float rotationSpeedMulti = 1.1f;
    [SerializeField] private float bulletDamageMulti = 1.1f;
    [SerializeField] private float missileDamageMulti = 1.1f;
    [SerializeField] private float shootingSpeedMulti = 0.95f;
    [SerializeField] private float missileChanceIncrease = 1;
    [SerializeField] private float healthIncrease = 1;
    [SerializeField] private int enemyAmountIncrease = 1;
    [SerializeField] private float spawnRateMulti = 0.95f;


    private void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        GameManager.Instance.WaveIndex++;
        GameManager.Instance.IsWaveActive = true;
        GameManager.Instance.ResumeGame();
        StartCoroutine(WaveCoroutine());
    }
    private IEnumerator WaveCoroutine()
    {
        for (int index = 0; index < enemyCount; index++) 
        {
            GameObject actualEnemy = enemyPool.RequestObject();

            actualEnemy.transform.position = GetSpawnPosition();

            SetEnemySpecs(actualEnemy.GetComponent<Enemy>(),actualEnemy.GetComponent<ShootingSystem>(),actualEnemy.GetComponent<HealthSystem>());

            yield return new WaitForSeconds(spawnRate);
        }

        

        GameManager.Instance.IsWaveActive = false;
        IncreaseDifficulty();

    }

    private Vector3 GetSpawnPosition()
    {
        int randomIndex = Random.Range(0,spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }
    private void IncreaseDifficulty()
    {
        movementSpeed *= movementSpeedMulti;
        rotationSpeed *= rotationSpeedMulti;
        bulletDamage *= bulletDamageMulti;
        missileDamage *= missileDamageMulti;
        shootingSpeed *= shootingSpeedMulti;
        missileChance += missileChanceIncrease;
        health += healthIncrease;
        enemyCount += enemyAmountIncrease;
        spawnRate *=spawnRateMulti;
    }

    private void SetEnemySpecs(Enemy enemy, ShootingSystem enemyShootingSystem,HealthSystem enemyLifeSystem)
    {
        enemy.MovementSpeed = movementSpeed;
        enemy.RotationSpeed = rotationSpeed;
        enemy.MissileChance = missileChance;
        enemyShootingSystem.BulletDamage = bulletDamage;
        enemyShootingSystem.MissileDamage = missileDamage;
        enemyShootingSystem.ShootingSpeed = shootingSpeed;
        enemyLifeSystem.MaxHealth = health;
        enemyLifeSystem.RestoreHealth();
    }
}
