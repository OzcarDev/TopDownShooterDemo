using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : TankMovement
{
    [SerializeField] private float attackRange = 4f;
    [SerializeField] private float coinChance = 10f;
    private Transform player;
    private ShootingSystem ShootingSystem;
    private float missileChance = 1f;

    public float MovementSpeed { set { movementSpeed = value; } }
    public float RotationSpeed { set { rotationSpeed = value; } }

    public float MissileChance { set { missileChance = value; } }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ShootingSystem = gameObject.GetComponent<ShootingSystem>();
    }

    private void Update()
    {
        if (player != null && !GameManager.Instance.IsPaused) 
        { 

            RotateCanon(player.transform.position);

            Attack();
            
        }
    }

    private void FixedUpdate()
    {
        if (player != null && !GameManager.Instance.IsPaused)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance > attackRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;

                Move(direction);
            }

        }
    }

    private void Attack()
    {
        float randomValue = Random.Range(0f, 100f);
        
        if (randomValue <= missileChance) 
        {
            StartCoroutine(ShootingSystem.ShootingBulletsCoroutine(Proyectile.ProyectileType.missile));
        }
        else
        {
            StartCoroutine(ShootingSystem.ShootingBulletsCoroutine(Proyectile.ProyectileType.bullet));
        }
    }

    private void SpawnCoin()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= coinChance) 
        {
            GameObject actualCoin = GameManager.Instance.CoinPool.RequestObject();
            actualCoin.transform.position = transform.position;
        }
    }

    public void OnDead()
    {
        Debug.Log("EnemyDead");
        rb.velocity = Vector2.zero;
        GameManager.Instance.CheckEndOfWave();
        GameManager.Instance.IncreaseScore(10);
        SpawnCoin();
    }
}
