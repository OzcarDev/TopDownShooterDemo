using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Proyectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ProyectileType type;
    [SerializeField] private Animator animator;
    [SerializeField] private float explotionDuration;
    [SerializeField] private CircleCollider2D explotionCollider;
    private float damage = 5;
    public float Damage { set { damage = value; } }

    public enum ProyectileType
    {
        bullet,
        missile
    }
    private void OnEnable()
    {
        StartCoroutine(SetSpeed());
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    
    private void DamageOther(HealthSystem otherLife)
    {
        otherLife.Damage(damage);
    }

    private IEnumerator SetSpeed()
    {
        yield return new WaitForEndOfFrame();
        rb.velocity = transform.up * speed;
    }

    private IEnumerator MissileCoroutine()
    {
        Debug.Log("Entrando a CorutinaMisil");
        rb.velocity = Vector2.zero;
        animator.Play("Explotion");
        explotionCollider.enabled = true;
        yield return new WaitForSeconds(explotionDuration);
        animator.Play("Default");
        explotionCollider.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthSystem>() != null)
        {
            DamageOther(collision.gameObject.GetComponent<HealthSystem>());
        }

        if (type == ProyectileType.missile)
        {
            
            StartCoroutine (MissileCoroutine());
            
        }

        if(type == ProyectileType.bullet)
        {
            gameObject.SetActive(false);
        }
    }



}
