using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float shootingSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float missileDamage;
   
    public float BulletDamage { set { bulletDamage = value; } }
    public float MissileDamage { set { missileDamage = value; } }
    public float ShootingSpeed { set { shootingSpeed = value; } }

    private bool canShoot = true;
    private Pool bulletPool;
    private Pool missilePool;
    
    private void Start()
    {
        if (isPlayer)
        {
            bulletPool = GameManager.Instance.BulletPool;
            missilePool = GameManager.Instance.MissilePool;
        }
        else
        {
            bulletPool = GameManager.Instance.EnemyBulletPool;
            missilePool = GameManager.Instance.EnemyMissilePool;
        }
        
    }
    private void OnEnable()
    {
        canShoot = true;
    }
    public IEnumerator ShootingBulletsCoroutine(Proyectile.ProyectileType type)
    {
        if (canShoot)
        {
            canShoot = false;

            while (!canShoot)
            {
                if(type == Proyectile.ProyectileType.bullet)
                {
                    ShootBullet();
                }
                if (type == Proyectile.ProyectileType.missile)
                {
                    ShootMisile();
                }
                yield return new WaitForSeconds(shootingSpeed);
                canShoot = true;
            }
        }
        else
        {
            yield return null;
        }
       
    }

    private void ShootBullet()
    {
        GameObject actualBullet = bulletPool.RequestObject();
        actualBullet.transform.position = shootingPoint.position;
        actualBullet.transform.rotation = shootingPoint.rotation;
        actualBullet.GetComponent<Proyectile>().Damage = bulletDamage;
    }
    private void ShootMisile()
    {
            GameObject actualMissile = missilePool.RequestObject();
            actualMissile.transform.position = shootingPoint.position;
            actualMissile.transform.rotation = shootingPoint.rotation;
            actualMissile.GetComponent<Proyectile>().Damage = missileDamage;  
    }

    

}
